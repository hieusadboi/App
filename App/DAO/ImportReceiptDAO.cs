using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace App.DAO
{
    public class ImportReceiptDAO
    {
        private static ImportReceiptDAO instance;
        public static ImportReceiptDAO Instance
        {
            get { if (instance == null) instance = new ImportReceiptDAO(); return instance; }
            private set { instance = value; }
        }

        private ImportReceiptDAO() { }

        public List<ImportReceipt> GetAllReceipts()
        {
            List<ImportReceipt> list = new List<ImportReceipt>();
            string query = "SELECT * FROM ImportReceipt ORDER BY idReceipt DESC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new ImportReceipt(row));
            }

            return list;
        }

        public int InsertReceipt(ImportReceipt receipt)
        {
            string query = "INSERT INTO ImportReceipt ( importDate, importedBy ) OUTPUT INSERTED.idReceipt VALUES ( @importDate , @importedBy )";
            object[] parameters = new object[]
            {
                receipt.ImportDate,
                receipt.ImportedBy
            };

            object result = DataProvider.Instance.ExecuteScalar(query, parameters);
            return (int)result;
        }

        public bool UpdateReceipt(ImportReceipt receipt)
        {
            string query = "UPDATE ImportReceipt SET importDate = @importDate , importedBy = @importedBy WHERE idReceipt = @idReceipt ";
            object[] parameters = new object[]
            {
                receipt.ImportDate,
                receipt.ImportedBy,
                receipt.IdReceipt
            };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool DeleteReceipt(int id)
        {
            string query = "DELETE FROM ImportReceipt WHERE idReceipt = @idReceipt";
            object[] parameters = new object[] { id };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
    }
}
