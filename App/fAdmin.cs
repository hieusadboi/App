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
        BindingSource accountList = new BindingSource();
        BindingSource ingredientList = new BindingSource();
        BindingSource staffList = new BindingSource();
        BindingSource supplierList = new BindingSource();

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
            //dtgvAccount.DataSource = accountList;
            LoadAccountList();
            AddAccountBinding();

            //dtgvFood.DataSource = foodList;
            LoadFoodList();
            AddFoodBinding();
            LoadCategoryIntoComboBox(cbFoodCategory);

            //dtgvCategory.DataSource = categoryList;
            LoadCategoryList();
            AddCategoryBinding();

            //dtgvTableFood.DataSource = tableList;
            LoadTableFood();
            AddTableFoodBinding();

            //dtgvIngredient.DataSource = ingredientList;
            LoadIngredient();
            AddIngredientBinding();

            //dtgvStaff.DataSource = staffList;
            LoadStaff();
            AddBindingStaff();

            LoadFoodIngredient();

            //dtgvSuplier.DataSource = supplierList;
            LoadSupplier();
            AddSupplierBinding();

            LoadDoanhThu(DateTime.Now, DateTime.Now);
        }

        // Load các danh sách từ fooddao
        void LoadFoodList()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
            dtgvFood.DataSource = foodList;

            dtgvFood.Columns["IdFood"].HeaderText = "Mã Món";
            dtgvFood.Columns["FoodName"].HeaderText = "Tên Món";
            dtgvFood.Columns["Price"].HeaderText = "Giá";
            dtgvFood.Columns["IdCategory"].HeaderText = "Mã Danh Mục";
        }

        void LoadAccountList()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
            dtgvAccount.DataSource = accountList;

            dtgvAccount.Columns["UserName"].HeaderText = "Tên Tài Khoản";
            dtgvAccount.Columns["Type"].HeaderText = "Loại Tài Khoản";
            dtgvAccount.Columns["isActive"].HeaderText = "Trạng Thái Hoạt Động";
        }



        void LoadCategoryList()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
            dtgvCategory.DataSource = categoryList;

            dtgvCategory.Columns["idCategory"].HeaderText = "Mã Danh Mục";
            dtgvCategory.Columns["categoryName"].HeaderText = "Tên Danh Mục";
        }

        void LoadTableFood()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();
            dtgvTableFood.DataSource = tableList;

            dtgvTableFood.Columns["id"].HeaderText = "Mã Bàn";
            dtgvTableFood.Columns["Name"].HeaderText = "Tên Bàn";
            dtgvTableFood.Columns["status"].HeaderText = "Trạng Thái";
        }

        void LoadIngredient()
        {
            ingredientList.DataSource = IngredientDAO.Instance.GetListIngredient();
            dtgvIngredient.DataSource = ingredientList;

            dtgvIngredient.Columns["idIngredient"].HeaderText = "Mã Nguyên Liệu";
            dtgvIngredient.Columns["ingredientName"].HeaderText = "Tên Nguyên Liệu";
            dtgvIngredient.Columns["unit"].HeaderText = "Đơn Vị Tính";
            dtgvIngredient.Columns["quantity"].HeaderText = "Số Lượng Tồn Kho";
        }


        void LoadStaff()
        {
            staffList.DataSource = StaffDAO.Instance.GetAllStaff();
            dtgvStaff.DataSource = staffList;

            dtgvStaff.Columns["IdStaff"].HeaderText = "Mã Nhân Viên";
            dtgvStaff.Columns["FullName"].HeaderText = "Họ và Tên";
            dtgvStaff.Columns["Gender"].HeaderText = "Giới tính";
            dtgvStaff.Columns["BirthDate"].HeaderText = "Ngày Sinh";
            dtgvStaff.Columns["Phone"].HeaderText = "Số Điện Thoại";
            dtgvStaff.Columns["Email"].HeaderText = "Email";
            dtgvStaff.Columns["AccountUserName"].HeaderText = "Tên Tài Khoản";
        }


        void LoadFoodIngredient()
        {
            string query = "EXEC GetFoodIngredientSummary";
            dtgvFoodIngredient.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void LoadSupplier()
        {
            supplierList.DataSource = SupplierDAO.Instance.GetAllSuppliers();
            dtgvSuplier.DataSource = supplierList;

            dtgvSuplier.Columns["idSupplier"].HeaderText = "Mã Nhà Cung Cấp";
            dtgvSuplier.Columns["supplierName"].HeaderText = "Tên Nhà Cung Cấp";
            dtgvSuplier.Columns["phone"].HeaderText = "Số Điện Thoại";
            dtgvSuplier.Columns["email"].HeaderText = "Email";
            dtgvSuplier.Columns["address"].HeaderText = "Địa Chỉ";
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


        void AddAccountBinding()
        {
            txbUserNameAccount.DataBindings.Clear();
            cbTypeAccount.DataBindings.Clear();
            cbIsActive.DataBindings.Clear();

            cbTypeAccount.Items.Clear();
            cbIsActive.Items.Clear();

            txbUserNameAccount.DataBindings.Add("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never);

            // Thêm giá trị hiển thị tương ứng
            cbTypeAccount.Items.Add("Nhân viên"); // index 0
            cbTypeAccount.Items.Add("Quản lý"); // index 1

            cbIsActive.Items.Add("Không khả dụng");    // index 0
            cbIsActive.Items.Add("Khả dụng");     // index 1

            // Binding SelectedIndex với dữ liệu số nguyên
            cbTypeAccount.DataBindings.Add("SelectedIndex", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never);
            cbIsActive.DataBindings.Add("SelectedIndex", dtgvAccount.DataSource, "isActive", true, DataSourceUpdateMode.Never);
        }

        void AddIngredientBinding()
        {
            txbIdIngredient.DataBindings.Clear();
            txbIngredientName.DataBindings.Clear();
            // Lấy danh sách các đơn vị duy nhất từ Ingredient
            List<string> units = IngredientDAO.Instance.GetAllUnits();

            // Xóa & thêm lại danh sách đơn vị
            cbUnitIngredient.Items.Clear();
            cbUnitIngredient.Items.AddRange(units.ToArray()); nmQuantity.DataBindings.Clear();
            txbIdIngredient.DataBindings.Add("Text", dtgvIngredient.DataSource, "idIngredient", true, DataSourceUpdateMode.Never);
            txbIngredientName.DataBindings.Add("Text", dtgvIngredient.DataSource, "ingredientName", true, DataSourceUpdateMode.Never);
            cbUnitIngredient.DataBindings.Add("SelectedItem", dtgvIngredient.DataSource, "unit", true, DataSourceUpdateMode.Never);
            nmQuantity.DataBindings.Add("Value", dtgvIngredient.DataSource, "quantity", true, DataSourceUpdateMode.Never);
        }

        void AddBindingStaff()
        {
            // Clear old bindings to avoid duplication errors
            txbIdStaff.DataBindings.Clear();
            txbFullName.DataBindings.Clear(); // Sử dụng txbFullName thay vì txbNameStaff
            cbGender.DataBindings.Clear();
            dtpkBirthDateStaff.DataBindings.Clear();
            txbPhoneStaff.DataBindings.Clear();
            txbEmailStaff.DataBindings.Clear();
            cbAccountStaff.DataBindings.Clear();

            // Khởi tạo danh sách item cho cbGender (giả định "Nam" và "Nữ")
            cbGender.Items.Clear();
            cbGender.Items.Add("Nam");
            cbGender.Items.Add("Nữ");

            // Khởi tạo danh sách item cho cbAccountStaff từ bảng Account
            cbAccountStaff.Items.Clear();
            var accountList = AccountDAO.Instance.GetListAccount(); // Lấy danh sách tài khoản
            foreach (DataRow row in accountList.Rows)
            {
                string username = row["UserName"].ToString();
                if (!string.IsNullOrEmpty(username)) // Kiểm tra giá trị không rỗng
                {
                    cbAccountStaff.Items.Add(username);
                }
            }

            // Add new bindings from DataGridView's DataSource
            txbIdStaff.DataBindings.Add("Text", dtgvStaff.DataSource, "IdStaff", true, DataSourceUpdateMode.Never);
            txbFullName.DataBindings.Add("Text", dtgvStaff.DataSource, "FullName", true, DataSourceUpdateMode.Never);
            cbGender.DataBindings.Add("SelectedItem", dtgvStaff.DataSource, "Gender", true, DataSourceUpdateMode.Never);
            dtpkBirthDateStaff.DataBindings.Add("Value", dtgvStaff.DataSource, "BirthDate", true, DataSourceUpdateMode.Never);
            txbPhoneStaff.DataBindings.Add("Text", dtgvStaff.DataSource, "Phone", true, DataSourceUpdateMode.Never);
            txbEmailStaff.DataBindings.Add("Text", dtgvStaff.DataSource, "Email", true, DataSourceUpdateMode.Never);
            cbAccountStaff.DataBindings.Add("SelectedItem", dtgvStaff.DataSource, "AccountUserName", true, DataSourceUpdateMode.Never);
        }

        void AddSupplierBinding()
        {
            txbIdSupplier.DataBindings.Clear();
            txbSupplierName.DataBindings.Clear();
            txbPhone.DataBindings.Clear();
            txbEmail.DataBindings.Clear();
            txbAddress.DataBindings.Clear();

            txbIdSupplier.DataBindings.Add("Text", dtgvSuplier.DataSource, "idSupplier", true, DataSourceUpdateMode.Never);
            txbSupplierName.DataBindings.Add("Text", dtgvSuplier.DataSource, "supplierName", true, DataSourceUpdateMode.Never);
            txbPhone.DataBindings.Add("Text", dtgvSuplier.DataSource, "phone", true, DataSourceUpdateMode.Never);
            txbEmail.DataBindings.Add("Text", dtgvSuplier.DataSource, "email", true, DataSourceUpdateMode.Never);
            txbAddress.DataBindings.Add("Text", dtgvSuplier.DataSource, "address", true, DataSourceUpdateMode.Never);
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
            this.Validate(); // hoặc gọi WriteValue() từng cái

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

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserNameAccount.Text;
            int type = cbTypeAccount.SelectedIndex; // 1: Admin, 0: Staff
            bool isActive = cbIsActive.SelectedIndex == 1; // True: 1, False: 0

            if (AccountDAO.Instance.InsertAccount(username, type, isActive))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
                LoadAccountList(); // reload lại DataGridView
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!");
            }
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserNameAccount.Text;
            int type = cbTypeAccount.SelectedIndex; // 1: Admin, 0: Staff
            bool isActive = cbIsActive.SelectedIndex == 1; // True: 1, False: 0

            if (AccountDAO.Instance.UpdateAccount(username, type, isActive))
            {
                MessageBox.Show("Sửa tài khoản thành công!");
                LoadAccountList(); // reload lại DataGridView
            }
            else
            {
                MessageBox.Show("Sửa tài khoản thất bại!");
            }
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            string name = txbIngredientName.Text.Trim();
            string unit = cbUnitIngredient.Text.Trim();
            decimal quantity = nmQuantity.Value;

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên nguyên liệu!");
                return;
            }

            if (string.IsNullOrEmpty(unit))
            {
                MessageBox.Show("Vui lòng chọn đơn vị!");
                return;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!");
                return;
            }

            if (IngredientDAO.Instance.InsertIngredient(name, unit, quantity))
            {
                MessageBox.Show("Thêm nguyên liệu thành công!");
                LoadIngredient(); // Reload lại datagrid
            }
            else
            {
                MessageBox.Show("Thêm nguyên liệu thất bại!");
            }
        }

        private void btnEditIngredient_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txbIdIngredient.Text, out int id))
            {
                MessageBox.Show("ID không hợp lệ!");
                return;
            }

            if (!IngredientDAO.Instance.IsIngredientExist(id))
            {
                MessageBox.Show("Không tìm thấy nguyên liệu có ID này!");
                return;
            }

            string name = txbIngredientName.Text;
            string unit = cbUnitIngredient.Text;
            decimal quantity = nmQuantity.Value;

            if (IngredientDAO.Instance.UpdateIngredient(id, name, unit, quantity))
            {
                MessageBox.Show("Cập nhật nguyên liệu thành công!");
                LoadIngredient();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void btnDeleteIngredient_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txbIdIngredient.Text, out int id))
            {
                MessageBox.Show("ID không hợp lệ!");
                return;
            }

            if (!IngredientDAO.Instance.IsIngredientExist(id))
            {
                MessageBox.Show("Không tìm thấy nguyên liệu có ID này!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa nguyên liệu này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (IngredientDAO.Instance.DeleteIngredient(id))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadIngredient();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }


        private void btnLoadUnusedAccounts_Click(object sender, EventArgs e)
        {
            UpdateAccountComboBox();
            MessageBox.Show("Đã cập nhật danh sách tài khoản trống!");
        }

        // Phương thức hỗ trợ để cập nhật ComboBox tài khoản
        private void UpdateAccountComboBox()
        {
            cbAccountStaff.Items.Clear();
            var unusedAccounts = StaffDAO.Instance.GetAccountsWithoutStaff();
            foreach (string username in unusedAccounts)
            {
                if (!string.IsNullOrEmpty(username))
                {
                    cbAccountStaff.Items.Add(username);
                }
            }
        }

        private void btnAddStaff_Click_1(object sender, EventArgs e)
        {
            this.Validate(); // Đảm bảo dữ liệu được đồng bộ từ binding

            string fullName = txbFullName.Text;
            string gender = cbGender.SelectedItem?.ToString();
            DateTime birthDate = dtpkBirthDateStaff.Value;
            string phone = txbPhoneStaff.Text;
            string email = txbEmailStaff.Text;
            string accountUserName = cbAccountStaff.SelectedItem?.ToString();

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(accountUserName))
            {
                MessageBox.Show("Họ và tên, giới tính, và tài khoản không được để trống!");
                return;
            }

            if (StaffDAO.Instance.InsertStaff(new Staff
            {
                FullName = fullName,
                Gender = gender,
                BirthDate = birthDate,
                Phone = phone,
                Email = email,
                AccountUserName = accountUserName
            }))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadStaff();
                AddBindingStaff(); // Cập nhật binding sau khi thêm
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại!");
            }
        }

        private void btnUpdateStaff_Click_1(object sender, EventArgs e)
        {
            this.Validate(); // Đảm bảo dữ liệu được đồng bộ từ binding

            int idStaff = Convert.ToInt32(txbIdStaff.Text);
            string fullName = txbFullName.Text;
            string gender = cbGender.SelectedItem?.ToString();
            DateTime birthDate = dtpkBirthDateStaff.Value;
            string phone = txbPhoneStaff.Text;
            string email = txbEmailStaff.Text;
            string accountUserName = cbAccountStaff.SelectedItem?.ToString();

            if (StaffDAO.Instance.UpdateStaff(new Staff
            {
                IdStaff = idStaff,
                FullName = fullName,
                Gender = gender,
                BirthDate = birthDate,
                Phone = phone,
                Email = email,
                AccountUserName = accountUserName
            }))
            {
                MessageBox.Show("Sửa nhân viên thành công!");
                LoadStaff();
                AddBindingStaff(); // Cập nhật binding sau khi sửa
            }
            else
            {
                MessageBox.Show("Sửa nhân viên thất bại!");
            }
        }

        private void btnDeleteStaff_Click_1(object sender, EventArgs e)
        {
            this.Validate();

            int idStaff = Convert.ToInt32(txbIdStaff.Text);

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên với ID {idStaff} không?\n\n" +
                "⚠️ Việc xóa sẽ dẫn đến mất toàn bộ thông tin liên quan.",
                "Xác nhận xóa nhân viên",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (StaffDAO.Instance.DeleteStaffById(idStaff))
                {
                    MessageBox.Show("Đã xóa nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStaff();
                    AddBindingStaff(); // Cập nhật binding sau khi xóa
                }
                else
                {
                    MessageBox.Show("Xóa nhân viên thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Đã hủy thao tác xóa nhân viên.", "Đã hủy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            this.Validate();

            string supplierName = txbSupplierName.Text;
            string phone = txbPhone.Text;
            string email = txbEmail.Text;
            string address = txbAddress.Text;

            if (string.IsNullOrEmpty(supplierName))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!");
                return;
            }

            if (SupplierDAO.Instance.InsertSupplier(new Supplier
            {
                SupplierName = supplierName,
                Phone = phone,
                Email = email,
                Address = address
            }))
            {
                MessageBox.Show("Thêm nhà cung cấp thành công!");
                LoadSupplier();
                AddSupplierBinding();
            }
            else
            {
                MessageBox.Show("Thêm nhà cung cấp thất bại!");
            }
        }

        private void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            this.Validate();

            int idSupplier = Convert.ToInt32(txbIdSupplier.Text);
            string supplierName = txbSupplierName.Text;
            string phone = txbPhone.Text;
            string email = txbEmail.Text;
            string address = txbAddress.Text;

            if (string.IsNullOrEmpty(supplierName))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!");
                return;
            }

            if (SupplierDAO.Instance.UpdateSupplier(new Supplier
            {
                IdSupplier = idSupplier,
                SupplierName = supplierName,
                Phone = phone,
                Email = email,
                Address = address
            }))
            {
                MessageBox.Show("Sửa nhà cung cấp thành công!");
                LoadSupplier();
                AddSupplierBinding();
            }
            else
            {
                MessageBox.Show("Sửa nhà cung cấp thất bại!");
            }
        }

        private void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            this.Validate();

            int idSupplier = Convert.ToInt32(txbIdSupplier.Text);

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhà cung cấp với ID {idSupplier} không?\n\n" +
                "⚠️ Việc xóa sẽ dẫn đến mất toàn bộ thông tin liên quan.",
                "Xác nhận xóa nhà cung cấp",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (SupplierDAO.Instance.DeleteSupplier(idSupplier))
                {
                    MessageBox.Show("Đã xóa nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplier();
                    AddSupplierBinding();
                }
                else
                {
                    MessageBox.Show("Xóa nhà cung cấp thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Đã hủy thao tác xóa nhà cung cấp.", "Đã hủy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }





    }
}
