using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TraCuuSach.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using DataTable = System.Data.DataTable;
using TextBox = System.Windows.Forms.TextBox;
using System.Security.Cryptography;

namespace TraCuuSach
{
    public partial class FormMuonSach : Form
    {
        SqlConnection connection;
        SqlCommand command;
        public static string str = @"Data Source=LAPTOP-RDTT4402;Initial Catalog=QLTV1;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();

        List<Reader> readers;
        BindingList<Book> stockBooks;
        BindingList<Book> chosenBooks;

        BindingSource bingdingStock;
        BindingSource bingdingChosen;

        int max;
        int maxDays;
        int addRow;
        int removeRow;
        int numborrowedBooks = -1;
        string newBorrowSlip;

        public static string borrowState = "";
        public static bool askBeforePrint = true;
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

            connection = new SqlConnection(str);
            connection.Open();

            readers = new List<Reader>();
            stockBooks = new BindingList<Book>();
            chosenBooks = new BindingList<Book>();

            LoadData();

            //Parameters.LoadParam();
            dtpReturn.Value = dtpBorrow.Value.AddDays(maxDays);
            lbMaxBorrow.Text = "Số sách được mượn tối đa: " + max;
            lbAmount.Text = "Số lượng: " + dtgvChosen.Rows.Count;
        }

        private void LoadData()
        {
            //Get reader list and load combobox
            command = connection.CreateCommand();
            command.CommandText = "select * from DOCGIA";
            adapter.SelectCommand = command;
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                Reader r = new Reader(item[0].ToString(), item[1].ToString(), item[2].ToString(), DateTime.Parse(item[3].ToString()), item[4].ToString(), item[5].ToString(), DateTime.Parse(item[6].ToString()), DateTime.Parse(item[7].ToString()), float.Parse(item[8].ToString()));
                readers.Add(r);
                cbbReaderId.Items.Add(r.id);
            }
            cbbReaderId.DisplayMember = "MaDocGia";
            cbbReaderId.SelectedIndex = -1;

