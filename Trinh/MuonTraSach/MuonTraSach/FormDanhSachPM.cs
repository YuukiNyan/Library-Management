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
    public partial class FormDanhSachPM : Form
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

        List<BorrowSlip> borrowSlips;
        SqlConnection conn;
        SqlCommand cmd;
        int index;
        int maxDays;
        bool isLocked = true;
        bool dateDropdown = false;
        public static bool dataChanged = false;
        int optUpdate = -1; //-1: Not changed; 1: Update; 2: Delete

        public FormDanhSachPM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
        }

        private void FormDanhSachPM_Load(object sender, EventArgs e)
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

            dtpBorrow.Enabled = false;

            borrowSlips = new List<BorrowSlip>();
            conn = new SqlConnection(FormMuonSach.stringConnect);
            conn.Open();

            //Get max number of days can be borrowed
            cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT SoNgayMuonMax FROM THAMSO";
            maxDays = int.Parse(cmd.ExecuteScalar().ToString());

            LoadBorrowSlipsList();
        }

        private void LoadBorrowSlipsList()
        {
            //Get list of borrow slips and load data grid view
            dtgv.Rows.Clear();
            borrowSlips.Clear();
            int stt = 1;
            cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT MaPhieuMuonSach, PHIEUMUON.MaDocGia, HoTen, NgMuon, HanTra
            FROM PHIEUMUON, DOCGIA
            WHERE PHIEUMUON.MaDocGia = DOCGIA.MaDocGia
			ORDER BY MaPhieuMuonSach, PHIEUMUON.MaDocGia, HoTen";

            using (SqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    BorrowSlip slip = new BorrowSlip();
                    slip.id = reader.GetString(0);
                    slip.readerId = reader.GetString(1);
                    slip.readerName = reader.GetString(2);
                    slip.borrowDate = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    slip.returnDate = reader.GetDateTime(4).ToString("dd/MM/yyyy");
                    borrowSlips.Add(slip);
                    dtgv.Rows.Add(new object[] { stt, slip.id, slip.readerId, slip.readerName, slip.borrowDate, slip.returnDate });
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
                dtpBorrow.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnDetail.Enabled = false;
                dtpBorrow.Enabled = false;
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
                query = $@"UPDATE PHIEUMUON
                    SET NgMuon = '{dtpBorrow.Value}', HanTra = '{lbReturnDate.Text}'
                    WHERE MaPhieuMuonSach = '{id}'";
                msg = "Lưu thay đổi thành công!";
                accept = true;
            }
            else if (optUpdate == 2)
            {
                var result = MessageBox.Show($"Bạn có muốn xóa phiếu mượn sách {id} không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    query = $@"UPDATE CUONSACH SET TinhTrang = 1 
                        WHERE CUONSACH.MaCuonSach IN (SELECT CTPHIEUMUON.MaCuonSach FROM CTPHIEUMUON 
                        WHERE CTPHIEUMUON.MaPhieuMuonSach = '{id}')
                        DELETE FROM CTPHIEUMUON WHERE MaPhieuMuonSach = '{id}'
                        DELETE FROM CTPT WHERE MaPhieuMuonSach = '{id}'
                        DELETE FROM PHIEUMUON WHERE MaPhieuMuonSach = '{id}'";
                    msg = $"Xóa phiếu mượn sách {id} thành công!";
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

                LoadBorrowSlipsList();
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
            lbReturnDate.Text = "";
            dtpBorrow.Value = DateTime.Now;

            pnlSlipId.Width = 0;
            pnlReaderId.Width = 0;
            pnlReaderName.Width = 0;
            pnlReturnDate.Width = 0;

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
                dtpBorrow.Value = DateTime.ParseExact(dtgv.Rows[i].Cells[4].Value.ToString(), "dd/MM/yyyy", null);
                lbReturnDate.Text = dtgv.Rows[i].Cells[5].Value.ToString();

                pnlSlipId.Width = lbSlipId.Width - 6;
                pnlReaderId.Width = lbReaderId.Width - 6;
                pnlReaderName.Width = lbReaderName.Width - 6;
                pnlReturnDate.Width = lbReturnDate.Width - 6;
            }
        }

        private void dtpBorrow_DropDown(object sender, EventArgs e)
        {
            dateDropdown = true;
        }

        private void dtpBorrow_ValueChanged(object sender, EventArgs e)
        {
            if (index != -1 && dateDropdown && (sender as DateTimePicker).Value != DateTime.ParseExact(borrowSlips[index].borrowDate, "dd/MM/yyyy", null))
                lbReturnDate.Text = dtpBorrow.Value.AddDays(maxDays).ToString("dd/MM/yyyy");
            dateDropdown = false;
            Lock();

            //if (value != DateTime.ParseExact(borrowSlips[index].borrowDate, "dd/MM/yyyy", null) && dateDropdown)         
            //if (value > DateTime.ParseExact(borrowSlips[index].returnDate, "dd/MM/yyyy", null))
            //{
            //    MessageBox.Show("Ngày mượn không thể trễ hơn hạn trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    (sender as DateTimePicker).Value = DateTime.ParseExact(borrowSlips[index].borrowDate, "dd/MM/yyyy", null);
            //    dataChanged = false;
            //}
            //else
            //    dataChanged = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            optUpdate = 1;
            UpdateData();
            Lock();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpBorrow.Value = DateTime.ParseExact(dtgv.Rows[index].Cells[4].Value.ToString(), "dd/MM/yyyy", null);
            lbReturnDate.Text = dtgv.Rows[index].Cells[5].Value.ToString();
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
            new FormChiTietPM(dtgv.Rows[index].Cells[1].Value.ToString()).ShowDialog();

            if (FormChiTietPM.deleteSlip)
            {
                this.Close();
                new FormDanhSachPM().ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (dataChanged)
            {
                this.Hide();
                new FormMuonSach();
                MessageBox.Show("Dữ liệu vừa được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
