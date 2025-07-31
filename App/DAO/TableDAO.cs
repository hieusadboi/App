using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            string query = "SELECT * FROM dbo.TableFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
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

        public bool InsertTable(string name)
        {
            string query = "INSERT dbo.TableFood (tableName, status) VALUES (N'" + name + "', N'Trống')";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateTable(int id, string name, string status)
        {
            string query = "UPDATE dbo.TableFood SET tableName = N'" + name + "', status = N'" + status + "' WHERE idTable = " + id;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteTable(int id)
        {
            // Hiển thị hộp thoại xác nhận xóa
            DialogResult resultConfirm = MessageBox.Show(
                "Bạn có chắc muốn xóa bàn này không?\nCác hóa đơn liên quan sẽ được chuyển sang bàn 'Mang Về'.",
                "Xác nhận xóa bàn",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            if (resultConfirm != DialogResult.OK)
                return false;

            // Tìm ID bàn "Mang Về"
            string getMangVeQuery = "SELECT idTable FROM TableFood WHERE tableName = N'Mang Về'";
            object result = DataProvider.Instance.ExecuteScalar(getMangVeQuery);

            if (result == null)
            {
                MessageBox.Show("Không tìm thấy bàn có tên là 'Mang Về'.\nVui lòng tạo bàn này trước khi xóa bàn khác.",
                    "Thiếu bàn 'Mang Về'", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int idMangVe = Convert.ToInt32(result);

            // Chuyển hóa đơn sang bàn "Mang Về"
            string updateBillQuery = $"UPDATE Bill SET idTable = {idMangVe} WHERE idTable = {id}";
            DataProvider.Instance.ExecuteNonQuery(updateBillQuery);

            // Xóa bàn
            string deleteTableQuery = $"DELETE FROM TableFood WHERE idTable = {id}";
            int rowsAffected = DataProvider.Instance.ExecuteNonQuery(deleteTableQuery);

            if (rowsAffected > 0)
            {
                MessageBox.Show("Xóa bàn thành công và đã chuyển các hóa đơn liên quan sang bàn 'Mang Về'.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Xóa bàn thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public List<Table> SearchTableByName(string name)
        {
            List<Table> list = new List<Table>();
            string query = "SELECT * FROM TableFood WHERE tableName LIKE @name";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { "%" + name + "%" });

            foreach (DataRow row in data.Rows)
            {
                list.Add(new Table(row));
            }

            return list;
        }


    }
}
