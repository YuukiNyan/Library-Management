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

//string slipCode = "";
//string readerName = "";
//bool lockReaderName = true;
//public static string lendState = "";
//public static bool askBeforePrint = true;

//Thread tdGetBookSlip;

//enum BtnOption
//{
//    ChooseBook,
//    UnchooseBook
//}

//public LendBook()
//{
//    CheckForIllegalCrossThreadCalls = false;
//    InitializeComponent();
//}

//private void btnAddRemove_Click(object sender, EventArgs e)
//{

//}

//private void GetSlipCode()
//{
//    SqlConnection conn = new SqlConnection(DatabaseInfo.connectionString);
//    conn.Open();
//    SqlCommand cmd = new SqlCommand(DatabaseInfo.getBookSlipCode, conn);
//    using (SqlDataReader reader = cmd.ExecuteReader())
//    {
//        while (reader.Read())
//        {
//            slipCode = reader.GetString(0);
//        }
//    }
//    conn.Close();
//    int stt = int.Parse(slipCode.Substring(4, 3)) + 1;
//    slipCode = $"MPMS{stt:000}";
//}

//private void btnChooseBook_Click(object sender, EventArgs e)
//{
//    if (cbbReaderCode.SelectedIndex == -1)
//    {
//        MessageBox.Show($"Vui lòng nhập mã độc giả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//    }
//    else
//    {
//        //try
//        {
//            Parameters.LoadParam();
//            LoadNumBorrowBooks();

//            if ((chosenBooks.Count + numborrowedBooks + 1 > Parameters.maxBorrowBook) && (tgBtnAllowMax.CheckState == CheckState.Checked))
//            {
//                MessageBox.Show($"Không được mượn quá {Parameters.maxBorrowBook} quyển sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//            else if (CheckBorrowed())
//            {
//                MessageBox.Show($"Độc giả đã mượn quyển sách này rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//            else
//            {
//                if (txbFindBook.Text.Length == 0)
//                {
//                    SelectFromNormalView();
//                }
//                else
//                {
//                    SelectFromFindingView();
//                }
//            }
//        }
//        //catch
//        //{
//        //    MessageBox.Show($"Vui lòng chọn 1 quyển sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //}
//    }
//}

//private bool CheckBorrowed()
//{
//    string bookCode;
//    bookCode = dtgvStock.SelectedRows[0].Cells[1].Value.ToString();

//    string queryCmd = $@"SELECT *
//                FROM PHIEUMUON, CTPHIEUMUON, CUONSACH
//                WHERE PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach AND TinhTrangPM = 0 
//		                AND MaDocGia = '{cbbReaderCode.Text}' AND CUONSACH.MaSach = '{bookCode}' AND CTPHIEUMUON.MaCuonSach = CUONSACH.MaCuonSach";
//    bool found = false;
//    Parameters.LoadParam();
//    SqlConnection conn = new SqlConnection(DatabaseInfo.connectionString);
//    conn.Open();
//    SqlCommand cmd = new SqlCommand(queryCmd, conn);
//    using (SqlDataReader reader = cmd.ExecuteReader())
//    {
//        while (reader.Read())
//        {
//            found = true;
//        }
//    }
//    conn.Close();

//    return found;
//}


//private void SelectFromFindingView()
//{
//    try
//    {
//        int stt = 1;
//        Book bookChosen = new Book();

//        bookChosen.stt = chosenBooks.Count + 1;
//        bookChosen.code = dtgvStock.SelectedRows[0].Cells[1].Value.ToString();
//        bookChosen.name = dtgvStock.SelectedRows[0].Cells[2].Value.ToString();
//        bookChosen.category = dtgvStock.SelectedRows[0].Cells[3].Value.ToString();
//        bookChosen.author = dtgvStock.SelectedRows[0].Cells[4].Value.ToString();
//        bookChosen.specCode = findBooks[dtgvStock.SelectedRows[0].Index].specCode;

//        chosenBooks.Add(bookChosen);

//        int index = 0;
//        foreach (Book book in stockBooks)
//        {
//            if (bookChosen.code == book.code)
//            {
//                break;
//            }
//            index++;
//        }
//        stockBooks.RemoveAt(index);
//        findBooks.RemoveAt(dtgvStock.SelectedRows[0].Index);

