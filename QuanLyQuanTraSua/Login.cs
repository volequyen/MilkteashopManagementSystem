using QuanLyQuanTraSua.DAO;
using QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public bool Logins(string username, string password, string type)
        {
            
            DataTable result = new DataTable();
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }

            if (type == "Admin")
            {
                
                result = DataProvider.Instance.ExecuteQuery("select * from dbo.admin where username = N'" + username + "' and password = N'" + hasPass + "'");
            }
            if (type == "Staff")
            {
                   string query = "select * from dbo.Account where username = N'" + username + "' and password = N'" + hasPass + "'";
                result = DataProvider.Instance.ExecuteQuery(query);
            }
            return result.Rows.Count > 0;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string password = txbPassWord.Text;
            string type = cbChoose.Text;
            if (Logins(username, password,type))
            {
               
              
                if (cbChoose.Text == "Admin")
                {
                    Account adminAccount = AccountDAO.Instance.GetAdminAccount(username);
                    Admin a = new Admin(adminAccount);
                    this.Hide();
                    a.ShowDialog();
                    this.Show();
                }

                else if (cbChoose.Text == "Staff")
                {
                    Account loginAccount = AccountDAO.Instance.GetAccount(username);
                    Staff s = new Staff(loginAccount);
                    this.Hide();
                    s.ShowDialog();
                    this.Show();
                }
            }
            else MessageBox.Show("Vui lòng nhập đúng thông tin!");
        }

    
         
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thật sự muốn thoát chương trình?","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }    
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chố_Click(object sender, EventArgs e)
        {

        }
    }
}
