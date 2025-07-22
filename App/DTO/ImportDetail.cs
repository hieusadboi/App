using System;
using System.Data;

namespace App.DTO
{
    public class ImportDetail
    {
        public int IdReceipt { get; set; }
        public int IdIngredient { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ImportDetail() { }

        public ImportDetail(DataRow row)
        {
            IdReceipt = (int)row["idReceipt"];
            IdIngredient = (int)row["idIngredient"];
            Quantity = (decimal)row["quantity"];
            UnitPrice = (decimal)row["unitPrice"];
        }
    }
}
