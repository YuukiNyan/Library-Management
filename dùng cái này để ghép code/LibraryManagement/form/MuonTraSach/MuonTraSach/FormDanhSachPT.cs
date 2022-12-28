using MuonTraSach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuonTraSach
{
    public partial class FormDanhSachPT : Form
    {
        #region DragForm
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region Make rounded form 
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );
        #endregion

        #region Drop Shadow
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        List<ReturnSlip> returnSlips;
        SqlConnection conn;
        SqlCommand cmd;
        int index;
        bool isLocked = true;
        public static bool dataChanged = false;
        int optUpdate = -1; //-1: Not changed; 1: Update; 2: Delete

        public FormDanhSachPT()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
        }

        private void FormDanhSachPT_Load(object sender, EventArgs e)
        {
            btnUpdate.BorderRadius = 12;
            btnDelete.BorderRadius = 12;
            btnCancel.BorderRadius = 12;
            btnDetail.BorderRadius = 12;
            btnExit.BorderRadius = 20;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnDetail.Enabled = false;

            dtpReturn.Enabled = false;

            returnSlips = new List<ReturnSlip>();
            conn = new SqlConnection(FormMuonSach.stringConnect);
            conn.Open();

            LoadReturnSlipsList();
        }

        private void LoadReturnSlipsList()
        {
            //Get list of return slips and load data grid view
            dtgv.Rows.Clear();
            returnSlips.Clear();
            int stt = 1;
            cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT DISTINCT PHIEUTRASACH.MaPhieuTraSach, PHIEUMUON.MaPhieuMuonSach, PHIEUTRASACH.MaDocGia, HoTen, NgTra, TienPhatKyNay
            FROM PHIEUTRASACH, DOCGIA, CTPT,PHIEUMUON
            WHERE PHIEUTRASACH.MaDocGia = DOCGIA.MaDocGia AND PHIEUTRASACH.MaPhieuTraSach = CTPT.MaPhieuTraSach 
			AND PHIEUMUON.MaPhieuMuonSach = CTPT.MaPhieuMuonSach
			ORDER BY MaPhieuTraSach, PHIEUTRASACH.MaDocGia";

            using (SqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    ReturnSlip slip = new ReturnSlip();
                    slip.id = reader.GetString(0);
                    slip.borrowSlipId = reader.GetString(1);
                    slip.readerId = reader.GetString(2);
                    slip.readerName = reader.GetString(3);
                    slip.returnDate = reader.GetDateTime(4).ToString("dd/MM/yyyy");
                    slip.fineThisPeriod = (long)reader.GetSqlMoney(5);
                    returnSlips.Add(slip);
                    dtgv.Rows.Add(new object[] { stt, slip.id, slip.readerId, slip.readerName, slip.returnDate, slip.fineThisPeriod });
                    stt++;
                }

            if (dtgv.Rows.Count != 0)
                if (optUpdate != 1)
                    dtgv.ClearSelection();
                else
                    dtgv.Rows[index].Selected = true;
        }

        private void Lock()
        {
            if (!isLocked)
            {
                btnDelete.Enabled = true;
                btnDetail.Enabled = true;
                dtpReturn.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnDetail.Enabled = false;
                dtpReturn.Enabled = false;
            }

            if (optUpdate != -1)
            {
                btnUpdate.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        private void UpdateData()
        {
            string query = "";
            string msg = "";
            string id = lbSlipId.Text;
            bool accept = false;
            if (optUpdate == 1)
            {
                query = $@"UPDATE PHIEUTRASACH
                    SET NgTra = '{dtpReturn.Value}'
                    WHERE MaPhieuTraSach = '{id}'";
                msg = "Lưu thay đổi thành công!";
                accept = true;
            }
            else if (optUpdate == 2)
            {
                var result = MessageBox.Show($"Bạn có muốn xóa phiếu trả sách {id} không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    query = $@"UPDATE CTPHIEUMUON SET TinhTrangPM = 0  WHERE MaChiTietPhieuMuon IN 
                    (SELECT MaChiTietPhieuMuon FROM CTPHIEUMUON, CTPT WHERE  CTPHIEUMUON.MaPhieuMuonSach = CTPT.MaPhieuMuonSach AND CTPT.MaPhieuTraSach = '{id}')
                    UPDATE CUONSACH SET TinhTrang = 0 WHERE CUONSACH.MaCuonSach IN(SELECT CTPT.MaCuonSach FROM CTPT WHERE CTPT.MaPhieuTraSach = '{id}')
                    DELETE FROM CTPT WHERE MaPhieuTraSach = '{id}'
                    DELETE FROM PHIEUTRASACH WHERE MaPhieuTraSach = '{id}'";
                    msg = $"Xóa phiếu trả sách {id} thành công!";
                    accept = true;
                }
                else
                    accept = false;
            }

            if (accept)
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadReturnSlipsList();
                optUpdate = -1;
                dataChanged = true;
            }
        }

        private void Clear()
        {
            index = -1;
            lbSlipId.Text = "";
            lbReaderId.Text = "";
            lbReaderName.Text = "";
            dtpReturn.Value = DateTime.Now;
            lbFine.Text = "";

            pnlSlipId.Width = 0;
            pnlReaderId.Width = 0;
            pnlReaderName.Width = 0;
            pnlFine.Width = 0;

            isLocked = true;
            Lock();
        }

        private void dtgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var i = e.RowIndex;
            if (i != -1)
            {
                isLocked = false;
                Lock();

                index = i;
                lbSlipId.Text = dtgv.Rows[i].Cells[1].Value.ToString();
                lbReaderId.Text = dtgv.Rows[i].Cells[2].Value.ToString();
                lbReaderName.Text = dtgv.Rows[i].Cells[3].Value.ToString();
                dtpReturn.Value = DateTime.ParseExact(dtgv.Rows[i].Cells[4].Value.ToString(), "dd/MM/yyyy", null);
                lbFine.Text = dtgv.Rows[i].Cells[5].Value.ToString();

                pnlSlipId.Width = lbSlipId.Width - 6;
                pnlReaderId.Width = lbReaderId.Width - 6;
                pnlReaderName.Width = lbReaderName.Width - 6;
                pnlFine.Width = lbFine.Width - 6;
            }
        }

        //private void dtpReturn_ValueChanged(object sender, EventArgs e)
        //{
        //    dateDropdown = false;
        //    Lock();
        //    //var value = (sender as DateTimePicker).Value;
        //    //if (index != -1)
        //    //{
        //    //    if (value != DateTime.ParseExact(returnSlips[index].returnDate, "dd/MM/yyyy", null) && dateDropdown)
        //    //        if (value < DateTime.ParseExact(returnSlips[index].returnDate, "dd/MM/yyyy", null))
        //    //        {
        //    //            MessageBox.Show("Bạn phải chọn ngày trả mới lớn hơn ngày cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //            (sender as DateTimePicker).Value = DateTime.ParseExact(returnSlips[index].returnDate, "dd/MM/yyyy", null);
        //    //            dataChanged = false;
        //    //        }
        //    //        else
        //    //            dataChanged = true;
        //    //}
        //    //dateDropdown = false;
        //    //Lock();
        //}

        //private void dtpReturn_DropDown(object sender, EventArgs e)
        //{
        //    dateDropdown = true;
        //}

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            optUpdate = 1;
            UpdateData();
            Lock();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpReturn.Value = DateTime.ParseExact(dtgv.Rows[index].Cells[4].Value.ToString(), "dd/MM/yyyy", null);
            btnUpdate.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            optUpdate = 2;
            UpdateData();
            if (dataChanged)
                Clear();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            new FormChiTietPT(returnSlips[index]).ShowDialog();

            if (FormChiTietPT.dataChanged)
            {
                this.Close();
                new FormDanhSachPT().ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (dataChanged)
            {
                this.Hide();
                new FormTraSach();
                MessageBox.Show("Dữ liệu vừa được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
