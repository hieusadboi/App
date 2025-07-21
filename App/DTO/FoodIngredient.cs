using System;
using System.Data;

namespace App.DTO
{
    public class FoodIngredient
    {
        //public int IdFood { get; set; }
        //public int IdIngredient { get; set; }
        //public decimal Quantity { get; set; }

        //public FoodIngredient() { }

        //public FoodIngredient(DataRow row)
        //{
        //    IdFood = Convert.ToInt32(row["idFood"]);
        //    IdIngredient = Convert.ToInt32(row["idIngredient"]);
        //    Quantity = Convert.ToDecimal(row["quantity"]);
        //}

        public int IdFood { get; set; }
        public int IdIngredient { get; set; }
        public string IngredientName { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }

        public FoodIngredient() { }

        public FoodIngredient(DataRow row)
        {
            IdFood = row.Field<int>("IdFood");
            IdIngredient = row.Field<int>("IdIngredient");
            IngredientName = row.Field<string>("IngredientName");
            Unit = row.Field<string>("Unit");
            Quantity = row.Field<decimal>("Quantity");
        }
    }
}
