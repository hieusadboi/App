using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    public class BillInfoDAO
    {
        public static BillInfoDAO instance;
        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set {instance = value; }
        }
        private BillInfoDAO() { }

        // Lấy danh sách thông tin hóa đơn theo ID hóa đơn
        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            string query = "SELECT * FROM dbo.BillInfo WHERE idBill = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }
            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery(
                "EXEC dbo.USP_InsertBillInfo @idBill , @idFood , @count",
                new object[] { idBill, idFood, count }
            );
        }

        public void DeleteFoodFromBill(int idBill, int idFood)
        {
            string query = "DELETE FROM BillInfo WHERE idBill = @idBill AND idFood = @idFood";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill, idFood });
        }

    }
}
