using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.Models
{
    public class ReturnSlip
    {
        public string id { get; set; }
        public string readerId { get; set; }
        public string readerName { get; set; }
        public string returnDate { get; set; }
        public int lateDays { get; set; }
        public long fineThisPeriod { get; set; }
        public string totalFine { get; set; }
        public string borrowSlipId { get; set; }
        public BindingList<ReturnBook> returnBooks;

        public ReturnSlip() { returnBooks = new BindingList<ReturnBook>(); }

        public ReturnSlip(string slipId, string readerId, string readerName, string returnDate, string totalFine, string fineThisPeriod = "", BindingList<ReturnBook> chosenBooks = null)
        {
            this.id = slipId;
            this.readerId = readerId;
            this.readerName = readerName;
            this.returnDate = returnDate;
            if (fineThisPeriod != "")
                this.fineThisPeriod = long.Parse(fineThisPeriod);
            this.totalFine = totalFine;

            if (chosenBooks != null)
                returnBooks = new BindingList<ReturnBook>(chosenBooks);
        }
    }
}
