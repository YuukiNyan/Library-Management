using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TraCuuSach.Models
{
    public class BookInfo
    {
        public string id { get; set; }
        public string bookId { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string author { get; set; }

        public BookInfo()
        {
            id = "";
            bookId = "";
            name = "";
            category = "";
            author = "";
        }
        public BookInfo(BookInfo book)
        {
            this.id = book.id;
            this.bookId = book.bookId;
            this.name = book.name;
            this.category = book.category;
            this.author = book.author;
        }
        public BookInfo(string id, string bookId, string name, string cate, string author)
        {
            this.id = id;
            this.bookId = bookId;
            this.name = name;
            this.category = cate;
            this.author = author;
        }
    }
}
