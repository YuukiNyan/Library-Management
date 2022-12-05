using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraCuuSach.Models;

namespace TraCuuSach
{
    public partial class FormCTPM : Form
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

        List<DetailBorrowSlip> detailSlips;
        string slipId;
        int index;

        public FormCTPM(string slipId)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
            this.slipId = slipId;
        }

        private void FormChiTietPhieuMuon_Load(object sender, EventArgs e)
        {
            btnDelete.BorderRadius = 12;
            btnCancel.BorderRadius = 12;
            btnExit.BorderRadius = 20;

            btnDelete.Enabled = false;
            btnCancel.Enabled = false;

            detailSlips = new List<DetailBorrowSlip>();
            LoadData();
        }

        private void LoadData()
        {
            detailSlips.Clear();
            dtgv.Rows.Clear();
            string queryCmd = $@"SELECT MaChiTietPhieuMuon, CTPHIEUMUON.MaCuonSach, TenDauSach, TinhTrangPM
            FROM CTPHIEUMUON, PHIEUMUON, DAUSACH, SACH, CUONSACH
            WHERE PHIEUMUON.MaPhieuMuonSach = '{slipId}' 
            AND PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach
            AND CUONSACH.MaCuonSach = CTPHIEUMUON.MaCuonSach
            AND CUONSACH.MaSach = SACH.MaSach
            AND DAUSACH.MaDauSach = SACH.MaDauSach";

            SqlConnection conn = new SqlConnection(FormMuonSach.str);
            conn.Open();
            SqlCommand cmd = new SqlCommand(queryCmd, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DetailBorrowSlip slip = new DetailBorrowSlip();
                    slip.id = reader.GetString(0);
                    slip.bookId = reader.GetString(1);
                    slip.bookName = reader.GetString(2);
                    slip.status = (reader.GetSqlBoolean(3)) ? "Đã trả" : "Chưa trả";
                    detailSlips.Add(slip);
                }
            }
            conn.Close();

            detailSlips.OrderBy(o => o.id).ThenBy(o => o.bookId).ThenBy(o => o.status).ToList();
            int stt = 1;
            foreach (DetailBorrowSlip slip in detailSlips)
            {
                stt++;
                dtgv.Rows.Add(new object[] { stt, slip.id, slip.bookId, slip.bookName, slip.status });
            }

            if (dtgv.Rows.Count != 0)
                dtgv.ClearSelection();
        }

        private void Clear()
        {
            lbSlipId.Text = "";
            lbDetailId.Text = "";
            lbBookId.Text = "";
            lbBookName.Text = "";
            lbStatus.Text = "";

            pnlSlipId.Width = 0;
            pnlDetailId.Width = 0;
            pnlBookId.Width = 0;
            pnlBookName.Width = 0;
            pnlStatus.Width = 0;

            btnDelete.Enabled = false;
            btnCancel.Enabled = false;

            if (dtgv.Rows.Count != 0)
                dtgv.ClearSelection();
        }

        private void dtgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            var i = e.RowIndex;
            if (i != -1)
            {
                index = i;
                lbSlipId.Text = slipId;
                lbDetailId.Text = dtgv.Rows[i].Cells[1].Value.ToString();
                lbBookId.Text = dtgv.Rows[i].Cells[2].Value.ToString();
                lbBookName.Text = dtgv.Rows[i].Cells[3].Value.ToString();
                lbStatus.Text = dtgv.Rows[i].Cells[4].Value.ToString();

                pnlSlipId.Width = lbSlipId.Width - 6;
                pnlDetailId.Width = lbDetailId.Width - 6;
                pnlBookId.Width = lbBookId.Width - 6;
                pnlBookName.Width = lbBookName.Width - 6;
                pnlStatus.Width = lbStatus.Width - 6;

                btnDelete.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = lbDetailId.Text;
            var result = MessageBox.Show($"Bạn có muốn xóa chi tiết phiếu mượn {id} không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                string queryUpdateCmd = $@"DELETE FROM CTPHIEUMUON
                WHERE MaChiTietPhieuMuon = '{id}'
            
                UPDATE CUONSACH
                SET TinhTrang = 0
                WHERE MaCuonSach = '{lbBookId.Text}'";

                SqlConnection conn = new SqlConnection(FormMuonSach.str);
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryUpdateCmd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                FormDanhSachPM.dataChanged = true;
                MessageBox.Show("Bạn đã xóa chi tiết phiếu mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clear();
                LoadData();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}