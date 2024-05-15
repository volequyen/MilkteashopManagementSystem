using QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { instance = value; }
        }
        private BillDAO() { }

        public int createBill()
        {
            string query = "INSERT INTO Bill(dateofbill) VALUES(GETDATE()); SELECT SCOPE_IDENTITY();";
            int id = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query));
            return id;
        }
        public int max_idBill()
        {
            return (int)DataProvider.Instance.ExecuteScalar("select max(id) from dbo.bill");
        }

        public bool isExist(int idDrink, int idBill)
        {
            string query = "select idDrink from billInfor where idDrink = " + idDrink + " and idBill = " + idBill;
            DataTable resultTable = DataProvider.Instance.ExecuteQuery(query);

            int rowCount = resultTable.Rows.Count;

            return rowCount > 0;
        }

        public int getCount(int idDrink, int idBill)
        {
            string query = "select count from billInfor where idDrink = " + idDrink   + " and idBill = " + idBill;
            int result = (int)DataProvider.Instance.ExecuteScalar(query);
            return result;
        }
        public void updateBill(int idBill, int totalPrice) 
        {
            string query = "update dbo.bill set status = 1, " + " totalPrice = "+ totalPrice  + " where id = " + idBill;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public DataTable GetListBill(DateTime date)
        {

            return DataProvider.Instance.ExecuteQuery("exec USP_GetListBill @dateofbill = '" + date.ToString("yyyy-MM-dd") + "'");

        }

        public void deleteBill(int idBill)
        {
            string query = "delete dbo.bill where id = " + idBill;
            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
