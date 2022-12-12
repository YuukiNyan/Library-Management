using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonTraSach.Models
{
    public class Reader
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string birth { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string createAt { get; set; }
        public string expiredDate { get; set; }
        public long debt { get; set; }

        public Reader()
        {
            id = "";
            name = "";
            type = "";
            birth = "";
            address = "";
            email = "";
            createAt = "";
            expiredDate = "";
            debt = long.Parse("");
        }
        public Reader(Reader reader)
        {
            this.id = reader.id;
            this.name = reader.name;
            this.type = reader.type;
            this.birth = reader.birth;
            this.address = reader.address;
            this.email = reader.email;
            this.createAt = reader.createAt;
            this.expiredDate = reader.expiredDate;
            this.debt = reader.debt;
        }
        public Reader(string id, string name, string type, DateTime birth, string address, string email, DateTime createAt, DateTime expiredDate, long debt)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.birth = birth.ToString("dd/MM/yyyy");
            this.address = address;
            this.email = email;
            this.createAt = createAt.ToString("dd/MM/yyyy");
            this.expiredDate = expiredDate.ToString("dd/MM/yyyy");
            this.debt = debt;
        }
    }
}
