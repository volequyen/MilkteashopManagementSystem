using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int idBill, int idDrink, int count) 
        { 
            this.ID = id;
            this.IDBill = idBill;
            this.IDDrink = idDrink;
            this.Count = count;
        }
        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.IDBill = (int)row["idBill"];
            this.IDDrink = (int)row["idDrink"];
            this.Count = (int)row["count"];
        }
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private int idBill;
        public int IDBill
        {
            get { return idBill; }
            set { idBill = value; }
        }
        private int idDrink;
        public int IDDrink
        {
            get { return idDrink; }
            set { idDrink = value; }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}
