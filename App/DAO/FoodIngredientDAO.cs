using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App.DAO
{
    public class FoodIngredientDAO
    {
        private static FoodIngredientDAO instance;
        public static FoodIngredientDAO Instance
        {
            get { if (instance == null) instance = new FoodIngredientDAO(); return instance; }
            private set { instance = value; }
        }

        private FoodIngredientDAO() { }

        

        public List<FoodIngredient> GetIngredientsByFoodId(int idFood)
        {
            List<FoodIngredient> list = new List<FoodIngredient>();
            string query = "SELECT fi.IdFood, fi.IdIngredient, i.IngredientName, i.Unit, fi.Quantity " +
                          "FROM FoodIngredient fi " +
                          "JOIN Ingredient i ON fi.IdIngredient = i.IdIngredient " +
                          "WHERE fi.IdFood = @idFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idFood });

            foreach (DataRow row in data.Rows)
            {
                list.Add(new FoodIngredient(row));
            }

            return list;
        }

        public DataTable GetFoodIngredientDetail(int idFood)
        {
            string query = @"
                SELECT fi.idFood, fi.idIngredient, i.ingredientName, i.unit, fi.quantity
                FROM FoodIngredient fi
                JOIN Ingredient i ON fi.idIngredient = i.idIngredient
                WHERE fi.idFood = @idFood";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { idFood });
        }

        public bool InsertFoodIngredient(FoodIngredient fi)
        {
            string query = "INSERT INTO FoodIngredient (idFood, idIngredient, quantity) VALUES ( @idFood , @idIngredient , @quantity )";
            object[] parameters = new object[]
            {
                fi.IdFood,
                fi.IdIngredient,
                fi.Quantity
            };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool UpdateFoodIngredient(FoodIngredient fi)
        {
            string query = "UPDATE FoodIngredient SET quantity = @quantity WHERE idFood = @idFood AND idIngredient = @idIngredient";
            object[] parameters = new object[]
            {
                fi.Quantity,
                fi.IdFood,
                fi.IdIngredient
            };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool DeleteFoodIngredient(int idFood, int idIngredient)
        {
            string query = "DELETE FROM FoodIngredient WHERE idFood = @idFood AND idIngredient = @idIngredient";
            object[] parameters = new object[] { idFood, idIngredient };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool DeleteAllIngredientsOfFood(int idFood)
        {
            string query = "DELETE FROM FoodIngredient WHERE idFood = @idFood";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idFood });
            return result > 0;
        }


    }
}