//        findBooks = findBooks.OrderBy(o => o.code).ThenBy(o => o.name).ToList();
//        chosenBooks = chosenBooks.OrderBy(o => o.code).ThenBy(o => o.name).ToList();

//        foreach (Book book in findBooks)
//        {
//            book.stt = stt;
//            stt++;
//        }

//        stt = 1;
//        foreach (Book book in chosenBooks)
//        {
//            book.stt = stt;
//            stt++;
//        }

//        stt = 1;
//        foreach (Book book in stockBooks)
//        {
//            book.stt = stt;
//            stt++;
//        }

//        bindingStock = new BindingSource();
//        bindingStock.DataSource = findBooks;
//        dtgvStock.DataSource = bindingStock;

//        bindingChosen = new BindingSource();
//        bindingChosen.DataSource = chosenBooks;
//        dtgvBookChosen.DataSource = bindingChosen;
//        //dtgvBookChosen.Update();
//        //dtgvBookChosen.Refresh();

//        lbAmount.Text = $"Số lượng: {chosenBooks.Count}";

//        if (dtgvStock.Rows.Count != 0)
//        {
//            dtgvStock.Rows[0].Selected = false;
//        }

//        foreach (DataGridViewRow row in dtgvBookChosen.Rows)
//        {
//            if (row.Cells[1].Value.ToString() == bookChosen.code)
//            {
//                dtgvBookChosen.Rows[row.Index].Selected = true;
//                break;
//            }
//        }
//    }
//    catch
//    {
//        MessageBox.Show($"Vui lòng chọn 1 quyển sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//    }
//}

//private void SelectFromNormalView()
//{
//    try
//    {
//        Book bookChosen = new Book();

//        bookChosen.stt = chosenBooks.Count + 1;
//        bookChosen.code = dtgvStock.SelectedRows[0].Cells[1].Value.ToString();
//        bookChosen.name = dtgvStock.SelectedRows[0].Cells[2].Value.ToString();
//        bookChosen.category = dtgvStock.SelectedRows[0].Cells[3].Value.ToString();
//        bookChosen.author = dtgvStock.SelectedRows[0].Cells[4].Value.ToString();
//        bookChosen.specCode = stockBooks[dtgvStock.SelectedRows[0].Index].specCode;

//        chosenBooks.Add(bookChosen);

//        UpdateData(BtnOption.ChooseBook);
//        if (dtgvStock.Rows.Count != 0)
//        {
//            dtgvStock.Rows[0].Selected = false;
//        }

//        foreach (DataGridViewRow row in dtgvBookChosen.Rows)
//        {
//            if (row.Cells[1].Value.ToString() == bookChosen.code)
//            {
//                dtgvBookChosen.Rows[row.Index].Selected = true;
//                break;
//            }
//        }
//    }
//    catch
//    {
//        MessageBox.Show($"Vui lòng chọn 1 quyển sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//    }
//}
//private void btnUnchooseBook_Click(object sender, EventArgs e)
//{
//    if (txbFindBook.Text == "")
//    {
//        DeselectFromNormalView();
//    }
//    else
//    {
//        DeselectFromFindingView();
//    }
//}

//private void DeselectFromFindingView()
//{
//    try
//    {
//        int stt = 1;
//        Book bookUnchosen = new Book();

//        bookUnchosen.stt = stockBooks.Count + 1;
//        bookUnchosen.code = dtgvBookChosen.SelectedRows[0].Cells[1].Value.ToString();
//        bookUnchosen.name = dtgvBookChosen.SelectedRows[0].Cells[2].Value.ToString();
//        bookUnchosen.category = dtgvBookChosen.SelectedRows[0].Cells[3].Value.ToString();
//        bookUnchosen.author = dtgvBookChosen.SelectedRows[0].Cells[4].Value.ToString();
//        bookUnchosen.specCode = chosenBooks[dtgvBookChosen.SelectedRows[0].Index].specCode;

//        findBooks.Add(bookUnchosen);
//        stockBooks.Add(bookUnchosen);
//        chosenBooks.RemoveAt(dtgvBookChosen.SelectedRows[0].Index);

