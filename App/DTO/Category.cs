using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO
{
    public class Category
    {
        public Category( int idCategory, string categoryName)
        {
            this.IdCategory = idCategory;
            this.CategoryName = categoryName;
        }

        public Category(DataRow row) 
        {
            this.IdCategory = (int)row["idCategory"];
            this.CategoryName = row["categoryName"].ToString();
        }

        private int idCategory;
        private string categoryName;

        public int IdCategory { get => idCategory; set => idCategory = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
    }
}
