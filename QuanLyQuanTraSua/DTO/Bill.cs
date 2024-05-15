using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DTO
{
    public class Bill
    {

        public Bill(int id, DateTime dateofbill, int status)
        {
            this.ID = id;
            this.Dateofbill = dateofbill;
            this.Status = status;

        }

        public Bill(DataRow row)
        {
            this.ID =(int)row["id"];
            this.Dateofbill = (DateTime)row ["dateofbill"];
            this.Status =(int)row["status"];
        }
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime dateofbill;
        public DateTime Dateofbill
        {
            get { return dateofbill; }
            set { dateofbill = value; }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
