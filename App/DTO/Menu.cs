using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO
{
    public class Menu
    {
        public Menu(string FoodName, int count, float price, float totalPrice, int idBill)
        {
            this.FoodName = FoodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
            this.IdBill = idBill;
        }

        public Menu(DataRow row)
        {
            this.FoodName = row["FoodName"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["price"]);
            this.TotalPrice = (float)Convert.ToDouble(row["TotalPrice"]);
            this.IdBill = (int)row["idBill"];
        }

        private int idBill;

        private float totalPrice;

        private float price;

        private int count;

        private string foodName;

        public string FoodName { get => foodName; set => foodName = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public int IdBill { get => idBill; set => idBill = value; }
    }
}
