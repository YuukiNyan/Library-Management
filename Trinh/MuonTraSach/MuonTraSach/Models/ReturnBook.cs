using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.Models
{
    public class ReturnBook : INotifyPropertyChanged
    {
        [System.ComponentModel.DisplayName("Mã c.sách")]
        public string id { get; set; }
        [System.ComponentModel.DisplayName("Tên sách")]
        public string bookName { get; set; }
        [System.ComponentModel.DisplayName("Ngày mượn")]
        public string borrowDate { get; set; }
        [System.ComponentModel.DisplayName("Số ngày mượn")]
        public int borrowedDays { get; set; }
        public string borrowSlipId { get; set; }
        public string detailBorrowId { get; set; }
        public long fine { get; set; }
        public int lateDays { get; set; }

        public ReturnBook() { }
        public ReturnBook(string id, string bookName, string borrowDate, int borrowedDays, long fine)
        {
            this.id = id;
            this.bookName = bookName;
            this.borrowDate = borrowDate;
            this.borrowedDays = borrowedDays;
            this.fine = fine;
        }
        public ReturnBook(ReturnBook returnBook)
        {
            this.id = returnBook.id;
            this.bookName = returnBook.bookName;
            this.borrowDate = returnBook.borrowDate;
            this.borrowedDays = returnBook.borrowedDays;
            this.fine = returnBook.fine;
        }

        public string GetDetailBorrowId(string borrowId, string bookId)
        {
            SqlConnection connection = new SqlConnection(FormMuonSach.stringConnect);
            SqlCommand command = new SqlCommand();
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $@"SELECT MaChiTietPhieuMuon FROM CTPHIEUMUON WHERE MaPhieuMuonSach = '{borrowId}' AND MaCuonSach = '{bookId}'";
            return command.ExecuteScalar().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
