using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return instance; }
            private set { instance = value; }
        }
        private CategoryDAO() { }

        /// <summary>   
        public List<DTO.Category> GetListCategory()
        {
            List<DTO.Category> listCategory = new List<DTO.Category>();

            string query = "SELECT * FROM dbo.FoodCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                DTO.Category category = new DTO.Category(item);
                listCategory.Add(category);
            }

            return listCategory;
        }

        public Category GetCategoryById(int idCategory)
        {
            Category category = null;
            string query = "SELECT * FROM dbo.FoodCategory WHERE idCategory = " + idCategory;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                category = new Category(data.Rows[0]);
            }
            return category;
        }

        public bool InsertCategory(string categoryName)
        {
            string query = "INSERT dbo.FoodCategory (categoryName) VALUES (N'" + categoryName + "')";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateCategory(int idCategory, string categoryName)
        {
            string query = "UPDATE dbo.FoodCategory SET categoryName = N'" + categoryName + "' WHERE idCategory = " + idCategory;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool HasFoodInCategory(int idCategory)
        {
            string query = "SELECT COUNT(*) FROM Food WHERE idCategory = @idCategory";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { idCategory });
            return Convert.ToInt32(result) > 0;
        }

        public bool DeleteCategory(int idCategory)
        {
            if (HasFoodInCategory(idCategory))
            {
                return false; 
            }
            string query = "DELETE FROM dbo.FoodCategory WHERE idCategory = " + idCategory;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public List<Category> SearchCategoryByName(string name)
        {
            List<Category> listCategory = new List<Category>();
            string query = "SELECT * FROM dbo.FoodCategory WHERE categoryName LIKE N'%" + name + "%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category);
            }
            return listCategory;
        }
    }
}
