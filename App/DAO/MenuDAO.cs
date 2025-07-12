using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Internal;
using System.Drawing.Printing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
            private set { instance = value; }
        }

        private MenuDAO() { }

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = "SELECT f.foodName, bi.count, f.price, (bi.count * f.price) AS TotalPrice, b.idBill from dbo.BillInfo as bi, dbo.Bill as b, dbo.Food as f where bi.idBill = b.idBill and bi.idFood = f.idFood and b.status = 0 and b.idTable = " + id;
                           
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                System.Diagnostics.Debug.WriteLine(
                     $"Count: {item["count"]}, Price: {item["price"]}, TotalPrice: {item["TotalPrice"]}, IdBill: {item["idBill"]}"
                );
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }
    }
}
