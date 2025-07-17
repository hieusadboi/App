using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App.DAO
{
    public class SupplierDAO
    {
        private static SupplierDAO instance;
        public static SupplierDAO Instance
        {
            get { if (instance == null) instance = new SupplierDAO(); return instance; }
            private set { instance = value; }
        }

        private SupplierDAO() { }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> list = new List<Supplier>();
            string query = "SELECT * FROM Supplier";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new Supplier(row));
            }

            return list;
        }

        public bool InsertSupplier(Supplier supplier)
        {
            string query = "INSERT INTO Supplier (supplierName, phone, email, address) VALUES ( @supplierName , @phone , @email , @address )";
            object[] parameters = new object[]
            {
                supplier.SupplierName,
                supplier.Phone,
                supplier.Email,
                supplier.Address
            };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool UpdateSupplier(Supplier supplier)
        {
            string query = "UPDATE Supplier SET supplierName = @supplierName , phone = @phone , email = @email , address = @address WHERE idSupplier = @idSupplier";
            object[] parameters = new object[]
            {
                supplier.SupplierName,
                supplier.Phone,
                supplier.Email,
                supplier.Address,
                supplier.IdSupplier
            };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool DeleteSupplier(int id)
        {
            string query = "DELETE FROM Supplier WHERE idSupplier = @idSupplier";
            object[] parameters = new object[] { id };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

    }
}