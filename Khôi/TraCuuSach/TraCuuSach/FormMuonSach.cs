using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TraCuuSach.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace TraCuuSach
{
    public partial class FormMuonSach : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=LAPTOP-RDTT4402;Initial Catalog=QLTV1;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();

        List<Reader> readers;
        List<BookInfo> stockBooks;
        List<BookInfo> chosenBooks;

        int max;
        int addRow;
        int removeRow;

        public FormMuonSach()
        {
            InitializeComponent();
        }

        private void FormMuonSach_Load(object sender, EventArgs e)
        {
            btnViewBorrowList.BorderRadius = 20;
            btnBorrow.BorderRadius = 20;
            btnUpdate.BorderRadius = 20;
            btnAdd.BorderRadius = 15;
            btnRemove.BorderRadius = 15;
            btnBorrow.Enabled = false;
            btnUpdate.Enabled = false;
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;

            connection = new SqlConnection(str);
            connection.Open();

            readers = new List<Reader>();
            stockBooks = new List<BookInfo>();
            chosenBooks = new List<BookInfo>();

            txtReaderName.Enabled = false;
            returnDate.Enabled = false;

            LoadData();

            //Parameters.LoadParam();
            returnDate.Value = borrowDate.Value.AddDays(5);
            lbMaxBorrow.Text = "Số sách được mượn tối đa: " + max;
            lbAmount.Text = "Số lượng: " + dtgvChosen.Rows.Count;
        }
        private void AddBook()
        {
            if (cbbReaderId.SelectedIndex == -1)
                MessageBox.Show($"Mã độc giả không hợp lệ!\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (addRow < 0)
                    MessageBox.Show($"Bạn chưa chọn cuốn sách cần thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (chosenBooks.Count + 1 > max)
                    MessageBox.Show("Không được mượn quá " + max + " cuốn sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //if ((chosenBooks.Count + numborrowedBooks + 1 > Parameters.maxBorrowBook) && (tgBtnAllowMax.CheckState == CheckState.Checked))

                //else if (CheckBorrowed())
                //{
                //    MessageBox.Show($"Độc giả đã mượn quyển sách này rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                else
                {
                    ChangeBookBetweenTwoList(stockBooks, chosenBooks, addRow);
                    LoadBooks(chosenBooks, dtgvChosen);
                    LoadBooks(stockBooks, dtgvStock);
                }
            }
        }

        private void RemoveBook()
        {
            if (removeRow < 0)
                MessageBox.Show("Vui lòng chọn sách cần bỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (cbbReaderId.SelectedIndex == -1)
                MessageBox.Show($"Mã độc giả không hợp lệ!\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (dtgvChosen.Rows.Count > 0)
            {
                ChangeBookBetweenTwoList(chosenBooks, stockBooks, removeRow);
                LoadBooks(chosenBooks, dtgvChosen);
                LoadBooks(stockBooks, dtgvStock);
            }
        }

        private void LoadData()
        {
            //Reader List
            command = connection.CreateCommand();
            command.CommandText = "select * from DOCGIA";
            adapter.SelectCommand = command;

            System.Data.DataTable table = new System.Data.DataTable();
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

            //Max Books
            command = connection.CreateCommand();
            command.CommandText = @"select SoSachMuonMax from THAMSO";
            adapter.SelectCommand = command;
            max = int.Parse(command.ExecuteScalar().ToString());

            Load_Combobox();
            LoadBooks(stockBooks, dtgvStock);
        }
        private void ChangeBookBetweenTwoList(List<BookInfo> l1, List<BookInfo> l2, int i)
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

        private void LoadBooks(List<BookInfo> list, DataGridView dtgv)
        {
            dtgv.Rows.Clear();
            foreach (BookInfo item in list)
            {
                dtgv.Rows.Add(new object[] { item.id, item.bookId, item.name, item.category, item.author });
            }

            if (dtgv.Rows.Count != 0)
                dtgv.ClearSelection();

            lbAmount.Text = "Số lượng: " + dtgvChosen.Rows.Count;
            if (dtgvChosen.Rows.Count > 0)
                btnBorrow.Enabled = true;
            else
                btnBorrow.Enabled = false;
        }

        private void Load_Combobox()
        {
            foreach (Reader item in readers)
            {
                cbbReaderId.Items.Add(item.id);
            }

            cbbReaderId.DisplayMember = "MaDocGia";
            cbbReaderId.SelectedIndex = -1;
        }

        private void cbbReaderId_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReaderName.Text = readers[cbbReaderId.SelectedIndex].name;
        }

        private void borrowDate_ValueChanged(object sender, EventArgs e)
        {
            //Parameters.LoadParam();
            returnDate.Value = borrowDate.Value.AddDays(5);
        }

        private void toggleButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!toggleButton1.Checked)
            {
                max = 0;
                lbMaxBorrow.Text = "Số sách được mượn tối đa: Không";
            }
            else
            {
                //Max Books
                command = connection.CreateCommand();
                command.CommandText = @"select SoSachMuonMax from THAMSO";
                adapter.SelectCommand = command;
                max = int.Parse(command.ExecuteScalar().ToString());
                lbMaxBorrow.Text = "Số sách được mượn tối đa: " + max;
            }
        }

        private void toggleButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dtgvStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txbSearchBook_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (txt.Text.Length == 0)
                LoadBooks(stockBooks, dtgvStock);
            else
            {
                List<BookInfo> search = new List<BookInfo>();

                command = connection.CreateCommand();
                command.CommandText = @"SELECT DISTINCT MaCuonSach, CUONSACH.MaSach, TenDauSach, TenTheLoai, TenTacGia
                     FROM SACH, DAUSACH, CUONSACH, THELOAI, CTTACGIA, TACGIA
                     WHERE SACH.MaDauSach = DAUSACH.MaDauSach AND DAUSACH.MaTheLoai = THELOAI.MaTheLoai
                     AND DAUSACH.MaDauSach = CTTACGIA.MaDauSach AND CTTACGIA.MaTacGia = TACGIA.MaTacGia
                     AND SACH.MaSach = CUONSACH.MaSach AND TinhTrang = 1
            AND CONCAT(MaCuonSach, CUONSACH.MaSach, TenDauSach, TenTheLoai, TenTacGia) LIKE '%" + txt.Text + "%'";
                adapter.SelectCommand = command;

                System.Data.DataTable table = new System.Data.DataTable();
                adapter.Fill(table);
                dtgvStock.Rows.Clear();
                foreach (DataRow item in table.Rows)
                    search.Add(new BookInfo(item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString()));

                LoadBooks(search, dtgvStock);
            }
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        private void txtReaderName_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0)
            {
                btnAdd.Enabled = true;
                btnRemove.Enabled = true;
            }
        }

        private void cbbReaderId_TextChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).Text.Length == 0)
            {
                btnAdd.Enabled = false;
                btnRemove.Enabled = false;
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

