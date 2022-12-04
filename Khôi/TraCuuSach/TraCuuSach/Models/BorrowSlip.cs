using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraCuuSach.Models
{
    public class BorrowSlip
    {
        public int stt { get; set; }
        public string slipCode { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string email;
        public string borrowDate { get; set; }
        public string returnDate { get; set; }
        public string amount;
        public BindingList<BookInfo> chosenBooks;

        public BorrowSlip()
        {
            slipCode = "";
            code = "";
            name = "";
            email = "";
            borrowDate = "";
            returnDate = "";
            amount = "";
            chosenBooks = new BindingList<BookInfo>();
        }
        public BorrowSlip(string slipCode, string code, string name, string email, string borrowDate, string returnDate, string amount, BindingList<BookInfo> selectedBooks)
        {
            this.slipCode = slipCode;
            this.code = code;
            this.name = name;
            this.email = email;
            this.borrowDate = borrowDate;
            this.returnDate = returnDate;
            this.amount = amount;
            chosenBooks = new BindingList<BookInfo>(selectedBooks);
        }
    }
}
