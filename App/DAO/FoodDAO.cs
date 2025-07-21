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

    }
}
