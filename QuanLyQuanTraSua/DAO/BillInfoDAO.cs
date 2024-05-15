using QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;
        public static BillInfoDAO Instance 
        {
            get { if(instance == null) instance = new BillInfoDAO(); return instance; } 
            set { instance = value; }
        }
        private BillInfoDAO() { }

       public List<BillInfo> GetListBillInfor(int id)
        {
            List <BillInfo> ListBill = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.billInfor where idBill = " + id);
            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                ListBill.Add(info);
            }
            return ListBill;

        }

        public void creatBillInfo(int idBill, int idDrink, int count)
        {
            string query = "INSERT INTO billInfor(idBill, idDrink, count) VALUES(@idBill, @idDrink, @count)";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@idBill", idBill),
            new SqlParameter("@idDrink", idDrink),
            new SqlParameter("@count",count)
            };

            DataProvider.Instance.ExecuteNonQuery(query, parameters);

        }
        
        public void deleteBillInfoByDrinkId(int idDrink)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.billInfor where idDrink = " + idDrink);
        }

     }
}

