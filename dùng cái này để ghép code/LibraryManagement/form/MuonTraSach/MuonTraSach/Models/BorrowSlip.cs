using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.Models
{
    public class BorrowSlip
    {
        public string id { get; set; }
        public string readerId { get; set; }
        public string readerName { get; set; }
        public string borrowDate { get; set; }
        public string returnDate { get; set; }
        public string amount { get; set; }
        public BindingList<Book> chosenBooks;

        public BorrowSlip()
        {
            id = "";
            readerId = "";
            readerName = "";
            borrowDate = "";
            returnDate = "";
            amount = "";
            chosenBooks = new BindingList<Book>();
        }
        public BorrowSlip(string slipId, string readerId, string readerName, string borrowDate, string returnDate, string am, BindingList<Book> selectedBooks)
        {
            this.id = slipId;
            this.readerId = readerId;
            this.readerName = readerName;
            this.borrowDate = borrowDate;
            this.returnDate = returnDate;
            this.amount = am;
            chosenBooks = new BindingList<Book>(selectedBooks);
        }
    }
}
