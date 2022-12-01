using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraCuuSach.Models
{
    public class Reader
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public DateTime birth { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public DateTime createAt { get; set; }
        public DateTime expiredDate { get; set; }
        public float debt { get; set; }

        public Reader()
        {
            id = "";
            name = "";
            type = "";
            birth = DateTime.Parse("");
            address = "";
            email = "";
            createAt = DateTime.Parse("");
            expiredDate = DateTime.Parse("");
            debt = float.Parse("");
        }
        public Reader(Reader reader)
        {
            this.id = reader.id;
            this.name = reader.name;
            this.type = reader.type;
            this.birth = reader.birth;
            this.address = reader.address;
            this.email = reader.address;
            this.createAt = reader.createAt;
            this.expiredDate = reader.expiredDate;
            this.debt = reader.debt;
        }
        public Reader(string id, string name, string type, DateTime birth, string address, string email, DateTime createAt, DateTime expiredDate, float debt)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.birth = birth;
            this.address = address;
            this.email = address;
            this.createAt = createAt;
            this.expiredDate = expiredDate;
            this.debt = debt;
        }
    }
}
