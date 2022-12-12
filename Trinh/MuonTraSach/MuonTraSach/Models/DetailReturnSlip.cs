using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.Models
{
    public class DetailReturnSlip
    {
        public string id { get; set; }
        public string slipId { get; set; }
        public string bookId { get; set; }
        public string bookName { get; set; }
        public string borrowDetailSlip { get; set; }
        public int borrowDays { get; set; }
        public long fine { get; set; }

        public DetailReturnSlip() { }

        public DetailReturnSlip(string id, string slipId, string bookId, string bookName, int borrowDays, long fine)
        {
            this.id = id;
            this.slipId = slipId;
            this.bookId = bookId;
            this.bookName = bookName;
            this.borrowDays = borrowDays;
            this.fine = fine;
        }
    }
}
