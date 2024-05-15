using QuanLyQuanTraSua.DAO;
using QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyQuanTraSua
{
    public partial class Admin : Form
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
        BindingSource drinkList = new BindingSource();
        BindingSource categoryList = new BindingSource();   
        BindingSource accountList = new BindingSource();    
        public Admin(Account account)
        {
            InitializeComponent();
            this.LoginAccount = account;
            dtgvDrink.DataSource = drinkList;
            dtgvCategory.DataSource = categoryList;
            dtgvAccount.DataSource = accountList;
            LoadAccountList();
            LoadCategoryList();
            LoadDrinkList();
            AddDrinkBinding();
            AddCategoryBinding();   
            AddAccountBinding();
            LoadCategoryIntoCB(cbCategory);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        void LoadAccountList()
        {
            string query = "select username as [Tên đăng nhập], displayname as [Tên nhân viên] from dbo.Account";
            DataProvider provider = new DataProvider();
            accountList.DataSource = provider.ExecuteQuery(query);
            dtgvAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void LoadCategoryList()
        {
            string query1 = "select id as [ID], name as [Tên danh mục] from dbo.drinkCategory";
            DataProvider provider1 = new DataProvider();    
            categoryList.DataSource = provider1.ExecuteQuery(query1);
            dtgvCategory.AutoSizeColumnsMode= DataGridViewAutoSizeColumnsMode.Fill;
        }
        void LoadDrinkList()
        {
            string query2 = "select id as [ID], name as [Tên thức uống], idCategory, price as [Giá] from dbo.drink";
            DataProvider provider2 = new DataProvider();
            drinkList.DataSource = provider2.ExecuteQuery(query2);
            dtgvDrink.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void LoadListBillByDate(DateTime date)
        {
         
            dtgvListBill.DataSource = BillDAO.Instance.GetListBill(date);
                
        }

        List<Drink> SearchDrinkByIdCategory(int idCategory)
        {
            List<Drink> list = new List<Drink>();
            DrinkDAO.Instance.GetListDrink(idCategory);
            return list;
        }
        

        
        private void button1_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(getDate.Value);
        }

        //void LoadListDrink()
        //{
        //    dtgvDrink.DataSource = DrinkDAO.Instance.GetDrinks();
        //}
        private void btnView_Click(object sender, EventArgs e)
        {
            //LoadListDrink();

        }

        /* drink */
         void AddDrinkBinding()
        {
            txbDrinkName.DataBindings.Add(new Binding("Text", dtgvDrink.DataSource, "tên thức uống",true, DataSourceUpdateMode.Never));
            txbIdDrink.DataBindings.Add(new Binding("Text", dtgvDrink.DataSource, "id",true, DataSourceUpdateMode.Never));
            nmPrice.DataBindings.Add(new Binding("Value", dtgvDrink.DataSource, "giá", true, DataSourceUpdateMode.Never));

        }

        void LoadCategoryIntoCB(ComboBox cb)
        {
            cbCategory.DataSource = CategoryDAO.Instance.GetListCategory();
            cbCategory.DisplayMember = "Name";
        }

        private void txbIdDrink_TextChanged(object sender, EventArgs e)
        { 
            if(dtgvDrink.SelectedCells.Count > 0)
            {
                int id = (int)dtgvDrink.SelectedCells[0].OwningRow.Cells["idCategory"].Value;

                Category category = CategoryDAO.Instance.GetCategory(id);
                cbCategory.SelectedItem = category;

                int index = -1;
                int i = 0;
                foreach (Category item in cbCategory.Items)
                {
                    if (item.ID == category.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
            cbCategory.SelectedIndex = index;

            }



        }

        private void addDrink_Click(object sender, EventArgs e)
        {
            string name = txbDrinkName.Text;
            int idCategory = (cbCategory.SelectedItem as Category).ID;
            int price =(int) nmPrice.Value;
            if (DrinkDAO.Instance.isDrinkExist(name))
            {
                MessageBox.Show("Món đã tồn tại, vui lòng thêm món khác!");
            }
            else if (DrinkDAO.Instance.InsertDrink(name, price, idCategory))
            {
                MessageBox.Show("Thêm món thành công");
                LoadDrinkList();
            }
            else MessageBox.Show("Thêm món không thành công");
        }

        private void updateDrink_Click(object sender, EventArgs e)
        {
            string name = txbDrinkName.Text;
            int idCategory = (cbCategory.SelectedItem as Category).ID;
            int price = (int)nmPrice.Value;
            int idDrink = Convert.ToInt32(txbIdDrink.Text);
            if (DrinkDAO.Instance.UpdateDrink(name, price, idCategory, idDrink))
            {
                MessageBox.Show("Sửa món thành công");
                LoadDrinkList();
            }
            else MessageBox.Show("Sửa món không thành công");
        }

        private void DeleteDrink_Click(object sender, EventArgs e)
        {
            int idDrink = Convert.ToInt32(txbIdDrink.Text);
            if (DrinkDAO.Instance.deleteDrink(idDrink))
            {
                MessageBox.Show("Xóa món thành công");
                LoadDrinkList();
            }
            else MessageBox.Show("Xóa món không thành công");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           this.Close();
        }




        /* category*/
        void AddCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "tên danh mục", true, DataSourceUpdateMode.Never));
            txbCategoryId.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
        }

        private void addCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            if (CategoryDAO.Instance.isCategoryExist(name))
            {
                MessageBox.Show("Danh mục đã tồn tại, vui lòng thêm danh mục khác!");
            }
            else if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công");
                LoadCategoryList();
            }
            else MessageBox.Show("Thêm danh mục không thành công");
        }

        private void updateCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            int idCategory = Convert.ToInt32(txbCategoryId.Text);
            if (CategoryDAO.Instance.UpdateCategory(name, idCategory))
            {
                MessageBox.Show("Sửa danh mục thành công");
                LoadCategoryList();
            }
            else MessageBox.Show("Sửa danh mục không thành công");
        }

        private void deleteCategory_Click(object sender, EventArgs e)
        {
            int idCategory = Convert.ToInt32(txbCategoryId.Text);
            if (CategoryDAO.Instance.DeleteCategory(idCategory))
            {
                MessageBox.Show("Xóa danh mục thành công");
                LoadCategoryList();
            }
            else MessageBox.Show("Xóa danh mục không thành công");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnUpdateAcc_Click(object sender, EventArgs e)
        {
            fAdmin a = new fAdmin(loginAccount);
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        /*Account*/
        void AddAccountBinding()
        {
            txbStaffName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "tên nhân viên", true, DataSourceUpdateMode.Never));
            txbAccUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "tên đăng nhập", true,DataSourceUpdateMode.Never));
            //txbPassword.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "mật khẩu", true, DataSourceUpdateMode.Never));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string displayName = txbStaffName.Text;
            string username = txbAccUserName.Text;
            if (AccountDAO.Instance.isExitUserName(username))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại");
            }
            else if (AccountDAO.Instance.InsertAccount(username,displayName))
            {
                MessageBox.Show("Thêm tài khoản thành công");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản không thành công");
            }
            
        }

        private void button13_Click(object sender, EventArgs e)
        {

            string displayName = txbStaffName.Text;
            string username = txbAccUserName.Text;
            if (AccountDAO.Instance.UpdateAccount(username,displayName))
            {
                MessageBox.Show("Sửa tài khoản thành công");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Sửa tài khoản không thành công");
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string username = txbAccUserName.Text;
            if (AccountDAO.Instance.deleteAccount(username))
            {
                MessageBox.Show("Xóa tài khoản thành công");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Xóa tài khoản không thành công");
            }
        }
    }
}
