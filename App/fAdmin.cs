using App.DAO;
using App.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace App
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            
            //LoadAccountList();

            //LoadFoodList();

            //LoadCategoryList();

            //LoadTableFood();

            //LoadIngredient();

            //LoadStaff();

            //LoadFoodIngredient();

            //LoadSupplier();

            //LoadDoanhThu(DateTime.Now, DateTime.Now);

            LoadALL();
        }

        void LoadALL()
        {
            LoadAccountList();

            dtgvFood.DataSource = foodList;
            LoadFoodList();
            AddFoodBinding();
            LoadCategoryIntoComboBox(cbFoodCategory);

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
            foodList .DataSource = FoodDAO.Instance.GetListFood();
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


        //void LoadDoanhThu(DateTime fromDate, DateTime toDate)
        //{
        //    string query = "EXEC GetThuChi_OneTable_WithNote @FromDate , @ToDate";
        //    dtgvDoanhThu.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        //}

        // Gọi một lần ở Form_Load hoặc khởi tạo
        void InitDoanhThuColumns()
        {
            dtgvDoanhThu.Columns.Clear();
            dtgvDoanhThu.AutoGenerateColumns = false;

            dtgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ngay",
                HeaderText = "Ngày",
                DataPropertyName = "Ngày",
                Width = 160,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });

            dtgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Loai",
                HeaderText = "Loại",
                DataPropertyName = "Loại",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenHang",
                HeaderText = "Tên Hàng",
                DataPropertyName = "Tên Hàng",
                Width = 250,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10)
                }
            });

            dtgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "Số Lượng",
                DataPropertyName = "Số Lượng",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn Giá",
                DataPropertyName = "Đơn Giá",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10),
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dtgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien",
                HeaderText = "Thành Tiền",
                DataPropertyName = "Thành Tiền",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10),
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dtgvDoanhThu.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GhiChu",
                HeaderText = "Ghi Chú",
                DataPropertyName = "Ghi Chú",
                Width = 650,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });

            // Font cho hàng tiêu đề (header): In đậm
            dtgvDoanhThu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dtgvDoanhThu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgvDoanhThu.EnableHeadersVisualStyles = false;
        }


        void LoadDoanhThu(DateTime fromDate, DateTime toDate)
        {
            InitDoanhThuColumns();
            string query = "EXEC GetThuChi_OneTable_WithNote @FromDate , @ToDate";
            dtgvDoanhThu.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        }


        void LoadNhapHang(DateTime fromDate, DateTime toDate)
        {
            string query = "EXEC GetThongKeNhapHang @FromDate , @ToDate";
            dtgvDoanhThu.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }


        private void btnXemDoanhThu_Click(object sender, EventArgs e)
        {
            LoadDoanhThu(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void btnXemNhapHang_Click(object sender, EventArgs e)
        {
            LoadNhapHang(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void dtgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tpBill_Click(object sender, EventArgs e)
        {

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadFoodList();
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadCategoryList();
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTableFood();
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccountList();
        }

        private void btnShowIngredient_Click(object sender, EventArgs e)
        {
            LoadIngredient();
        }

        private void btnShowFoodIngredient_Click(object sender, EventArgs e)
        {
            LoadFoodIngredient();
        }

        private void btnShowStaff_Click(object sender, EventArgs e)
        {
            LoadStaff();
        }

        private void btnShowSuplier_Click(object sender, EventArgs e)
        {
            LoadSupplier();
        }

        private void btnShowImportReceipt_Click(object sender, EventArgs e)
        {

        }

        private void btnShowImportDetail_Click(object sender, EventArgs e)
        {

        }

        // Binding 

        void AddFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "IdFood", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "FoodName", true, DataSourceUpdateMode.Never));
            nmPriceFood.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));

            // Binding IDCategory với SelectedValue của ComboBox
            cbFoodCategory.DataBindings.Add(new Binding("SelectedValue", dtgvFood.DataSource, "IdCategory", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "categoryName";  // hiển thị tên
            cb.ValueMember = "IdCategory";      // dùng giá trị Id để binding
        }


        public void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int idCategory = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value;
                Category category = CategoryDAO.Instance.GetCategoryById(idCategory);
                cbFoodCategory.SelectedItem = category;

                int index = -1;
                int i = 0;
                foreach (Category item in cbFoodCategory.Items)
                {
                    if (item.IdCategory == idCategory)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string foodName = txbFoodName.Text;
            int idCategory = (cbFoodCategory.SelectedItem as Category).IdCategory;
            float price = (float)nmPriceFood.Value;

            if (FoodDAO.Instance.InsertFood(foodName, idCategory, price))
            {
                MessageBox.Show("Thêm món ăn thành công!");
                LoadFoodList();
            }
            else
            {
                MessageBox.Show("Thêm món ăn thất bại!");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int idFood = Convert.ToInt32(txbFoodID.Text);
            string foodName = txbFoodName.Text;
            int idCategory = (cbFoodCategory.SelectedItem as Category).IdCategory;
            float price = (float)nmPriceFood.Value;

            if (FoodDAO.Instance.UpdateFood(idFood ,foodName, idCategory, price))
            {
                MessageBox.Show("Sửa món ăn thành công!");
                LoadFoodList();
            }
            else
            {
                MessageBox.Show("Sửa món ăn thất bại!");
            }

        }
    }
}
