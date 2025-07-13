using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            private set { instance = value; }
        }
        private BillDAO() { }

        /// Lấy danh sách hóa đơn chưa thanh toán theo ID bàn
        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.Bill WHERE idTable = " + id + " AND status = 0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1; // Trả về -1 nếu không tìm thấy hóa đơn chưa thanh toán
        }


        //public void InsertBill(int idTable)
        //{
        //    DataProvider.Instance.ExecuteNonQuery("EXEC dbo.USP_InsertBill @idTable", new object[] { idTable });
        //}

        public void InsertBill(int idTable, string createdBy)
        {
            DataProvider.Instance.ExecuteNonQuery(
                "EXEC dbo.USP_InsertBill @idTable , @createdBy",
                new object[] { idTable, createdBy }
            );
        }

        //public int GetMaxIDBill()
        //{
        //    try
        //    {
        //        DataTable data = DataProvider.Instance.ExecuteQuery("SELECT MAX(idBill) AS MaxID FROM dbo.Bill");
        //        if (data.Rows.Count > 0)
        //        {
        //            return Convert.ToInt32(data.Rows[0]["MaxID"]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Error in GetMaxIDBill: " + ex.Message);
        //    }
        //    return 1; // Trả về 1 nếu không tìm thấy ID tối đa
        //}


        public void CheckOut(int id)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), status = 1 WHERE idBill = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

    }
}
