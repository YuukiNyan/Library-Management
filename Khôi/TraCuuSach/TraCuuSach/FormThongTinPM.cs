using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraCuuSach.Models;

namespace TraCuuSach
{
    public partial class FormThongTinPM : Form
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

        public static BorrowSlip borrowSlip;
        BindingSource bindingChosen;

        public FormThongTinPM()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
        }
        private void FormXacNhanMuonSach_Load(object sender, EventArgs e)
        {
            btnDone.BorderRadius = 20;
            btnCancel.BorderRadius = 20;

            lbSlipId.Text = borrowSlip.id;
            lbReaderId.Text = borrowSlip.readerId;
            lbReaderName.Text = borrowSlip.readerName;
            lbBorrowDate.Text = DateTime.Parse(borrowSlip.borrowDate).ToString("dd/MM/yyyy");
            lbReturnDate.Text = DateTime.Parse(borrowSlip.returnDate).ToString("dd/MM/yyyy");
            lbAmount.Text = borrowSlip.amount.ToString();

            pnlSlipId.Width = lbSlipId.Width - 6;
            pnlReaderId.Width = lbReaderId.Width - 6;
            pnlReaderName.Width = lbReaderName.Width - 6;
            pnlBorrowDate.Width = lbBorrowDate.Width - 6;
            pnlReturnDate.Width = lbReturnDate.Width - 6;
            pnlAmount.Width = lbAmount.Width - 8;

            bindingChosen = new BindingSource();
            bindingChosen.DataSource = borrowSlip.chosenBooks;
            dtgvChosen.DataSource = bindingChosen;

            if (dtgvChosen.Rows.Count != 0)
                dtgvChosen.Rows[0].Selected = false;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (FormMuonSach.askBeforePrint)
                Print();
            UpdataData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormMuonSach.borrowState = "Cancelled";
            this.Close();
        }

        private void UpdataData()
        {
            string createBorrowSlipCmd = $@"INSERT INTO PHIEUMUON (MaDocGia, NgMuon, HanTra) VALUES('{borrowSlip.readerId}','{borrowSlip.borrowDate}','{borrowSlip.returnDate}')";
            string insertSlipDetail = "";
            string updateBookState = "";

            foreach (Book book in borrowSlip.chosenBooks)
            {
                insertSlipDetail = insertSlipDetail + $@"INSERT INTO CTPHIEUMUON(MaPhieuMuonSach, MaCuonSach, TinhTrangPM) VALUES('{borrowSlip.id}','{book.id}', 0)" + "\n";
                updateBookState = updateBookState + $@"UPDATE CUONSACH SET TinhTrang = 1 WHERE MaCuonSach = '{book.id}'" + "\n";
            }

            SqlConnection conn = new SqlConnection(FormMuonSach.str);
            conn.Open();
            SqlCommand cmd = new SqlCommand(createBorrowSlipCmd, conn);
            cmd.ExecuteNonQuery();
            cmd.CommandText = insertSlipDetail;
            cmd.ExecuteNonQuery();
            cmd.CommandText = updateBookState;
            cmd.ExecuteNonQuery();
            conn.Close();

            FormMuonSach.borrowState = "Success";
            this.Close();
        }
        #region Print Borrow Slip
        Bitmap bmp;

        private void Print()
        {
            bmp = new Bitmap(pnlPrint.Width, pnlPrint.Height);
            pnlPrint.DrawToBitmap(bmp, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height));
            printDocument1.DocumentName = "BorrowSlip_" + borrowSlip.id;
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
