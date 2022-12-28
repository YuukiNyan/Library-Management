using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.Models
{
    public class Book : INotifyPropertyChanged
    {
        [System.ComponentModel.DisplayName("Mã cuốn")]
        public string id { get; set; }
        [System.ComponentModel.DisplayName("Mã sách")]
        public string bookId { get; set; }
        [System.ComponentModel.DisplayName("Tên sách")]
        public string name { get; set; }
        [System.ComponentModel.DisplayName("Thể loại")]
        public string category { get; set; }
        [System.ComponentModel.DisplayName("Tác giả")]
        public string author { get; set; }

        public Book()
        {
            id = "";
            bookId = "";
            name = "";
            category = "";
            author = "";
        }
        public Book(Book book)
        {
            this.id = book.id;
            this.bookId = book.bookId;
            this.name = book.name;
            this.category = book.category;
            this.author = book.author;
        }
        public Book(string id, string bookId, string name, string cate, string author)
        {
            this.id = id;
            this.bookId = bookId;
            this.name = name;
            this.category = cate;
            this.author = author;
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
