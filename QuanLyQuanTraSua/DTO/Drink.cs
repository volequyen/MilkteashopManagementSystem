using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DTO
{
    public class Drink
    {
        public Drink(int id, string name, int idCategory, int price)
        {
            this.ID = id;
            this.Name = name;
            this.IDCategory = idCategory;
            this.Price = price;
        }
        public Drink(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.IDCategory = (int)row["idCategory"];
            this.Price = (int)row["price"];
        }
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int idCategory;
        public int IDCategory
        {
            get { return idCategory; }
            set { idCategory = value; }
        }
        private int price;
        public int Price
        {
            get { return price; }   
            set { price = value; }
        }
    }
}