//        findBooks = findBooks.OrderBy(o => o.code).ThenBy(o => o.name).ToList();
//        chosenBooks = chosenBooks.OrderBy(o => o.code).ThenBy(o => o.name).ToList();

//        foreach (Book book in findBooks)
//        {
//            book.stt = stt;
//            stt++;
//        }

//        stt = 1;
//        foreach (Book book in chosenBooks)
//        {
//            book.stt = stt;
//            stt++;
//        }

//        stt = 1;
//        foreach (Book book in stockBooks)
//        {
//            book.stt = stt;
//            stt++;
//        }

//        bindingStock = new BindingSource();
//        bindingStock.DataSource = findBooks;
//        dtgvStock.DataSource = bindingStock;

//        bindingChosen = new BindingSource();
//        bindingChosen.DataSource = chosenBooks;
//        dtgvBookChosen.DataSource = bindingChosen;
//        //dtgvBookChosen.Update();
//        //dtgvBookChosen.Refresh();

//        lbAmount.Text = $"Số lượng: {chosenBooks.Count}";
//        if (dtgvBookChosen.Rows.Count != 0)
//        {
//            dtgvBookChosen.Rows[0].Selected = false;
//        }
//        foreach (DataGridViewRow row in dtgvStock.Rows)
//        {
//            if (row.Cells[1].Value.ToString() == bookUnchosen.code)
//            {
//                dtgvStock.Rows[row.Index].Selected = true;
//                break;
//            }
//        }
//    }
//    catch
//    {
//        MessageBox.Show($"Vui lòng chọn 1 quyển sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//    }
//}

//private void DeselectFromNormalView()
//{
//    try
//    {
//        Book bookUnchosen = new Book();

//        bookUnchosen.stt = stockBooks.Count + 1;
//        bookUnchosen.code = dtgvBookChosen.SelectedRows[0].Cells[1].Value.ToString();
//        bookUnchosen.name = dtgvBookChosen.SelectedRows[0].Cells[2].Value.ToString();
//        bookUnchosen.category = dtgvBookChosen.SelectedRows[0].Cells[3].Value.ToString();
//        bookUnchosen.author = dtgvBookChosen.SelectedRows[0].Cells[4].Value.ToString();
//        bookUnchosen.specCode = chosenBooks[dtgvBookChosen.SelectedRows[0].Index].specCode;

//        stockBooks.Add(bookUnchosen);

//        UpdateData(BtnOption.UnchooseBook);
//        if (dtgvBookChosen.Rows.Count != 0)
//        {
//            dtgvBookChosen.Rows[0].Selected = false;
//        }
//        foreach (DataGridViewRow row in dtgvStock.Rows)
//        {
//            if (row.Cells[1].Value.ToString() == bookUnchosen.code)
//            {
//                dtgvStock.Rows[row.Index].Selected = true;
//                break;
//            }
//        }
//    }
//    catch
//    {
//        MessageBox.Show($"Vui lòng chọn 1 quyển sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//    }
//}

//private void UpdateData(BtnOption btnOption)
//{
//    int stt = 1;
//    if (btnOption == BtnOption.ChooseBook)
//    {
//        stockBooks.RemoveAt(dtgvStock.SelectedRows[0].Index);
//    }
//    else
//    {
//        chosenBooks.RemoveAt(dtgvBookChosen.SelectedRows[0].Index);
//    }

//    stockBooks = stockBooks.OrderBy(o => o.code).ThenBy(o => o.name).ToList();
//    chosenBooks = chosenBooks.OrderBy(o => o.code).ThenBy(o => o.name).ToList();

//    foreach (Book book in stockBooks)
//    {
//        book.stt = stt;
//        stt++;
//    }
//    stt = 1;
//    foreach (Book book in chosenBooks)
//    {
//        book.stt = stt;
//        stt++;
//    }

//    bindingStock = new BindingSource();
//    bindingStock.DataSource = stockBooks;
//    dtgvStock.DataSource = bindingStock;

