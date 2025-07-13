using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO
{
    public class Food
    {

        public Food(int idFood, string foodName, int idCategory, float price)
        {
            this.IdFood = idFood;
            this.FoodName = foodName;
            this.IdCategory = idCategory;
            this.Price = price;
        }
        public Food(DataRow row)
        {
            this.IdFood = (int)row["idFood"];
            this.FoodName = row["foodName"].ToString();
            this.IdCategory = (int)row["idCategory"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }

        private int idFood;
        private string foodName;
        private float price;
        private int idCategory;

        public int IdFood { get => idFood; set => idFood = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public float Price { get => price; set => price = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }
    }
}
