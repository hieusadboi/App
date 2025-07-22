using System;
using System.Data;

namespace App.DTO
{
    public class ImportReceipt
    {
        public int IdReceipt { get; set; }
        public DateTime ImportDate { get; set; }
        public string ImportedBy { get; set; }
        public int IdSupplier { get; set; }
        public ImportReceipt() { }

        public ImportReceipt(DataRow row)
        {
            IdReceipt = (int)row["idReceipt"];
            ImportDate = (DateTime)row["importDate"];
            ImportedBy = row["importedBy"] != DBNull.Value ? row["importedBy"].ToString() : null;
            IdSupplier = (int)row["idSupplier"];
        }
    }
}
