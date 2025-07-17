using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO
{
    public class Ingredient
    {
        public Ingredient(int idIngredient, string ingredientName, string unit, decimal quantity)
        {
            this.IdIngredient = idIngredient;
            this.IngredientName = ingredientName;
            this.Unit = unit;
            this.Quantity = quantity;
        }

        public Ingredient(DataRow row)
        {
            this.IdIngredient = (int)row["idIngredient"];
            this.IngredientName = row["ingredientName"].ToString();
            this.Unit = row["unit"].ToString();
            this.Quantity = Convert.ToDecimal(row["quantity"]);
        }

        private int idIngredient;
        private string ingredientName;
        private string unit;
        private decimal quantity;

        public int IdIngredient { get => idIngredient; set => idIngredient = value; }
        public string IngredientName { get => ingredientName; set => ingredientName = value; }
        public string Unit { get => unit; set => unit = value; }
        public decimal Quantity { get => quantity; set => quantity = value; }
    }
}