//    bindingChosen = new BindingSource();
//    bindingChosen.DataSource = chosenBooks;
//    dtgvBookChosen.DataSource = bindingChosen;
//    //dtgvBookChosen.Update();
//    //dtgvBookChosen.Refresh();

//    lbAmount.Text = $"Số lượng: {chosenBooks.Count}";
//}

//private void cbbReaderCode_SelectedIndexChanged(object sender, EventArgs e)
//{
//    lockReaderName = false;
//    txbReaderName.Text = readers[cbbReaderCode.SelectedIndex].name;
//    lockReaderName = true;
//}

//private void txbReaderName_MouseDown(object sender, MouseEventArgs e)
//{
//    readerName = txbReaderName.Text.ToString();
//}

//private void returnDay_MouseDown(object sender, MouseEventArgs e)
//{
//    this.ActiveControl = null;
//}

//private void txbReaderName_TextChanged(object sender, EventArgs e)
//{
//    if (lockReaderName)
//    {
//        txbReaderName.Text = readerName;
//    }
//}

//private void cbbReaderCode_TextChanged(object sender, EventArgs e)
//{
//    if (cbbReaderCode.Text != "")
//    {
//        ComboBox cbb = (ComboBox)sender;
//        string text = cbb.Text.ToString();
//        cbb.Text = text.ToUpper();
//        cbb.Select(cbb.Text.Length, 0);

//        string readerCode = cbbReaderCode.Text;
//        bool isFound = false;
//        foreach (Reader reader in readers)
//        {
//            if (readerCode == reader.code)
//            {
//                isFound = true;
//                break;
//            }
//        }
//        lbWCode.Visible = (isFound) ? false : true;
//    }
//}

//private void txbFindBook_TextChanged(object sender, EventArgs e)
//{
//    TextBox txb = (TextBox)sender;
//    if (txb.Text.Length == 0)
//    {
//        bindingStock = new BindingSource();
//        bindingStock.DataSource = stockBooks;
//        dtgvStock.DataSource = bindingStock;
//    }
//    else
//    {
//        String findText = txb.Text;
//        String code, name, category, author;
//        findText = RemoveUnicode(findText.ToLower());


//        findBooks.Clear();
//        int stt = 1;
//        bool found = false;
//        foreach (Book book in stockBooks)
//        {
//            found = false;

//            code = RemoveUnicode(book.code.ToLower());
//            name = RemoveUnicode(book.name.ToLower());
//            category = RemoveUnicode(book.category.ToLower());
//            author = RemoveUnicode(book.author.ToLower());

//            if (code.Contains(findText) || name.Contains(findText) || category.Contains(findText) || author.Contains(findText))
//            {
//                found = true;
//            }
//            else if (book.code.ToLower().Contains(findText) || book.name.ToLower().Contains(findText) || book.category.ToLower().Contains(findText) || book.author.ToLower().Contains(findText))
//            {
//                found = true;
//            }
//            if (found)
//            {
//                Book findBook = new Book(book);
//                findBook.stt = stt;
//                stt++;
//                findBooks.Add(findBook);
//                findBooks = findBooks.OrderBy(o => o.code).ThenBy(o => o.name).ToList();
//            }
//        }

//        bindingStock = new BindingSource();
//        bindingStock.DataSource = findBooks;
//        dtgvStock.DataSource = bindingStock;
//        if (dtgvStock.Rows.Count != 0)
//        {
//            dtgvStock.Rows[0].Selected = false;
//        }
//    }
//}

//public enum Valid
//{
//    Valid,
//    MissingCode,
//    MissingBook,
//    BorrowedMax,
//    LendDayPast,
//    Borrowed
//}

//private void btnLend_Click(object sender, EventArgs e)
//{
//    switch (IsValid())
//    {
//        case Valid.Valid:
//            {
//                ShowConfirmForm();
//                break;
//            }
//        case Valid.MissingCode:
//            {
//                MessageBox.Show("Vui lòng nhập mã độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                break;
//            }
//        case Valid.MissingBook:
//            {
//                MessageBox.Show("Vui chọn 1 quyển sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                break;
//            }
//        case Valid.BorrowedMax:
//            {
//                MessageBox.Show($"Không được mượn quá {Parameters.maxBorrowBook} quyển sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                break;
//            }
//        case Valid.Borrowed:
//            {

