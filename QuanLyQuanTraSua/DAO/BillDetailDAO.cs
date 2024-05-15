using QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DAO
{
    public class BillDetailDAO
    {
        private static BillDetailDAO instance;
        public static BillDetailDAO Instance
        {
            get { if (instance == null) instance = new BillDetailDAO(); return instance; }
            set { instance = value; }

        }
        private BillDetailDAO() { }
        public List<BillDetail> GetBillDetailList(int idBill)
        {
           
            List<BillDetail> billDetails = new List<BillDetail>();
            DataTable data = DataProvider.Instance.ExecuteQuery ("select d.name, bi.count, d.price, d.price*bi.count as totalPrice\r\nfrom dbo.bill as b, dbo.billInfor as bi, dbo.drink as d\r\nwhere bi.idBill = b.id and bi.idDrink = d.id and idBill = " + idBill);
            foreach (DataRow item in data.Rows)
            {
                BillDetail billDetail = new BillDetail(item);
                billDetails.Add(billDetail);
                
            }
            return billDetails;
        }
            
        
    }
}
