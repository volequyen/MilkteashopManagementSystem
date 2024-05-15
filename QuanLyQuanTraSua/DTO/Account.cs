using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DTO
{
    public class Account
    {
    
        public Account(string username, string displayname, string password = null)
        {
            this.UserName = username;
            this.DisplayName = displayname;
            this.Password = password;
        
        }
        public Account(DataRow row)
        {
            this.UserName = row ["username"].ToString();
            this.DisplayName = row["displayname"].ToString();
            this.Password = row["password"].ToString();
        }
        private string username;
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }
        private string displayname;
        public string DisplayName
        {
            get { return displayname; } 
            set { displayname = value; }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    
    }
}
