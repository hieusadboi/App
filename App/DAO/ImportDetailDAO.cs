using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App.DAO
{
    public class ImportDetailDAO
    {
        private static ImportDetailDAO instance;
        public static ImportDetailDAO Instance
        {
            get { if (instance == null) instance = new ImportDetailDAO(); return instance; }
            private set { instance = value; }
        }

        private ImportDetailDAO() { }

        public List<ImportDetail> GetDetailsByReceipt(int idReceipt)
        {
            List<ImportDetail> list = new List<ImportDetail>();
            string query = "SELECT * FROM ImportDetail WHERE idReceipt = @idReceipt";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idReceipt });

            foreach (DataRow row in data.Rows)
            {
                list.Add(new ImportDetail(row));
            }

            return list;
        }

        public List<ImportDetail> GetDetailsAndUnitByReceipt(int idReceipt)
        {
            List<ImportDetail> list = new List<ImportDetail>();
            string query = "SELECT id.*, i.Unit " +
                                       "FROM ImportDetail id " +
                                       "INNER JOIN Ingredient i ON id.IdIngredient = i.IdIngredient " +
                                       "WHERE id.idReceipt = @idReceipt";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idReceipt });

            foreach (DataRow row in data.Rows)
            {
                list.Add(new ImportDetail(row));
            }

            return list;
        }

        public decimal GetTotalCostByReceipt(int idReceipt)
        {
            decimal totalCost = 0;
            string query = "SELECT SUM(Quantity * UnitPrice) AS TotalCost FROM ImportDetail WHERE IdReceipt = @IdReceipt";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { idReceipt });

            if (result != null && result != DBNull.Value)
            {
                totalCost = Convert.ToDecimal(result);
            }

            return totalCost;
        }

        public bool InsertDetail(ImportDetail detail)
        {
            string query = "INSERT INTO ImportDetail ( idReceipt , idIngredient , quantity , unitPrice ) VALUES ( @idReceipt , @idIngredient , @quantity , @unitPrice )";
            object[] parameters = new object[]
            {
                detail.IdReceipt,
                detail.IdIngredient,
                detail.Quantity,
                detail.UnitPrice
            };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool UpdateDetail(ImportDetail detail)
        {
            string query = "UPDATE ImportDetail SET quantity = @quantity , unitPrice = @unitPrice WHERE idReceipt = @idReceipt AND idIngredient = @idIngredient ";
            object[] parameters = new object[]
            {
                detail.Quantity,
                detail.UnitPrice,
                detail.IdReceipt,
                detail.IdIngredient
            };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool DeleteDetail(int idReceipt, int idIngredient)
        {
            string query = "DELETE FROM ImportDetail WHERE idReceipt = @idReceipt AND idIngredient = @idIngredient";
            object[] parameters = new object[] { idReceipt, idIngredient };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
    }
}
