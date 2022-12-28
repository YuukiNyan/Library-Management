using MuonTraSach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MuonTraSach
{
    public partial class FormMuonSach : Form
    {
        // TINHTRANG CUONSACH = 1: Available;; = 0: Is borrowed
        SqlConnection connection;
        SqlCommand command;
        public static string stringConnect = @"Data Source=LAPTOP-RDTT4402;Initial Catalog=QLTV1;Integrated Security=True";

        List<Reader> readers;
        BindingList<Book> stockBooks;
        BindingList<Book> chosenBooks;

        BindingSource bingdingStock;
        BindingSource bingdingChosen;

        int max;
        int maxDays;
        int addRow = -1, removeRow = -1;
        int numborrowedBooks = -1;
        string newBorrowSlip;

        public static string borrowState = "";
        public static bool print = true;

        public FormMuonSach()
        {
            InitializeComponent();
        }

        private void FormMuonSach_Load(object sender, EventArgs e)
        {
            this.dtgvStock.AutoGenerateColumns = false;
            this.dtgvChosen.AutoGenerateColumns = false;
            btnBorrowList.BorderRadius = 12;
            btnBorrow.BorderRadius = 12;
            btnUpdate.BorderRadius = 12;
            btnAdd.BorderRadius = 15;
            btnRemove.BorderRadius = 15;
            btnBorrow.Enabled = false;
            txtReaderName.Enabled = false;
            dtpReturn.Enabled = false;

            connection = new SqlConnection(stringConnect);
            connection.Open();

            readers = new List<Reader>();
            stockBooks = new BindingList<Book>();
            chosenBooks = new BindingList<Book>();

            //Get list of readers and load combobox
            command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM DOCGIA  WHERE NgHetHan >= GETDATE()";
            using (SqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    Reader r = new Reader(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6), reader.GetDateTime(7), (long)reader.GetSqlMoney(8));
                    readers.Add(r);
                    cbbReaderId.Items.Add(r.id);
                }
            cbbReaderId.DisplayMember = "MaDocGia";
            cbbReaderId.SelectedIndex = -1;

            LoadData();

            dtpReturn.Value = dtpBorrow.Value.AddDays(maxDays);
            lbMaxBorrow.Text = "Số sách được mượn tối đa: " + max;
            lbAmount.Text = "Số lượng: " + dtgvChosen.Rows.Count;
        }

        private void LoadData()
        {
            //Get list of books in stock and fill datagridview
            command = connection.CreateCommand();
            command.CommandText = @"SELECT DISTINCT MaCuonSach, CUONSACH.MaSach, TenDauSach, TenTheLoai, TenTacGia
            FROM SACH, DAUSACH, CUONSACH, THELOAI, CTTACGIA, TACGIA
            WHERE SACH.MaDauSach = DAUSACH.MaDauSach AND SACH.MaSach = CUONSACH.MaSach
			AND DAUSACH.MaTheLoai = THELOAI.MaTheLoai AND DAUSACH.MaDauSach = CTTACGIA.MaDauSach 
			AND CTTACGIA.MaTacGia = TACGIA.MaTacGia AND TinhTrang = 1
			AND CUONSACH.MaCuonSach = (SELECT MAX(B.MaCuonSach)
				FROM CUONSACH B
				WHERE B.MaSach = CUONSACH.MaSach AND B. TinhTrang = 1)
			ORDER BY CUONSACH.MaCuonSach, MaSach, TenDauSach";
            using (SqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    Book b = new Book(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    stockBooks.Add(b);
                }
            bingdingStock = new BindingSource();
            bingdingChosen = new BindingSource();
            FillDataGridView(stockBooks, dtgvStock, bingdingStock);

            //Get max number of books can be borrowed
            command = connection.CreateCommand();
            command.CommandText = @"SELECT SoSachMuonMax FROM THAMSO";
            max = int.Parse(command.ExecuteScalar().ToString());

            //Get max number of days can be borrowed
            command = connection.CreateCommand();
            command.CommandText = @"SELECT SoNgayMuonMax FROM THAMSO";
            maxDays = int.Parse(command.ExecuteScalar().ToString());

            //Get the last borrow slip
            command = connection.CreateCommand();
            command.CommandText = @"SELECT TOP(1) MAPHIEUMUONSACH
            FROM PHIEUMUON
            ORDER BY MaPhieuMuonSach DESC";
            if (command.ExecuteScalar() != null)
            {
                string last = command.ExecuteScalar().ToString();
                int stt = int.Parse(last.Substring(4, 3)) + 1;
                newBorrowSlip = $"MPMS{stt:000}";
            }
            else
                newBorrowSlip = "MPMS001";
        }

        private void FillDataGridView(BindingList<Book> list, DataGridView dtgv, BindingSource bd)
        {
            //bd.DataSource = list.OrderBy(o => o.id).ToList();
            bd.DataSource = list;
            dtgv.DataSource = bd;

            if (dtgv.Rows.Count != 0)
                dtgv.ClearSelection();

            lbAmount.Text = "Số lượng: " + dtgvChosen.Rows.Count;
            if (dtgvChosen.Rows.Count > 0)
                btnBorrow.Enabled = true;
            else
                btnBorrow.Enabled = false;
        }

        private void ChangeBook(int opt)
        {
            //1: Add; 2: Remove
            if (opt == 1)
            {
                if (cbbReaderId.SelectedIndex == -1)
                    MessageBox.Show("Độc giả không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (chosenBooks.Count + numborrowedBooks + 1 > max && toggleButton1.Checked)
                    MessageBox.Show("Không được mượn quá " + max + " cuốn sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (addRow < 0)
                    MessageBox.Show("Bạn chưa chọn cuốn sách cần thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    ChangeBookBetweenTwoList(stockBooks, chosenBooks, addRow);
                    FillDataGridView(chosenBooks, dtgvChosen, bingdingChosen);
                    FillDataGridView(stockBooks, dtgvStock, bingdingStock);
                }
            }
            else if (opt == 2)
            {
                if (removeRow < 0)
                    MessageBox.Show("Vui lòng chọn sách cần bỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (dtgvChosen.Rows.Count > 0)
                {
                    ChangeBookBetweenTwoList(chosenBooks, stockBooks, removeRow);
                    FillDataGridView(chosenBooks, dtgvChosen, bingdingChosen);
                    FillDataGridView(stockBooks, dtgvStock, bingdingStock);
                }
            }
        }

        private void ChangeBookBetweenTwoList(BindingList<Book> l1, BindingList<Book> l2, int i)
        {
            int j = 0;
            foreach (Book b in l1)
            {
                if (i == j)
                {
                    l2.Add(b);
                    l1.Remove(b);
                    return;
                }
                else
                    j++;
            }
        }

        private void cbbReaderId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbb = (sender as ComboBox);
            numborrowedBooks = -1;

            if (cbb.SelectedIndex != -1)
            {
                txtReaderName.Text = readers[cbbReaderId.SelectedIndex].name;

                command = connection.CreateCommand();
                command.CommandText = $@"SELECT COUNT(*)
                FROM PHIEUMUON, CTPHIEUMUON
                WHERE MaDocGia = '{cbbReaderId.Text}' AND PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach AND TinhTrangPM = 0";
                numborrowedBooks = int.Parse(command.ExecuteScalar().ToString());
                lbBorrowed.Text = "Số sách đang mượn: " + numborrowedBooks;
            }
        }

        private void cbbReaderId_TextChanged(object sender, EventArgs e)
        {
            var cbb = (sender as ComboBox);
            bool existed = false;

            if (cbb.Text.Length == 0)
            {
                txtReaderName.Text = "";
                existed = true;
            }
            else
            {
                cbb.Text = cbb.Text.ToUpper();

                foreach (Reader reader in readers)
                    if (cbb.Text == reader.id)
                    {
                        existed = true;
                        break;
                    }
            }

            if (!existed)
                txtReaderName.Text = "";
            lbWCode.Visible = existed ? false : true;
            cbb.SelectionStart = cbb.Text.Length;
        }

        private void dtpBorrow_ValueChanged(object sender, EventArgs e)
        {
            dtpReturn.Value = dtpBorrow.Value.AddDays(maxDays);
        }

        private void toggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!toggleButton1.Checked)
                lbMaxBorrow.Text = "Số sách được mượn tối đa: Không";
            else
                lbMaxBorrow.Text = "Số sách được mượn tối đa: " + max;
        }

        private void toggleButton2_CheckedChanged(object sender, EventArgs e)
        {
            print = (toggleButton2.CheckState == CheckState.Checked) ? true : false;
        }

        public static DataTable ToDataTable<T>(BindingList<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (stockBooks.Count != 0)
            {
                if (txt.Text.Length == 0)
                    FillDataGridView(stockBooks, dtgvStock, bingdingStock);
                else
                {
                    try
                    {
                        var table = ToDataTable(stockBooks);
                        var rows = table.Select(string.Format("id LIKE '%{0}%' OR bookId LIKE '%{0}%' OR name LIKE '%{0}%' OR category LIKE '%{0}%' OR author LIKE '%{0}%'", txt.Text));

                        if (rows.Length > 0)
                        {
                            dtgvStock.DataSource = rows.CopyToDataTable();
                            dtgvStock.Refresh();
                        }
                        else
                        {
                            dtgvStock.DataSource = null;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (txt.TextLength == 0)
                FillDataGridView(stockBooks, dtgvStock, bingdingStock);
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ChangeBook(1);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dtgvChosen.Rows.Count > 0)
                ChangeBook(2);
            else
                MessageBox.Show("Danh sách sách đã chọn trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dtgvStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangeBook(1);
        }

        private void dtgvStock_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dtgv = sender as DataGridView;
            if (dtgv.SelectedRows.Count > 0)
                addRow = dtgv.CurrentCell.RowIndex;
            else
                addRow = -1;
        }

        private void dtgvChosen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangeBook(2);
        }

        private void dtgvChosen_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dtgv = sender as DataGridView;
            if (dtgv.SelectedRows.Count > 0)
                removeRow = dtgv.CurrentCell.RowIndex;
            else
                removeRow = -1;
        }

        private void btnBorrowList_Click(object sender, EventArgs e)
        {
            new FormDanhSachPM().ShowDialog();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (cbbReaderId.SelectedIndex == -1)
                MessageBox.Show("Vui lòng nhập mã độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (toggleButton1.CheckState == CheckState.Checked)
            {
                if (numborrowedBooks + dtgvChosen.Rows.Count > max)
                    MessageBox.Show($"Không được mượn quá {max} quyển sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    ShowConfirmForm();
            }
            else
                ShowConfirmForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //fHome.Switch(new FormMuonSach();
        }

        private void ShowConfirmForm()
        {
            string readerId = cbbReaderId.Text;
            string name = txtReaderName.Text;
            string date = dtpBorrow.Value.ToString("yyyy - MM - dd");
            string backDate = dtpReturn.Value.ToString("yyyy-MM-dd");
            string amount = dtgvChosen.Rows.Count.ToString();

            FormThongTinPM.borrowSlip = new BorrowSlip(newBorrowSlip, readerId, name, date, backDate, amount, chosenBooks);
            new FormThongTinPM().ShowDialog();

            if (borrowState == "Success")
            {
                MessageBox.Show("Mượn sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //fHome.Switch(new FormMuonSach();
                SendMail();
                LoadData();
                command = connection.CreateCommand();
                command.CommandText = $@"SELECT COUNT(*)
                FROM PHIEUMUON, CTPHIEUMUON
                WHERE MaDocGia = '{cbbReaderId.Text}' AND PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach AND TinhTrangPM = 0";
                numborrowedBooks = int.Parse(command.ExecuteScalar().ToString());
                lbBorrowed.Text = "Số sách đang mượn: " + numborrowedBooks;
                dtgvChosen.Rows.Clear();
                btnBorrow.Enabled = false;
                borrowState = "";
                lbAmount.Text = "Số lượng: 0";
            }
        }

        private void SendMail()
        {
            var sender = new MailAddress("20520824@gm.uit.edu.vn");
            command = connection.CreateCommand();
            command.CommandText = $"SELECT EMAIL FROM DOCGIA WHERE MADOCGIA = '{cbbReaderId.Text}'";
            var temp = command.ExecuteScalar().ToString();
            var recv = new MailAddress(temp);
            string pass = "Ngtrinh1";
            string subject = "[THƯ VIỆN] Xác nhận mượn sách thành công";
            string body = $"Chào {txtReaderName.Text},\n\nThư này dùng để xác nhận bạn đã mượn sách thành công ở thư viện chúng tôi. Bao gồm:\n";
            for (int i = 0; i < dtgvChosen.Rows.Count; i++)
            {
                body += $"          + Sách {dtgvChosen.Rows[i].Cells[2].Value} của tác giả {dtgvChosen.Rows[i].Cells[4].Value}\n";
            }
            body += $"\nHạn trả: {dtpReturn.Text}\n\nTrân trọng";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(sender.Address, pass);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sender.Address, "THƯ VIỆN");
            mail.To.Add(recv);
            mail.Subject = subject;
            mail.Body = body;

            smtp.Send(mail);
        }
    }
}
