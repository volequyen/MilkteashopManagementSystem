using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DTO
{
    public class BillDetail
    {
        public BillDetail(string drinkName, int count, int price, int totalPrice = 0) 
        {
            this.DrinkName = drinkName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public BillDetail(DataRow row)
        {
            this.DrinkName = (string)row ["name"];
            this.Count =(int)row["count"];
            this.Price = (int)row["price"];
            this.TotalPrice = (int)row["totalPrice"];
        }
        private string drinkName;
        public string DrinkName
        {
            get { return drinkName; }
            set { drinkName = value; }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set {  count = value; } 
        }
        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        private int totalPrice;
        public int TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
    }
}
