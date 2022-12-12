using MuonTraSach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuonTraSach
{
    public partial class FormChiTietPM : Form
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
        public static bool deleteSlip = false;

        public FormChiTietPM(string slipId)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
            this.slipId = slipId;
        }

        private void FormChiTietPM_Load(object sender, EventArgs e)
        {
            btnDelete.BorderRadius = 12;
            btnExit.BorderRadius = 20;
            btnDelete.Enabled = false;

            lbSlipId.Text = slipId;
            pnlSlipId.Width = lbSlipId.Width - 6;

            detailSlips = new List<DetailBorrowSlip>();
            LoadDetailList();
        }

        private void LoadDetailList()
        {
            detailSlips.Clear();
            dtgv.Rows.Clear();
            string queryCmd = $@"SELECT MaChiTietPhieuMuon, CTPHIEUMUON.MaCuonSach, TenDauSach, TinhTrangPM
            FROM CTPHIEUMUON, PHIEUMUON, DAUSACH, SACH, CUONSACH
            WHERE PHIEUMUON.MaPhieuMuonSach = '{slipId}' 
            AND PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach
            AND CUONSACH.MaCuonSach = CTPHIEUMUON.MaCuonSach
            AND CUONSACH.MaSach = SACH.MaSach
            AND DAUSACH.MaDauSach = SACH.MaDauSach
			ORDER BY MaChiTietPhieuMuon, CTPHIEUMUON.MaCuonSach, TenDauSach";

            SqlConnection conn = new SqlConnection(FormMuonSach.stringConnect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(queryCmd, conn);
            int stt = 1;
            using (SqlDataReader reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    DetailBorrowSlip slip = new DetailBorrowSlip(reader.GetString(0), slipId, reader.GetString(1), reader.GetString(2), (reader.GetSqlBoolean(3)) ? "Đã trả" : "Chưa trả");
                    detailSlips.Add(slip);
                    dtgv.Rows.Add(new object[] { stt, slip.id, slip.bookId, slip.bookName, slip.status });
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
            lbStatus.Text = "";

            pnlDetailId.Width = 0;
            pnlBookId.Width = 0;
            pnlBookName.Width = 0;
            pnlStatus.Width = 0;

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
                lbStatus.Text = dtgv.Rows[i].Cells[4].Value.ToString();

                pnlDetailId.Width = lbDetailId.Width - 6;
                pnlBookId.Width = lbBookId.Width - 6;
                pnlBookName.Width = lbBookName.Width - 6;
                pnlStatus.Width = lbStatus.Width - 6;

                btnDelete.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = lbDetailId.Text;
            string msg = $"Bạn có muốn xóa chi tiết phiếu mượn {id} không?";
            if (dtgv.Rows.Count == 1)
            {
                msg += $"\n\nNếu xóa chi tiết phiếu mượn {id} thì sẽ xóa luôn phiếu mượn {slipId}!";
                deleteSlip = true;
            }
            var result = MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                string queryUpdateCmd = $@"DELETE FROM CTPHIEUMUON
                WHERE MaChiTietPhieuMuon = '{id}'
            
                UPDATE CUONSACH
                SET TinhTrang = 1
                WHERE MaCuonSach = '{lbBookId.Text}'
                ";
                if (deleteSlip)
                    queryUpdateCmd += $@" DELETE FROM PHIEUMUON
                    WHERE MaPhieuMuonSach = '{slipId}'";
                SqlConnection conn = new SqlConnection(FormMuonSach.stringConnect);
                conn.Open();
                SqlCommand cmd = new SqlCommand(queryUpdateCmd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Bạn đã xóa chi tiết phiếu mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (deleteSlip)
                    this.Close();
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