//                break;
//            }
//    }
//}

//private Valid IsValid()
//{
//    if (cbbReaderCode.SelectedIndex == -1)
//    {
//        return Valid.MissingCode;
//    }
//    else if (dtgvBookChosen.Rows.Count == 0)
//    {
//        return Valid.MissingBook;
//    }
//    else
//    {
//        if (tgBtnAllowMax.CheckState == CheckState.Checked)
//        {
//            Parameters.LoadParam();
//            SqlConnection conn = new SqlConnection(DatabaseInfo.connectionString);
//            conn.Open();
//            SqlCommand cmd = new SqlCommand(DatabaseInfo.GetNumOfBooksBorrowed(cbbReaderCode.Text), conn);
//            using (SqlDataReader reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    numborrowedBooks = reader.GetInt32(0);
//                }
//            }
//            conn.Close();

//            if (numborrowedBooks + dtgvBookChosen.Rows.Count > Parameters.maxBorrowBook)
//            {
//                return Valid.BorrowedMax;
//            }
//        }
//    }

//    string msg = "";
//    Parameters.LoadParam();
//    SqlConnection conn1 = new SqlConnection(DatabaseInfo.connectionString);
//    conn1.Open();
//    SqlCommand cmd1;
//    string queryCmd = "";
//    foreach (Book book in chosenBooks)
//    {
//        queryCmd = $@"SELECT *
//                FROM PHIEUMUON, CTPHIEUMUON, CUONSACH
//                WHERE PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach AND TinhTrangPM = 0 
//		                AND MaDocGia = '{cbbReaderCode.Text}' AND CUONSACH.MaSach = '{book.code}' AND CTPHIEUMUON.MaCuonSach = CUONSACH.MaCuonSach";

//        cmd1 = new SqlCommand(queryCmd, conn1);
//        using (SqlDataReader reader = cmd1.ExecuteReader())
//        {
//            while (reader.Read())
//            {
//                msg += book.code + " ";
//            }
//        }
//    }
//    conn1.Close();
//    if (msg != "")
//    {
//        MessageBox.Show("Độc giả này đã mượn " + msg + "rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        return Valid.Borrowed;
//    }

//    return Valid.Valid;
//}

//private void ShowConfirmForm()
//{
//    tdGetBookSlip.Join();
//    string code = cbbReaderCode.Text.ToString();
//    string name = txbReaderName.Text.ToString();
//    string email = readers[cbbReaderCode.SelectedIndex].email;
//    string lendDate = borrowDate.Value.ToString("yyyy-MM-dd");
//    string backDate = returnDate.Value.ToString("yyyy-MM-dd");
//    string amount = chosenBooks.Count.ToString();

//    ConfirmLendBook.borrowSlip = new BorrowSlip(slipCode, code, name, email, lendDate, backDate, amount, chosenBooks);
//    new ConfirmLendBook().ShowDialog();

//    if (lendState == "Success")
//    {
//        MessageBox.Show("Cho mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

//        btnLend.Enabled = false;

//        //chosenBooks.Clear();
//        //bindingChosen = new BindingSource();
//        //bindingChosen.DataSource = chosenBooks;
//        //dtgvBookChosen.DataSource = bindingChosen;

//        lendState = "";
//        tdGetBookSlip = new Thread(new ThreadStart(GetSlipCode));
//        tdGetBookSlip.Start();
//        LibraryManagement.fHome.SwitchForm(new LendBook());
//    }
//    //String returnDay = 
//}

//private void btnCancel_Click(object sender, EventArgs e)
//{
//    ShowConfirmForm();
//}

//private void tgBtnAskBeforePrint_CheckedChanged(object sender, EventArgs e)
//{
//    askBeforePrint = (tgBtnAskBeforePrint.CheckState == CheckState.Checked) ? true : false;
//}

//private void btnViewBorrowSlip_Click(object sender, EventArgs e)
//{
//    new ViewBorrowSlip().ShowDialog();
//}

//private void btnCan_Click(object sender, EventArgs e)
//{
//    LibraryManagement.fHome.SwitchForm(new LendBook());
//}
//    }