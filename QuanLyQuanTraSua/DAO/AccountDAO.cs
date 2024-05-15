using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLyQuanTraSua.DTO;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace QuanLyQuanTraSua.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO();  return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }


        public Account GetAccount(string username)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from account where username = '" + username + "'");
            foreach (DataRow row in data.Rows)
            {
                Account account = new Account(row);
                return account;
            }
            return null;
        }

        public bool isExitUserName(string username)
        {
            string query = "SELECT COUNT(*) FROM Account WHERE username = N'" + username + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar(query);
            return result > 0;
        }

        public Account GetAdminAccount(string username)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from admin where username = '" + username + "'");
            foreach (DataRow row in data.Rows)
            {
                Account account = new Account(row);
                return account;
            }
            return null;
        }
        public bool InsertAccount(string username, string displayName)
        {
            string query = string.Format("insert into Account (username, displayname,password) values (N'{0}', N'{1}',N'{2}')", username, displayName, "3244185981728979115075721453575112");

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateAccount(string username, string displayName)
        {
            string query = string.Format("update Account set displayname = N'" + displayName + "' where username = '" + username + "'");
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool deleteAccount(string username)
        {
            string query = string.Format("delete Account where username =  '" + username + "'");
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateAccount(string username, string password,string displayname, string newpassword)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            string query = "EXEC USP_UpdateAcc @username, @displayname, @password, @newpassword";
            var parameters = new SqlParameter[]
                {
                new SqlParameter("@username", username),
                new SqlParameter("@password", hasPass),
                new SqlParameter("@displayName", displayname),
                new SqlParameter("@newPassword", newpassword)

                };
            int result = DataProvider.Instance.ExecuteNonQuery(query,parameters);
            return result > 0;
        }

        public bool UpdateAdmin(string username, string password, string displayname, string newpassword) 
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            string query = "EXEC USP_UpdateAd @username, @displayname, @password, @newpassword";
            var parameters = new SqlParameter[]
                {
                new SqlParameter("@username", username),
                new SqlParameter("@password", hasPass),
                new SqlParameter("@displayName", displayname),
                new SqlParameter("@newPassword", newpassword)

                };
            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }


    }
}
