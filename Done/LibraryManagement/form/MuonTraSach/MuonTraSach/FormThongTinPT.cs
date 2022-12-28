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
    public partial class FormThongTinPT : Form
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

        public static ReturnSlip returnSlip;

        public FormThongTinPT()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
        }

        private void FormThongTinPT_Load(object sender, EventArgs e)
        {
            this.dtgvChosen.AutoGenerateColumns = false;
            btnDone.BorderRadius = 20;
            btnCancel.BorderRadius = 20;

            lbSlipId.Text = returnSlip.id;
            lbReaderId.Text = returnSlip.readerId;
            lbReaderName.Text = returnSlip.readerName;
            lbReturnDate.Text = DateTime.Parse(returnSlip.returnDate).ToString("dd/MM/yyyy");
            lbFine.Text = returnSlip.fineThisPeriod.ToString();
            lbTotalFine.Text = returnSlip.totalFine.ToString();

            pnlSlipId.Width = lbSlipId.Width - 6;
            pnlReaderId.Width = lbReaderId.Width - 6;
            pnlReaderName.Width = lbReaderName.Width - 6;
            pnlReturnDate.Width = lbReturnDate.Width - 6;
            pnlFine.Width = lbFine.Width - 6;
            pnlTotalFine.Width = lbTotalFine.Width - 8;

            returnSlip.returnBooks = new BindingList<ReturnBook>(returnSlip.returnBooks.OrderBy(o => o.id).ThenBy(o => o.bookName).ToList());
            foreach (ReturnBook b in returnSlip.returnBooks)
                dtgvChosen.Rows.Add(b.id, b.bookName, b.borrowDate, b.borrowedDays, b.fine);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (FormTraSach.print)
                Print();
            UpdateData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormTraSach.returnState = "Cancelled";
            this.Close();
        }

        private void UpdateData()
        {
            string createReturnSlip = $@"INSERT INTO PHIEUTRASACH(MaPhieuTraSach, MaDocGia, NgTra, TienPhatKyNay) VALUES('{returnSlip.id}', '{returnSlip.readerId}', '{returnSlip.returnDate}', {returnSlip.fineThisPeriod})";
            string insertDetail = "";
            string updateStatus = "";

            foreach (ReturnBook book in returnSlip.returnBooks)
            {
                insertDetail += $"INSERT INTO CTPT(MaPhieuTraSach, MaCuonSach, MaPhieuMuonSach, SoNgayMuon, TienPhat) VALUES('{returnSlip.id}','{book.id}','{book.borrowSlipId}', {book.borrowedDays}, {book.fine})" + "\n";
                updateStatus += $@"UPDATE CTPHIEUMUON SET TinhTrangPM = 1 WHERE MaChiTietPhieuMuon = '{book.detailBorrowId}'
                UPDATE CUONSACH SET TinhTrang = 1 WHERE MaCuonSach = '{book.id}'" + "\n";
            }

            SqlConnection conn = new SqlConnection(FormMuonSach.stringConnect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(createReturnSlip, conn);
            cmd.ExecuteNonQuery();
            cmd.CommandText = insertDetail;
            cmd.ExecuteNonQuery();
            cmd.CommandText = updateStatus;
            cmd.ExecuteNonQuery();
            conn.Close();

            FormTraSach.returnState = "Success";
            this.Close();
        }
        #region Print Return Slip
        Bitmap bmp;

        private void Print()
        {
            bmp = new Bitmap(pnlPrint.Width, pnlPrint.Height);
            pnlPrint.DrawToBitmap(bmp, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height));
            printDocument1.DocumentName = "ReturnSlip_" + returnSlip.id;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(bmp, (pagearea.Width / 2) - (pnlPrint.Width / 2), pnlPrint.Location.Y);
        }
        #endregion
    }
}
