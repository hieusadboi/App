using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    public class FoodDAO
    {
        public static FoodDAO instance;
        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return instance; }
            private set { instance = value; }
        }
        private FoodDAO() { }

        // Lấy danh sách món ăn theo ID danh mục
        public List<Food> GetListFoodByCategoryID(int id)
        {
            List<Food> listFood = new List<Food>();
            string query = "SELECT * FROM Food WHERE idCategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }

        // Lấy danh sách món ăn
        public List<Food> GetListFood()
        {
            List<Food> listFood = new List<Food>();
            string query = "SELECT * FROM Food";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }


        public bool InsertFood(string foodName, int idCategory, float price)
        {
            string query = "INSERT INTO Food (foodName, idCategory, price) VALUES (N'" + foodName + "', " + idCategory + ", " + price + ")";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateFood(int idFood, string foodName, int idCategory, float price)
        {
            string query = "UPDATE Food SET foodName = N'" + foodName + "', idCategory = " + idCategory + ", price = " + price + " WHERE idFood = " + idFood;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteFood(int idFood)
        {
            string query = "DELETE FROM Food WHERE idFood = " + idFood;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public List<Food> SearchFood(string keyword)
        {
            List<Food> listFood = new List<Food>();

            string query = @"
        SELECT f.*
        FROM Food AS f
        JOIN FoodCategory AS c ON f.idCategory = c.idCategory
        WHERE CONCAT(
            ISNULL(f.foodName, ''), ' ',
            ISNULL(f.idFood, ''), ' ',
            ISNULL(f.price, ''), ' ',
            ISNULL(f.idCategory, ''), ' ',
            ISNULL(c.categoryName, '')
        ) LIKE @keyword";

            object[] parameters = new object[] { "%" + keyword + "%" };
            DataTable data = DataProvider.Instance.ExecuteQuery(query, parameters);

            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);
                listFood.Add(food);
            }

            return listFood;
        }

        public DataTable SearchFoodIngredient(string keyword)
        {
            string query = @"
        SELECT f.IdFood, f.FoodName, f.Price, f.IdCategory, c.CategoryName
        FROM Food f
        JOIN FoodCategory c ON f.IdCategory = c.IdCategory
        WHERE CONCAT(
            ISNULL(CAST(f.IdFood AS NVARCHAR), ''), ' ',
            ISNULL(f.FoodName, ''), ' ',
            ISNULL(CAST(f.Price AS NVARCHAR), ''), ' ',
            ISNULL(c.CategoryName, '')
        ) LIKE @keyword";

            object[] parameters = new object[] { "%" + keyword + "%" };
            return DataProvider.Instance.ExecuteQuery(query, parameters);
        }

        public int GetFoodIdByName(string foodName)
        {
            string query = "SELECT idFood FROM Food WHERE foodName = @foodName";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { foodName });

            if (result != null)
                return Convert.ToInt32(result);
            return -1; // Trả về -1 nếu không tìm thấy
        }

        public Food GetFoodById(int idFood)
        {
            string query = "SELECT * FROM Food WHERE idFood = @idFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idFood });

            if (data.Rows.Count > 0)
                return new Food(data.Rows[0]);

            return null;
        }


    }
}
