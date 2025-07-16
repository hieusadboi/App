using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    internal class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        public static int TableWidth = 100;
        public static int TableHeight = 100;

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                // Chỉ thêm bàn nếu tên không phải là "Mang về"
                if (table.Name != "Mang về")
                    tableList.Add(table);
            }

            return tableList;
        }

        // Lấy thông tin bàn theo ID
        public Table GetTableByID(int id)
        {
            Table table = null;
            string query = "SELECT idTable, tableName, status FROM dbo.TableFood WHERE idTable = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                table = new Table(item);
                break; // Chỉ lấy bản ghi đầu tiên (vì idTable là PK, không lặp)
            }
            return table;
        }

        public Table GetTakeAwayTable()
        {
            string query = "SELECT * FROM TableFood WHERE tableName = N'Mang về'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                return new Table(data.Rows[0]);
            }
            return null;
        }

    }
}
