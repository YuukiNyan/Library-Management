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
    public partial class FormChiTietPT : Form
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

        List<DetailReturnSlip> detailSlips;
        ReturnSlip slip;
        public static bool dataChanged = false;
        bool deleteSlip = false;

        public FormChiTietPT(ReturnSlip slip)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
            this.slip = slip;
        }

        private void FormChiTietPT_Load(object sender, EventArgs e)
        {
            btnDelete.BorderRadius = 12;
            btnExit.BorderRadius = 20;
            btnDelete.Enabled = false;

            lbSlipId.Text = slip.id;
            pnlSlipId.Width = lbSlipId.Width - 6;

            detailSlips = new List<DetailReturnSlip>();
            LoadDetailList();
        }

        private void LoadDetailList()
        {
            detailSlips.Clear();
            dtgv.Rows.Clear();
            string queryCmd = $@"SELECT MaChiTietPhieuTra, CTPT.MaCuonSach, TenDauSach, SoNgayMuon, TienPhat
            FROM CTPT, DAUSACH, SACH, CUONSACH
            WHERE CTPT.MaPhieuTraSach = '{slip.id}' 
            AND CUONSACH.MaCuonSach = CTPT.MaCuonSach
            AND CUONSACH.MaSach = SACH.MaSach
            AND DAUSACH.MaDauSach = SACH.MaDauSach
			ORDER BY MaChiTietPhieuTra, CTPT.MaPhieuTraSach, TenDauSach";

            SqlConnection conn = new SqlConnection(FormMuonSach.stringConnect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(queryCmd, conn);
            int stt = 1;
            using (SqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    DetailReturnSlip detail = new DetailReturnSlip(reader.GetString(0), slip.id, reader.GetString(1), reader.GetString(2), (int)reader.GetSqlInt32(3), (long)reader.GetSqlMoney(4));
                    detailSlips.Add(detail);
                    dtgv.Rows.Add(new object[] { stt, detail.id, detail.bookId, detail.bookName, detail.borrowDays, detail.fine });
                    stt++;
                }
            conn.Close();

            if (dtgv.Rows.Count != 0)
                dtgv.ClearSelection();
        }

        private void Clear()
        {
            lbDetailId.Text = "";
            lbBookId.Text = "";
            lbBookName.Text = "";
            lbBorrowDays.Text = "";
            lbFine.Text = "";

            pnlDetailId.Width = 0;
            pnlBookId.Width = 0;
            pnlBookName.Width = 0;
            pnlBorrowDays.Width = 0;
            pnlFine.Width = 0;

            btnDelete.Enabled = false;

            if (dtgv.Rows.Count != 0)
                dtgv.ClearSelection();
        }

        private void dtgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var i = e.RowIndex;
            if (i != -1)
            {
                lbDetailId.Text = dtgv.Rows[i].Cells[1].Value.ToString();
                lbBookId.Text = dtgv.Rows[i].Cells[2].Value.ToString();
                lbBookName.Text = dtgv.Rows[i].Cells[3].Value.ToString();
                lbBorrowDays.Text = dtgv.Rows[i].Cells[4].Value.ToString();
                lbFine.Text = dtgv.Rows[i].Cells[5].Value.ToString();

                pnlDetailId.Width = lbDetailId.Width - 6;
                pnlBookId.Width = lbBookId.Width - 6;
                pnlBookName.Width = lbBookName.Width - 6;
                pnlBorrowDays.Width = lbBorrowDays.Width - 6;
                pnlFine.Width = lbFine.Width - 6;

                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = lbDetailId.Text;
            string msg = $"Bạn có muốn xóa chi tiết phiếu trả {id} không?";
            if (dtgv.Rows.Count == 1)
            {
                msg += $"\n\nNếu xóa chi tiết phiếu trả {id} thì sẽ xóa luôn phiếu trả {slip.id}!";
                deleteSlip = true;
                dataChanged = true;
            }
            var result = MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                string queryUpdateCmd = $@"DELETE FROM CTPT WHERE MaChiTietPhieuTra = '{id}'
                UPDATE CUONSACH SET TinhTrang = 0 WHERE MaCuonSach = '{lbBookId.Text}'
                UPDATE CTPHIEUMUON SET TinhTrangPM = 0 WHERE MaChiTietPhieuMuon = '{new ReturnBook().GetDetailBorrowId(slip.borrowSlipId, lbBookId.Text)}'
                UPDATE PHIEUTRASACH SET TienPhatKyNay = TienPhatKyNay - {lbFine.Text} WHERE MaPhieuTraSach = '{slip.id}'";
                if (deleteSlip)
                    queryUpdateCmd += $@"DELETE FROM PHIEUTRASACH WHERE MaPhieuTraSach = '{slip.id}'";
                SqlConnection conn = new SqlConnection(FormMuonSach.stringConnect);
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryUpdateCmd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Bạn đã xóa chi tiết phiếu trả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (deleteSlip)
                    this.Close();
                dataChanged = true;
                Clear();
                LoadDetailList();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
