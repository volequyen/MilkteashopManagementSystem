﻿using QuanLyQuanTraSua.DAO;
using QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class fAccount : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                ChangeAccount(LoginAccount);
            }


        }
        public fAccount(Account account)
        {
            InitializeComponent();
            this.LoginAccount = account;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void ChangeAccount(Account account) 
        {
            txbDisplayName.Text = account.DisplayName;
            txbUserName.Text = account.UserName;
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string displayName = txbDisplayName.Text;
            string userName = txbUserName.Text;
            string password = txbPassWord.Text;
            string newpass = txbNewPass.Text;
       
            if(AccountDAO.Instance.UpdateAccount(userName, password, displayName, newpass))
            {
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng mật khẩu");
            }

        }


    }
}
