using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.Models
{
    public class DetailBorrowSlip
    {
        public string id { get; set; }
        public string slipId { get; set; }
        public string bookId { get; set; }
        public string bookName { get; set; }
        public string status { get; set; }

        public DetailBorrowSlip() { }

        public DetailBorrowSlip(string id, string slipId, string bookId, string bookName, string status)
        {
            this.id = id;
            this.slipId = slipId;
            this.bookId = bookId;
            this.bookName = bookName;
            this.status = status;
        }
    }
}
