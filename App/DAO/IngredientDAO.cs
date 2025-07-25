﻿using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace App.DAO
{
    public class IngredientDAO
    {
        private static IngredientDAO instance;
        public static IngredientDAO Instance
        {
            get { if (instance == null) instance = new IngredientDAO(); return instance; }
            private set { instance = value; }
        }

        private IngredientDAO() { }

        public List<Ingredient> GetListIngredient()
        {
            List<Ingredient> list = new List<Ingredient>();
            string query = "SELECT * FROM Ingredient";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                Ingredient ingredient = new Ingredient(row);
                list.Add(ingredient);
            }
            return list;
        }

        public List<string> GetAllUnits()
        {
            List<string> units = new List<string>();
            string query = "SELECT DISTINCT unit FROM Ingredient";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                units.Add(row["unit"].ToString());
            }
            return units;
        }

        public List<IngredientUnit> GetUnitsWithId()
        {
            List<IngredientUnit> units = new List<IngredientUnit>();
            string query = "SELECT DISTINCT IdIngredient, Unit FROM Ingredient WHERE Unit IS NOT NULL";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                if (row["Unit"] != DBNull.Value)
                {
                    units.Add(new IngredientUnit(
                        (int)row["IdIngredient"],
                        row["Unit"].ToString()
                    ));
                }
            }
            return units;
        }

        public bool IsIngredientExist(int id)
        {
            string query = "SELECT COUNT(*) FROM Ingredient WHERE idIngredient = " + id;
            object result = DataProvider.Instance.ExecuteScalar(query);
            return Convert.ToInt32(result) > 0;
        }
        public string GetUnitByIdIngredient(int idIngredient)
        {
            string query = "SELECT unit FROM Ingredient WHERE IdIngredient = @IdIngredient";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idIngredient });
            return data.Rows.Count > 0 ? data.Rows[0]["unit"].ToString() : null;
        }

        public Ingredient GetIngredientByID(int id)
        {
            string query = "SELECT * FROM Ingredient WHERE idIngredient = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                return new Ingredient(data.Rows[0]);
            }
            return null;
        }
        public List<FoodIngredient> GetNotEnoughIngredients(int foodId, int count)
        {
            var result = new List<FoodIngredient>();

            var requiredIngredients = FoodIngredientDAO.Instance.GetIngredientsByFoodId(foodId);

            foreach (var item in requiredIngredients)
            {
                Ingredient current = IngredientDAO.Instance.GetIngredientByID(item.IdIngredient);
                decimal requiredAmount = item.Quantity * count;

                if (current == null || current.Quantity < requiredAmount)
                {
                    result.Add(item);
                }
            }

            return result;
        }



        public List<Ingredient> GetLowStockIngredients()
        {
            List<Ingredient> list = new List<Ingredient>();

            string query = "SELECT * FROM Ingredient";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Ingredient ingredient = new Ingredient(row);
                decimal threshold = GetThresholdByUnit(ingredient.Unit);

                if (ingredient.Quantity <= threshold)
                {
                    list.Add(ingredient);
                }
            }

            return list;
        }

        private decimal GetThresholdByUnit(string unit)
        {
            switch (unit.ToLower())
            {
                case "gram": return 500;
                case "ml": return 30;
                case "kg": return 0.5m;
                case "cái":
                case "miếng":
                case "trái":
                case "chai":
                case "gói":
                    return 10;
                default: return 1;
            }
        }


        public bool InsertIngredient(string name, string unit, decimal quantity)
        {
            string query = $"INSERT INTO Ingredient (ingredientName, unit, quantity) VALUES (N'{name}', N'{unit}', {quantity.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateIngredient(int id, string name, string unit, decimal quantity)
        {
            string query = $"UPDATE Ingredient SET ingredientName = N'{name}', unit = N'{unit}', quantity = {quantity.ToString(System.Globalization.CultureInfo.InvariantCulture)} WHERE idIngredient = {id}";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteIngredient(int id)
        {
            string query = $"DELETE FROM Ingredient WHERE idIngredient = {id}";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable SearchIngredient(string keyword)
        {
            string query = @"
        SELECT * FROM Ingredient
        WHERE CONCAT(
            ISNULL(CAST(idIngredient AS NVARCHAR), ''), ' ',
            ISNULL(ingredientName, ''), ' ',
            ISNULL(unit, ''), ' ',
            ISNULL(CAST(quantity AS NVARCHAR), '')
        ) LIKE @keyword";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { "%" + keyword + "%" });
        }


    }
}
