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
                tableList.Add(table);
            }

            return tableList;
        }

    }
}
