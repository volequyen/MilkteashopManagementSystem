using QuanLyQuanTraSua.DAO;
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
    public partial class Staff : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
            }

        }
        public Staff(Account account)
        {
            InitializeComponent();
            this.LoginAccount = account;
            LoadCategory();
        }


        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccount a = new fAccount(loginAccount);
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        
        void LoadDrinkBycategory(int id)
        {
            List<Drink> listDrink = DrinkDAO.Instance.GetListDrink(id);
            cbDrink.DataSource = listDrink;
            cbDrink.DisplayMember = "Name";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if(cb.SelectedItem == null)
            {
                return;
            }
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadDrinkBycategory(id);
        }

        private void btnAddBill_Click(object sender, EventArgs e)
        {


        }
        private int currentId = -1;
        private void button1_Click(object sender, EventArgs e)
        {
            LvBill.Items.Clear();
            currentId = BillDAO.Instance.createBill();
        }

        private void btnAddDrink_Click(object sender, EventArgs e)
        {
            
           
        }
        public void showBill(int idBill)
        {
            LvBill.Items.Clear();
            int totalPrice = 0;
            List<BillDetail> listBillDetail = BillDetailDAO.Instance.GetBillDetailList(idBill);
            foreach(BillDetail item in listBillDetail)
            {
                ListViewItem lvItem = new ListViewItem(item.DrinkName.ToString()); 
                lvItem.SubItems.Add(item.Count.ToString());
                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                LvBill.Items.Add(lvItem);
            }
            ttPrice.Text = totalPrice.ToString();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            int idBill = BillDAO.Instance.max_idBill();
            int totalPrice = Convert.ToInt32(ttPrice.Text);
            if(idBill != -1)
            {
                if(totalPrice == 0)
                {
                    BillDAO.Instance.deleteBill(idBill);
                }
                else if(MessageBox.Show("Bạn có thật sự muốn thanh toán hóa đơn này?","Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.updateBill(idBill, totalPrice);
                    LvBill.Items.Clear();   
                    ttPrice.Text = "0";
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (currentId != -1)
            {
                int idDrink = (cbDrink.SelectedItem as Drink).ID;
                int count = (int)nmCount.Value;
                if (BillDAO.Instance.isExist(idDrink, currentId))
                {
                    int newcount = count;
                    string query = "update dbo.billInfor set count = " + newcount + "where idBill = " + currentId + "and idDrink = " + idDrink;
                    DataProvider.Instance.ExecuteNonQuery(query);
                }
                showBill(currentId);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idDrink = (cbDrink.SelectedItem as Drink).ID;
            if (BillDAO.Instance.isExist(idDrink, currentId))
            {
                string query = "delete from billInfor where idBill = " + currentId + "and idDrink = " + idDrink;
                DataProvider.Instance.ExecuteNonQuery(query);
            }
            showBill(currentId);
        }

        private void btnAddDrink_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAddDrink_Click_2(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (currentId != -1)
            {
                LvBill.Items.Clear();
                int idDrink = (cbDrink.SelectedItem as Drink).ID;
                int count = (int)nmCount.Value;
                if (BillDAO.Instance.isExist(idDrink, currentId) == false)
                {
                    BillInfoDAO.Instance.creatBillInfo(currentId, idDrink, count);
                }
                else
                {
                    int newcount = count + BillDAO.Instance.getCount(idDrink, currentId);
                    string query = "update dbo.billInfor set count = " + newcount + "where idBill = " + currentId + "and idDrink = " + idDrink;
                    DataProvider.Instance.ExecuteNonQuery(query);
                }
                showBill(currentId);
            }

        }
    }
}
