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
            string query = "INSERT INTO ImportReceipt ( importDate , importedBy , idSupplier ) " +
                           "OUTPUT INSERTED.idReceipt " +
                           "VALUES ( @importDate , @importedBy , @idSupplier )";

            object[] parameters = new object[]
            {
        receipt.ImportDate,
        receipt.ImportedBy,
        receipt.IdSupplier // thêm dòng này
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

        public DataTable SearchImportReceipt(string keyword)
        {
            string query = @"
        SELECT r.IdReceipt, r.ImportDate, r.ImportedBy, r.IdSupplier, s.SupplierName
        FROM ImportReceipt r
        JOIN Supplier s ON r.IdSupplier = s.IdSupplier
        WHERE CONCAT(
            ISNULL(CAST(r.IdReceipt AS NVARCHAR), ''), ' ',
            ISNULL(r.ImportedBy, ''), ' ',
            ISNULL(CAST(r.ImportDate AS NVARCHAR), ''), ' ',
            ISNULL(s.SupplierName, '')
        ) LIKE @keyword";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { "%" + keyword + "%" });
        }

    }
}
