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
using System.Reflection;

namespace TraCuuSach
{
    public partial class FormMuonSach : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=LAPTOP-RDTT4402;Initial Catalog=QLTV1;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();

        List<Reader> readers;
        BindingList<BookInfo> stockBooks;
        BindingList<BookInfo> chosenBooks;

        BindingSource bingdingStock;
        BindingSource bingdingChosen;

        int max;
        int maxDays;
        int addRow;
        int removeRow;
        int numborrowedBooks = -1;

        public FormMuonSach()
        {
            InitializeComponent();
        }

        private void FormMuonSach_Load(object sender, EventArgs e)
        {
            this.dtgvStock.AutoGenerateColumns = false;
            btnViewBorrowList.BorderRadius = 20;
            btnBorrow.BorderRadius = 20;
            btnUpdate.BorderRadius = 20;
            btnAdd.BorderRadius = 15;
            btnRemove.BorderRadius = 15;
            btnBorrow.Enabled = false;
            btnUpdate.Enabled = false;

            connection = new SqlConnection(str);
            connection.Open();

            readers = new List<Reader>();
            stockBooks = new BindingList<BookInfo>();
            chosenBooks = new BindingList<BookInfo>();

            txtReaderName.Enabled = false;
            returnDate.Enabled = false;

            LoadData();

            //Parameters.LoadParam();
            returnDate.Value = borrowDate.Value.AddDays(maxDays);
            lbMaxBorrow.Text = "Số sách được mượn tối đa: " + max;
            lbAmount.Text = "Số lượng: " + dtgvChosen.Rows.Count;
        }

        private void LoadData()
        {
            //Reader List
            command = connection.CreateCommand();
            command.CommandText = "select * from DOCGIA";
            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow item in table.Rows)
            {
                Reader r = new Reader(item[0].ToString(), item[1].ToString(), item[2].ToString(), DateTime.Parse(item[3].ToString()), item[4].ToString(), item[5].ToString(), DateTime.Parse(item[6].ToString()), DateTime.Parse(item[7].ToString()), float.Parse(item[8].ToString()));
                readers.Add(r);
            }

            //Datagridview
            command = connection.CreateCommand();
            command.CommandText = @"SELECT DISTINCT MaCuonSach, CUONSACH.MaSach, TenDauSach, TenTheLoai, TenTacGia
            FROM SACH, DAUSACH, CUONSACH, THELOAI, CTTACGIA, TACGIA
            WHERE SACH.MaDauSach = DAUSACH.MaDauSach AND DAUSACH.MaTheLoai = THELOAI.MaTheLoai
            AND DAUSACH.MaDauSach = CTTACGIA.MaDauSach AND CTTACGIA.MaTacGia = TACGIA.MaTacGia
            AND SACH.MaSach = CUONSACH.MaSach AND TinhTrang = 1
			AND CUONSACH.MaCuonSach = (SELECT MAX(B.MaCuonSach)
				FROM CUONSACH B
				WHERE B.MaSach = CUONSACH.MaSach AND B. TinhTrang = 1)
			ORDER BY CUONSACH.MaSach";
            adapter.SelectCommand = command;

            table.Reset();
            adapter.Fill(table);

            foreach (DataRow item in table.Rows)
            {
                BookInfo b = new BookInfo(item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString());
                stockBooks.Add(b);
            }
            bingdingStock = new BindingSource();
            bingdingChosen = new BindingSource();

            //Max Books
            command = connection.CreateCommand();
            command.CommandText = @"select SoSachMuonMax from THAMSO";
            adapter.SelectCommand = command;
            max = int.Parse(command.ExecuteScalar().ToString());


            //Max Days
            command = connection.CreateCommand();
            command.CommandText = @"select SoNgayMuonMax from THAMSO";
            adapter.SelectCommand = command;
            maxDays = int.Parse(command.ExecuteScalar().ToString());

            LoadCombobox();
            LoadDatagridview(stockBooks, dtgvStock, bingdingStock);
        }

        private void LoadCombobox()
        {
            foreach (Reader item in readers)
            {
                cbbReaderId.Items.Add(item.id);
            }

            cbbReaderId.DisplayMember = "MaDocGia";
            cbbReaderId.SelectedIndex = -1;
        }

        private void LoadDatagridview(BindingList<BookInfo> list, DataGridView dtgv, BindingSource bd)
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

            //else if (CheckBorrowed())
            //    MessageBox.Show($"Độc giả đã mượn quyển sách này rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                ChangeBookBetweenTwoList(stockBooks, chosenBooks, addRow);
                LoadDatagridview(chosenBooks, dtgvChosen, bingdingChosen);
                LoadDatagridview(stockBooks, dtgvStock, bingdingStock);
            }
        }

        private void RemoveBook()
        {
            if (removeRow < 0)
                MessageBox.Show("Vui lòng chọn sách cần bỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (dtgvChosen.Rows.Count > 0)
            {
                ChangeBookBetweenTwoList(chosenBooks, stockBooks, removeRow);
                LoadDatagridview(chosenBooks, dtgvChosen, bingdingChosen);
                LoadDatagridview(stockBooks, dtgvStock, bingdingStock);
            }
        }

        private void ChangeBookBetweenTwoList(BindingList<BookInfo> l1, BindingList<BookInfo> l2, int i)
        {
            {
                int j = 0;
                foreach (BookInfo b in l1)
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
            numborrowedBooks = -1;

            if (cbbReaderId.SelectedIndex != -1)
            {
                txtReaderName.Text = readers[cbbReaderId.SelectedIndex].name;

                command = connection.CreateCommand();
                command.CommandText = $@"SELECT count(*)
                FROM PHIEUMUON, CTPHIEUMUON
                WHERE MaDocGia = '{cbbReaderId.Text}' AND PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach AND TinhTrangPM = 0";
                adapter.SelectCommand = command;
                numborrowedBooks = int.Parse(command.ExecuteScalar().ToString());
            }
            else
                txtReaderName.Text = "";
        }

        private void borrowDate_ValueChanged(object sender, EventArgs e)
        {
            returnDate.Value = borrowDate.Value.AddDays(maxDays);
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
                    LoadDatagridview(stockBooks, dtgvStock, bingdingStock);
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
                LoadDatagridview(stockBooks, dtgvStock, bingdingStock);
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        private void cbbReaderId_TextChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.Length == 0)
            {
                txtReaderName.Text = "";
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

        private void btnBorrow_Click(object sender, EventArgs e)
        {

        }
    }
}