            //Get book in stock and fill datagridview
            command = connection.CreateCommand();
            command.CommandText = @"SELECT DISTINCT MaCuonSach, CUONSACH.MaSach, TenDauSach, TenTheLoai, TenTacGia
            FROM SACH, DAUSACH, CUONSACH, THELOAI, CTTACGIA, TACGIA
            WHERE SACH.MaDauSach = DAUSACH.MaDauSach AND DAUSACH.MaTheLoai = THELOAI.MaTheLoai
            AND DAUSACH.MaDauSach = CTTACGIA.MaDauSach AND CTTACGIA.MaTacGia = TACGIA.MaTacGia
            AND SACH.MaSach = CUONSACH.MaSach AND TinhTrang = 0
			AND CUONSACH.MaCuonSach = (SELECT MAX(B.MaCuonSach)
				FROM CUONSACH B
				WHERE B.MaSach = CUONSACH.MaSach AND B. TinhTrang = 0)
			ORDER BY CUONSACH.MaSach";
            adapter.SelectCommand = command;
            table.Reset();
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                Book b = new Book(item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString());
                stockBooks.Add(b);
            }
            bingdingStock = new BindingSource();
            bingdingChosen = new BindingSource();
            LoadDataGridView(stockBooks, dtgvStock, bingdingStock);

            //Get max books can borrow
            command = connection.CreateCommand();
            command.CommandText = @"select SoSachMuonMax from THAMSO";
            adapter.SelectCommand = command;
            max = int.Parse(command.ExecuteScalar().ToString());


            //Get max days can borrow
            command = connection.CreateCommand();
            command.CommandText = @"select SoNgayMuonMax from THAMSO";
            adapter.SelectCommand = command;
            maxDays = int.Parse(command.ExecuteScalar().ToString());

            //Get the last borrow slip
            command = connection.CreateCommand();
            command.CommandText = @"SELECT TOP(1) MAPHIEUMUONSACH
            FROM phieumuon
            ORDER BY maphieumuonsach DESC";
            adapter.SelectCommand = command;
            string last = command.ExecuteScalar().ToString();
            int stt = int.Parse(last.Substring(4, 3)) + 1;
            newBorrowSlip = $"MPMS{stt:000}";
        }

        private void LoadDataGridView(BindingList<Book> list, DataGridView dtgv, BindingSource bd)
        {
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

        private void AddBook()
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
                LoadDataGridView(chosenBooks, dtgvChosen, bingdingChosen);
                LoadDataGridView(stockBooks, dtgvStock, bingdingStock);
            }
        }

        private void RemoveBook()
        {
            if (removeRow < 0)
                MessageBox.Show("Vui lòng chọn sách cần bỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (dtgvChosen.Rows.Count > 0)
            {
                ChangeBookBetweenTwoList(chosenBooks, stockBooks, removeRow);
                LoadDataGridView(chosenBooks, dtgvChosen, bingdingChosen);
                LoadDataGridView(stockBooks, dtgvStock, bingdingStock);
            }
        }

        private void ChangeBookBetweenTwoList(BindingList<Book> l1, BindingList<Book> l2, int i)
        {
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
        }

        private void cbbReaderId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbb = (sender as ComboBox);
            numborrowedBooks = -1;

            if (cbb.SelectedIndex != -1)
            {
                txtReaderName.Text = readers[cbbReaderId.SelectedIndex].name;

                command = connection.CreateCommand();
                command.CommandText = $@"SELECT count(*)
                FROM PHIEUMUON, CTPHIEUMUON
                WHERE MaDocGia = '{cbbReaderId.Text}' AND PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach AND TinhTrangPM = 1";
                adapter.SelectCommand = command;
                numborrowedBooks = int.Parse(command.ExecuteScalar().ToString());
                lbBorrowed.Text = "Số sách đang mượn: " + numborrowedBooks;

                dtpBorrow.Text = DateTime.Now.ToString();
            }
            else
                txtReaderName.Text = "";
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

            lbWCode.Visible = existed ? false : true;
            dtpBorrow.Value = existed ? DateTime.Now : dtpBorrow.Value;
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
            askBeforePrint = (toggleButton2.CheckState == CheckState.Checked) ? true : false;
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

        private void txbSearchBook_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (stockBooks.Count != 0)
            {
                if (txt.Text.Length == 0)
                    LoadDataGridView(stockBooks, dtgvStock, bingdingStock);
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
                LoadDataGridView(stockBooks, dtgvStock, bingdingStock);
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
            AddBook();
        }

        private void dtgvStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddBook();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveBook();
        }

        private void dtgvStock_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dtgv = sender as DataGridView;
            if (dtgv.SelectedRows.Count > 0)
                addRow = dtgv.CurrentCell.RowIndex;
            else
                addRow = -1;
        }

        private void dtgvChosen_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dtgv = sender as DataGridView;
            if (dtgv.SelectedRows.Count > 0)
                removeRow = dtgv.CurrentCell.RowIndex;
            else
                removeRow = -1;
        }

        private void dtgvChosen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RemoveBook();
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
            }
            else
                ShowConfirmForm();

            lbBorrowed.Text = "Số sách đang mượn: " + (numborrowedBooks + dtgvChosen.Rows.Count);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //    LibraryManagement.fHome.SwitchForm(new LendBook());

        }

        //private void CheckBorrowed()
        //{
        //    List<string> borrowedBooks = new List<string>();
        //    List<string> existedBooks = new List<string>();

        //    command = connection.CreateCommand();
        //    command.CommandText = $@"SELECT CTPHIEUMUON.MaCuonSach
        //    FROM PHIEUMUON, CTPHIEUMUON
        //    WHERE PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach AND TinhTrangPM = 0 
        //    AND MaDocGia = '{cbbReaderId.Text}'";
        //    adapter.SelectCommand = command;
        //    DataTable table = new DataTable();
        //    adapter.Fill(table);
        //    foreach (DataRow item in table.Rows)
        //        borrowedBooks.Add(item[0].ToString());

        //    for (int i = 0; i < dtgvChosen.Rows.Count; i++)
        //        for (int j = 0; j < borrowedBooks.Count; j++)
        //            if (dtgvChosen.Rows[i].Cells[1].Value.ToString() == borrowedBooks[j].ToString())
        //                existedBooks.Add(dtgvChosen.Rows[i].Cells[3].Value.ToString());
        //    var msg = "";
        //    if (existedBooks.Count > 0)
        //    {
        //        msg = string.Join(Environment.NewLine, existedBooks);
        //        MessageBox.Show($"Độc giả đã mượn:\n" + msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

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
                MessageBox.Show("Cho mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBorrow.Enabled = false;
                borrowState = "";
            }
        }
    }
}