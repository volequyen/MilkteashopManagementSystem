using QuanLyQuanTraSua.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanTraSua.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        public static CategoryDAO Instance {
        get { if (instance == null) instance = new CategoryDAO(); return CategoryDAO.instance; }
        private set { CategoryDAO.instance = value; }

                
                
        }
        private CategoryDAO() { }
        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select * from DrinkCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);

            }
            return list;
        }
         
        public Category GetCategory(int id)
        {
            Category category = null;
            string query = "select * from DrinkCategory where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;

            }
            return category;

        }

        public bool isCategoryExist(string name)
        {
            string query = "select count(*) FROM dbo.drinkCategory where name = N'" + name + "'";
            int result = (int)DataProvider.Instance.ExecuteScalar(query);
            return result > 0;
        }

        public bool InsertCategory(string name)
        {
            string query = "INSERT INTO dbo.drinkCategory (name) VALUES (N'" + name + "')";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateCategory(string name, int idCategory)
        {
            string query = "update dbo.drinkCategory set name = N'" + name + "' where id = " + idCategory;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteCategory(int idCategory)
        {
            string query = "delete dbo.drinkCategory where id = " + idCategory;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
            
        }
    }
}
