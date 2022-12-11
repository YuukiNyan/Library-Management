﻿using MuonTraSach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuonTraSach
{
    public partial class FormTraSach : Form
    {
        SqlConnection connection;
        SqlCommand command;

        List<Reader> readers;
        BindingList<ReturnBook> borrowBooks;
        BindingList<ReturnBook> chosenBooks;

        BindingSource bingdingBorrow;
        BindingSource bingdingChosen;

        int maxDays;
        long finePerDay;
        int addRow = -1, removeRow = -1;
        public static bool print = true;
        string newReturnSlip;

        public static string returnState = "";

        public FormTraSach()
        {
            InitializeComponent();
        }

        private void FormTraSach_Load(object sender, EventArgs e)
        {

            this.dtgvBorrow.AutoGenerateColumns = false;
            this.dtgvChosen.AutoGenerateColumns = false;
            btnReturnList.BorderRadius = 12;
            btnReturn.BorderRadius = 12;
            btnUpdate.BorderRadius = 12;
            btnAdd.BorderRadius = 15;
            btnRemove.BorderRadius = 15;
            btnReturn.Enabled = false;
            txtReaderName.Enabled = false;

            connection = new SqlConnection(FormMuonSach.str);
            connection.Open();
            readers = new List<Reader>();
            borrowBooks = new BindingList<ReturnBook>();
            chosenBooks = new BindingList<ReturnBook>();
            bingdingBorrow = new BindingSource();
            bingdingChosen = new BindingSource();

            LoadData();
        }

        private void LoadData()
        {
            //Get reader list and load combobox
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM DOCGIA";
            using (SqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                {
                    Reader r = new Reader(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.GetString(4), reader.GetString(5), reader.GetDateTime(6).ToString("dd/MM/yyyy"), reader.GetDateTime(7).ToString("dd/MM/yyyy"), (long)reader.GetSqlMoney(8));
                    readers.Add(r);
                    cbbReaderId.Items.Add(r.id);
                }
            cbbReaderId.DisplayMember = "MaDocGia";
            cbbReaderId.SelectedIndex = -1;

            //Get max days can borrow
            command = connection.CreateCommand();
            command.CommandText = @"SELECT SoNgayMuonMax FROM THAMSO";
            maxDays = int.Parse(command.ExecuteScalar().ToString());

            //Get fine per day for late
            command = connection.CreateCommand();
            command.CommandText = @"SELECT MucThuTienPhat FROM THAMSO";
            using (SqlDataReader reader = command.ExecuteReader())
                while (reader.Read())
                    finePerDay = (long)reader.GetSqlMoney(0);

            //Get the last return slip
            command = connection.CreateCommand();
            command.CommandText = @"SELECT TOP (1) MAPHIEUTRASACH
            FROM PHIEUTRASACH
            ORDER BY MAPHIEUTRASACH DESC";
            string last = command.ExecuteScalar().ToString();
            int stt = int.Parse(last.Substring(4, 3)) + 1;
            newReturnSlip = $"MPTS{stt:000}";
        }

        private void LoadDataGridView(BindingList<ReturnBook> list, DataGridView dtgv, BindingSource bd)
        {
            bd.DataSource = list;
            dtgv.DataSource = bd;

            if (dtgv.Rows.Count != 0)
                dtgv.ClearSelection();

            if (dtgvChosen.Rows.Count > 0)
                btnReturn.Enabled = true;
            else
                btnReturn.Enabled = false;
        }

        private long CalculateFine(int days)
        {
            long fine = finePerDay * days;
            return fine;
        }

        private void ChangeBook(int opt)
        {
            //1: Add; 2: Remove
            if (opt == 1)
            {
                if (addRow < 0)
                    MessageBox.Show("Bạn chưa chọn cuốn sách cần thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    ChangeBookBetweenTwoList(borrowBooks, chosenBooks, addRow);
                    LoadDataGridView(chosenBooks, dtgvChosen, bingdingChosen);
                    LoadDataGridView(borrowBooks, dtgvBorrow, bingdingBorrow);
                }
            }
            else if (opt == 2)
            {
                if (removeRow < 0)
                    MessageBox.Show("Vui lòng chọn sách cần bỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (dtgvChosen.Rows.Count > 0)
                {
                    ChangeBookBetweenTwoList(chosenBooks, borrowBooks, removeRow);
                    LoadDataGridView(chosenBooks, dtgvChosen, bingdingChosen);
                    LoadDataGridView(borrowBooks, dtgvBorrow, bingdingBorrow);
                }
            }

            //Calculate total late days, fine this period and total fine of that reader
            int totalLateDays = 0;
            long fineThisPeriod = 0;
            foreach (ReturnBook b in chosenBooks)
            {
                totalLateDays += b.lateDays;
                fineThisPeriod += b.fine;
            }

            lbLateDays.Text = totalLateDays.ToString();
            txtFineThisPeriod.Text = fineThisPeriod.ToString();
            txbTotalFine.Text = (readers[cbbReaderId.SelectedIndex].debt + fineThisPeriod).ToString();
        }

        private void ChangeBookBetweenTwoList(BindingList<ReturnBook> l1, BindingList<ReturnBook> l2, int i)
        {
            {
                int j = 0;
                foreach (ReturnBook b in l1)
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
            if (cbb.SelectedIndex != -1)
            {
                //Get reader name by cbbReaderId
                txtReaderName.Text = readers[cbbReaderId.SelectedIndex].name;
                lbLateDays.Text = "0";
                txtFineThisPeriod.Text = "0";
                txbTotalFine.Text = readers[cbbReaderId.SelectedIndex].debt.ToString();

                //Get books were borrowed by cbbReaderId and fill datagridview
                dtgvBorrow.Rows.Clear();
                command = connection.CreateCommand();
                command.CommandText = $@"SELECT DISTINCT CTPHIEUMUON.MaPhieuMuonSach, MaChiTietPhieuMuon, CUONSACH.MaCuonSach, TenDauSach, NgMuon
                FROM PHIEUMUON, CTPHIEUMUON, SACH, CUONSACH, DAUSACH
                WHERE PHIEUMUON.MaPhieuMuonSach = CTPHIEUMUON.MaPhieuMuonSach 
                AND CTPHIEUMUON.MaCuonSach = CUONSACH.MaCuonSach
                AND CUONSACH.MaSach = SACH.MaSach 
                AND SACH.MaDauSach = DAUSACH.MaDauSach 
                AND TinhTrangPM = 0 AND MaDocGia='{cbbReaderId.Text}'";
                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                    {
                        ReturnBook b = new ReturnBook();
                        b.borrowSlipId = reader.GetString(0);
                        b.detailBorrowId = reader.GetString(1);
                        b.id = reader.GetString(2);
                        b.bookName = reader.GetString(3);
                        DateTime dt = reader.GetDateTime(4);
                        b.borrowDate = dt.ToString("dd/MM/yyyy");
                        b.borrowedDays = Math.Abs(dt.Subtract(DateTime.Now).Days);

                        if (b.borrowedDays > maxDays)
                        {
                            b.lateDays = b.borrowedDays - maxDays;
                            b.fine = CalculateFine(b.lateDays);
                        }
                        borrowBooks.Add(b);
                    }

                LoadDataGridView(borrowBooks, dtgvBorrow, bingdingBorrow);
                dtgvChosen.Rows.Clear();
            }
        }

        private void txtReaderName_TextChanged(object sender, EventArgs e)
        {
            var cbb = (sender as ComboBox);
            bool existed = false;

            if (cbb.Text.Length == 0)
            {
                txtReaderName.Text = "";
                dtgvBorrow.Rows.Clear();
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
            {
                dtgvBorrow.Rows.Clear();
                txtReaderName.Text = "";
            }
            lbWCode.Visible = existed ? false : true;
            cbb.SelectionStart = cbb.Text.Length;
        }

        private void dtpReturn_ValueChanged(object sender, EventArgs e)
        {
            var value = (sender as DateTimePicker).Value;
            List<DateTime> borrowDateList = new List<DateTime>();
            for (int i = 0; i < dtgvBorrow.Rows.Count; i++)
                borrowDateList.Add(DateTime.ParseExact(dtgvBorrow.Rows[i].Cells[2].Value.ToString(), "dd/MM/yyyy", null));

            DateTime max = borrowDateList[0];
            foreach (DateTime dt in borrowDateList)
                if (dt > max)
                    max = dt;
            if (value < max)
            {
                MessageBox.Show("Ngày trả không thể sớm hơn ngày mượn gần nhất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                (sender as DateTimePicker).Value = DateTime.Now;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ChangeBook(1);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ChangeBook(2);
        }
        private void toggleButton_CheckedChanged(object sender, EventArgs e)
        {
            print = (toggleButton.CheckState == CheckState.Checked) ? true : false;
        }

        private void btnReturnList_Click(object sender, EventArgs e)
        {
            //new FormDanhSachPT().ShowDialog();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (cbbReaderId.SelectedIndex == -1)
                MessageBox.Show("Vui lòng nhập mã độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                ShowConfirmForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //LibraryManagement.fHome.SwitchForm(new FormTraSach());
        }

        private void dtgvBorrow_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangeBook(1);
        }

        private void dtgvBorrow_SelectionChanged(object sender, EventArgs e)
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

        private void ShowConfirmForm()
        {
            string readerId = cbbReaderId.Text;
            string name = txtReaderName.Text;
            string date = dtpReturn.Value.ToString("yyyy - MM - dd");
            string totalFine = txbTotalFine.Text;
            string fine = txtFineThisPeriod.Text;

            //FormThongTinPT.returnSlip = new ReturnSlip(newReturnSlip, readerId, name, date, totalFine, fine, chosenBooks);
            //new FormThongTinPT().ShowDialog();

            if (returnState == "Success")
            {
                MessageBox.Show("Trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //LibraryManagement.fHome.SwitchForm(new FormTraSach());
                dtgvChosen.Rows.Clear();
                btnReturn.Enabled = false;
                returnState = "";
            }
        }
    }
}
