using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO
{
    public class Supplier
    {
        public int IdSupplier { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Supplier() { }

        public Supplier(DataRow row)
        {
            IdSupplier = Convert.ToInt32(row["idSupplier"]);
            SupplierName = row["supplierName"].ToString();
            Phone = row["phone"].ToString();
            Email = row["email"].ToString();
            Address = row["address"].ToString();
        }
    }
}