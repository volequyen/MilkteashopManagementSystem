using QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyQuanTraSua.DAO
{
    public class DrinkDAO
    {
        private static DrinkDAO instance;
        public static DrinkDAO Instance
        {
            get { if (instance == null) instance = new DrinkDAO(); return DrinkDAO.instance; }
            private set { DrinkDAO.instance = value; }



        }
        private DrinkDAO() { }
        public List<Drink> GetListDrink(int Id)
        {
            List<Drink> list = new List<Drink>();
            string query = "select * from Drink where idCategory = " + Id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Drink drink = new Drink(item);
                list.Add(drink);

            }
            return list;
        }

        public bool isDrinkExist(string name)
        {
            string query = "SELECT COUNT(*) FROM dbo.drink WHERE name = N'" + name + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar(query);
            return result > 0;
        }

        public bool InsertDrink(string name, int idCategory, int price)
        {
            string query = string.Format("insert into dbo.drink (name, idCategory, price) values (N'{0}', {1}, {2})", name, price, idCategory);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateDrink(string name, int idCategory, int price, int idDrink)
        {
            string query = string.Format("update dbo.drink set name = N'{0}', idCategory = {1}, price = {2} where id = {3} ", name, price, idCategory, idDrink);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool deleteDrink(int idDrink)
        {
            BillInfoDAO.Instance.deleteBillInfoByDrinkId(idDrink);
            string query = string.Format("delete dbo.drink where id =  " + idDrink);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

      
     


        //public List<Drink> GetDrinks()
        //{
        //    List<Drink> list = new List<Drink>();
        //    string query = "select * from Drink";
        //    DataTable data = DataProvider.Instance.ExecuteQuery(query);
        //    foreach (DataRow item in data.Rows)
        //    {
        //        Drink drink = new Drink(item);
        //        list.Add(drink);

        //    }
        //    return list;
        //}

    }
}
