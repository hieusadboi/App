using App.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            
            LoadAccountList();

            LoadFoodList();

            LoadCategoryList();

            LoadTableFood();

            LoadIngredient();

            LoadStaff();

            LoadFoodIngredient();

            LoadSupplier();

            LoadDoanhThu(DateTime.Now, DateTime.Now);
        }

        void LoadFoodList()
        {
            string query = "SELECT \r\n    F.idFood AS [Mã món],\r\n   C.categoryName AS [Danh mục],\r\n    F.foodName AS [Tên món],\r\n      F.price AS [Giá]\r\nFROM \r\n    Food F\r\nINNER JOIN FoodCategory C ON F.idCategory = C.idCategory\r\nORDER BY \r\n    C.categoryName, F.foodName;\r\n";
            dtgvFood.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void LoadAccountList()
        {
            string query = "EXEC dbo.USP_GetAccount";

            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }



        void LoadCategoryList()
        {
            string query = "USP_GetFoodCategory";
            dtgvCategory.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void LoadTableFood()
        {
            string query = "EXEC USP_GetTableFood";
            dtgvTableFood.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void LoadIngredient()
        {
            string query = "EXEC USP_GetIngredient";
            dtgvIngredient.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }


        void LoadStaff()
        {
            string query = "EXEC USP_GetStaff";
            dtgvStaff.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }


        void LoadFoodIngredient()
        {
            string query = "EXEC GetFoodIngredientSummary";
            dtgvFoodIngredient.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void LoadSupplier()
        {
            string query = "USP_GetSupplier";
            dtgvSuplier.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }


        void LoadDoanhThu(DateTime fromDate, DateTime toDate)
        {
            string query = "EXEC GetThuChi_OneTable_WithNote @FromDate , @ToDate";
            dtgvDoanhThu.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txbUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXemDoanhThu_Click(object sender, EventArgs e)
        {
            LoadDoanhThu(dtpkFromDate.Value, dtpkToDate.Value);
        }
    }
}
