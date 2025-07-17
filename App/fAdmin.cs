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
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();

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

            dtgvCategory.DataSource = categoryList;
            LoadCategoryList();
            AddCategoryBinding();

            dtgvTableFood.DataSource = tableList;
            LoadTableFood();
            AddTableFoodBinding();

            LoadIngredient();

            LoadStaff();

            LoadFoodIngredient();

            LoadSupplier();

            LoadDoanhThu(DateTime.Now, DateTime.Now);
        }

        // Load các danh sách từ fooddao
        void LoadFoodList()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();

            dtgvFood.DataSource = foodList;
            // Cấu hình header text cho DataGridView
            dtgvFood.Columns["IdFood"].HeaderText = "Mã Món";
            dtgvFood.Columns["FoodName"].HeaderText = "Tên Món";
            dtgvFood.Columns["Price"].HeaderText = "Giá";
            dtgvFood.Columns["IdCategory"].HeaderText = "Mã Danh Mục";
        }

        void LoadAccountList()
        {
            string query = "EXEC dbo.USP_GetAccount";

            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        // dùng proc để load 
        //void LoadCategoryList()
        //{
        //    string query = "USP_GetFoodCategory";
        //    //dtgvCategory.DataSource = DataProvider.Instance.ExecuteQuery(query);
        //    categoryList.DataSource = DataProvider.Instance.ExecuteQuery(query);
        //}

        // dùng hàm GetListCategory trong CategoryDAO để load
        void LoadCategoryList()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();

            dtgvCategory.DataSource = categoryList;

            // Cấu hình header text cho DataGridView
            dtgvCategory.Columns["idCategory"].HeaderText = "Mã Danh Mục";
            dtgvCategory.Columns["categoryName"].HeaderText = "Tên Danh Mục";
        }

        void LoadTableFood()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();

            dtgvTableFood.DataSource = tableList;

            // Cấu hình header text cho DataGridView
            dtgvTableFood.Columns["id"].HeaderText = "Mã Bàn";
            dtgvTableFood.Columns["Name"].HeaderText = "Tên Bàn";
            dtgvTableFood.Columns["status"].HeaderText = "Trạng Thái";
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
            txbFoodID.DataBindings.Clear();
            txbFoodName.DataBindings.Clear();
            nmPriceFood.DataBindings.Clear();
            cbFoodCategory.DataBindings.Clear();

            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "IdFood", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "FoodName", true, DataSourceUpdateMode.Never));
            nmPriceFood.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));

            // Binding IDCategory với SelectedValue của ComboBox
            cbFoodCategory.DataBindings.Add(new Binding("SelectedValue", dtgvFood.DataSource, "IdCategory", true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBinding()
        {
            txbIdCategory.DataBindings.Clear();
            txbCategoryName.DataBindings.Clear();

            txbIdCategory.DataBindings.Add("Text", dtgvCategory.DataSource, "idCategory", true, DataSourceUpdateMode.Never);
            txbCategoryName.DataBindings.Add("Text", dtgvCategory.DataSource, "categoryName", true, DataSourceUpdateMode.Never);
        }


        void AddTableFoodBinding()
        {
            txbIdTable.DataBindings.Clear();
            txbTableName.DataBindings.Clear();
            cbTableStatus.Items.Clear();

            txbIdTable.DataBindings.Add("Text", dtgvTableFood.DataSource, "id", true, DataSourceUpdateMode.Never);
            txbTableName.DataBindings.Add("Text", dtgvTableFood.DataSource, "Name", true, DataSourceUpdateMode.Never);
            // Gán sẵn danh sách trạng thái nếu cần
            cbTableStatus.Items.Add("Trống");
            cbTableStatus.Items.Add("Có Người");

            // Binding vào SelectedItem để không bị lỗi
            cbTableStatus.DataBindings.Add("SelectedItem", dtgvTableFood.DataSource, "status", true, DataSourceUpdateMode.Never);
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
            this.Validate(); // hoặc gọi WriteValue() từng cái
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
            this.Validate(); // hoặc gọi WriteValue() từng cái

            int idFood = Convert.ToInt32(txbFoodID.Text);
            string foodName = txbFoodName.Text;
            int idCategory = (cbFoodCategory.SelectedItem as Category).IdCategory;
            float price = (float)nmPriceFood.Value;

            if (FoodDAO.Instance.UpdateFood(idFood, foodName, idCategory, price))
            {
                MessageBox.Show("Sửa món ăn thành công!");
                LoadFoodList();
            }
            else
            {
                MessageBox.Show("Sửa món ăn thất bại!");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            this.Validate();

            // Lấy thông tin món ăn cần xóa
            int idFood = Convert.ToInt32(txbFoodID.Text);
            string foodName = txbFoodName.Text;

            // Hỏi người dùng có chắc chắn muốn xóa không
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa món \"{foodName}\" không?\n\n" +
                "⚠️ Việc xóa sẽ dẫn đến mất toàn bộ thông tin liên quan như nguyên liệu, hóa đơn,...",
                "Xác nhận xóa món ăn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Nếu người dùng chọn Yes thì mới tiến hành xóa
            if (result == DialogResult.Yes)
            {
                if (FoodDAO.Instance.DeleteFood(idFood))
                {
                    MessageBox.Show("✅ Đã xóa món ăn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFoodList();
                }
                else
                {
                    MessageBox.Show("❌ Xóa món ăn thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("❎ Đã hủy thao tác xóa món ăn.", "Đã hủy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            this.Validate(); // hoặc gọi WriteValue() từng cái

            string categoryName = txbCategoryName.Text;

            if (CategoryDAO.Instance.InsertCategory(categoryName))
            {
                MessageBox.Show("Thêm doanh mục món ăn  thành công!");
                LoadCategoryList();
            }
            else
            {
                MessageBox.Show("Thêm doanh mục món ăn thất bại!");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            this.Validate(); // hoặc gọi WriteValue() từng cái

            int idCategory = Convert.ToInt32(txbIdCategory.Text);
            string categoryName = txbCategoryName.Text;
            if (CategoryDAO.Instance.UpdateCategory(idCategory, categoryName))
            {
                MessageBox.Show("Sửa doanh mục món ăn thành công!");
                LoadCategoryList();
            }
            else
            {
                MessageBox.Show("Sửa doanh mục món ăn thất bại!");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int idCategory = Convert.ToInt32(txbIdCategory.Text);

            // Xác nhận xóa
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa danh mục này không?\nLưu ý: Danh mục sẽ không được xóa nếu còn món ăn thuộc về nó.",
                "Xác nhận xóa",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.OK)
            {
                bool success = CategoryDAO.Instance.DeleteCategory(idCategory);

                if (success)
                {
                    MessageBox.Show("Xóa danh mục thành công!");
                    LoadCategoryList(); // Load lại danh sách danh mục nếu có
                }
                else
                {
                    MessageBox.Show("Không thể xóa danh mục vì còn món ăn thuộc danh mục này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            this.Validate(); // hoặc gọi WriteValue() từng cái

            string tableName = txbTableName.Text.Trim(); // Lấy tên bàn từ textbox

            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Tên bàn không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TableDAO.Instance.InsertTable(tableName))
            {
                MessageBox.Show("Thêm bàn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTableFood(); // Tải lại danh sách bàn
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            this.Validate(); // hoặc gọi WriteValue() từng cái

            int idTable = Convert.ToInt32(txbIdTable.Text);

            string tableName = txbTableName.Text.Trim(); // Lấy tên bàn từ textbox

            string status = cbTableStatus.SelectedItem?.ToString(); // Lấy trạng thái bàn từ combobox

            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Tên bàn và trạng thái không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TableDAO.Instance.UpdateTable(idTable, tableName, status))
            {
                MessageBox.Show("Sửa thông tin bàn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTableFood(); // Tải lại danh sách bàn
            }
            else
            {
                MessageBox.Show("Sửa thông tin bàn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int idTable = Convert.ToInt32(txbIdTable.Text);
            if (TableDAO.Instance.DeleteTable(idTable))
            {
                LoadTableFood();
            }
            
        }
    }
}
