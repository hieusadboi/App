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
using System.Text.RegularExpressions;
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
        BindingSource foodList1 = new BindingSource();
        BindingSource ingredientBinding = new BindingSource();
        BindingSource receiptBindingSource = new BindingSource();
        BindingSource detailBindingSource = new BindingSource();

        public fAdmin()
        {
            InitializeComponent();
            LoadALL();
        }

        void LoadALL()
        {
            LoadDoanhThu(DateTime.Today, DateTime.Now);

            LoadAccountList();
            AddAccountBinding();

            LoadFoodList();
            AddFoodBinding();
            LoadCategoryIntoComboBox(cbFoodCategory);

            LoadCategoryList();
            AddCategoryBinding();

            LoadTableFood();
            AddTableFoodBinding();

            LoadIngredient();
            AddIngredientBinding();

            LoadStaff();
            AddBindingStaff();

            LoadFoodIngredient();

            LoadSupplier();
            AddSupplierBinding();

            LoadImportReceiptAndDetail();
        }


        #region Tải DataGridView từ DAO
        void LoadDoanhThu(DateTime fromDate, DateTime toDate)
        {
            //InitDoanhThuColumns();
            string query = "EXEC DoanhThu @FromDate , @ToDate ";
            dtgvDoanhThu.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
        }
    
        void LoadChiTiet(object id, string loai)
        {
            DataTable data;

            if (loai == "Thu") // Bill
            {
                string query = "EXEC GetBillDetails @BillID ";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });
            }
            else if (loai == "Chi") // Phiếu nhập
            {
                string query = "EXEC GetImportDetails @ReceiptID ";
                data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });
            }
            else
            {
                dtgvChiTiet.DataSource = null;
                return;
            }

            dtgvChiTiet.DataSource = data;
        }

        void loadFoodHot(DateTime fromDate, DateTime toDate)
        {
            DataTable data;
            string query = "EXEC MonAnBanChay @fromDate , @toDate ";
            data = DataProvider.Instance.ExecuteQuery(query, new object[] { fromDate, toDate });
            dtgvDoanhThu.DataSource = data;
        }

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
            fTableManager.ShowLowStockWarning();
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

        #endregion


        #region  Tải định lượng món ăn
        void LoadFoodIngredient()
        {
            // Load danh sách món ăn
            foodList1.DataSource = FoodDAO.Instance.GetListFood();
            dtgvFood1.DataSource = foodList1;
            dtgvFood1.Columns["IdFood"].HeaderText = "Mã Món";
            dtgvFood1.Columns["FoodName"].HeaderText = "Tên Món";
            dtgvFood1.Columns["Price"].Visible = false;
            dtgvFood1.Columns["IdCategory"].Visible = false;
            dtgvFood1.Columns["IdFood"].Width = 120;
            dtgvFood1.Columns["FoodName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            // Binding món ăn vào ComboBox và TextBox
            LoadFoodNameToComboBox();
            AddFoodBinding1();

            // Load nguyên liệu của món đầu tiên (nếu có)
            if (dtgvFood1.Rows.Count > 0)
            {
                dtgvFood1.Rows[0].Selected = true;
                LoadFoodIngredientDetail_ByCurrentSelection();
            }
            else
            {
                ingredientBinding.DataSource = null;
                dtgvFoodIngredient.DataSource = null;
            }

            // Binding nguyên liệu
            AddIngredientBinding1();
        }

        void LoadFoodIngredientDetail_ByCurrentSelection()
        {
            if (!string.IsNullOrEmpty(txbIdFoodIngredient.Text) && int.TryParse(txbIdFoodIngredient.Text, out int idFood))
            {
                List<FoodIngredient> ingredientList = FoodIngredientDAO.Instance.GetIngredientsByFoodId(idFood);
                if (ingredientList != null && ingredientList.Count > 0)
                {
                    // Debug: Kiểm tra dữ liệu
                    Console.WriteLine("Danh sách nguyên liệu:");
                    foreach (var item in ingredientList)
                    {
                        Console.WriteLine($"IdIngredient: {item.IdIngredient}, Name: {item.IngredientName}, Unit: {item.Unit}, Quantity: {item.Quantity}");
                    }

                    ingredientBinding.DataSource = ingredientList;
                    dtgvFoodIngredient.DataSource = ingredientBinding;
                    dtgvFoodIngredient.Columns["IdIngredient"].HeaderText = "Mã Nguyên Liệu";
                    dtgvFoodIngredient.Columns["IngredientName"].HeaderText = "Tên Nguyên Liệu";
                    dtgvFoodIngredient.Columns["Unit"].HeaderText = "Đơn Vị";
                    dtgvFoodIngredient.Columns["Quantity"].HeaderText = "Số Lượng";
                    dtgvFoodIngredient.Columns["IdFood"].Visible = false; // Ẩn cột IdFood nếu không cần thiết
                }
                else
                {
                    // Xóa dữ liệu cũ và làm mới DataGridView
                    dtgvFoodIngredient.Rows.Clear(); // Xóa các hàng hiển thị
                    dtgvFoodIngredient.Refresh();   // Làm mới giao diện

                    // Reset các control về trạng thái trống
                    txbIdbingredient.Text = "";
                    cbNameIngredient.SelectedIndex = -1;
                    cbUnitIngredient1.SelectedIndex = -1;
                    nmQuantityFoodIngredient.Value = 0;

                    MessageBox.Show("Không có nguyên liệu nào cho món ăn này!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                ingredientBinding.DataSource = null;
                dtgvFoodIngredient.DataSource = null;
                dtgvFoodIngredient.Rows.Clear();
                dtgvFoodIngredient.Refresh();

                // Reset các control về trạng thái trống
                txbIdbingredient.Text = "";
                cbNameIngredient.SelectedIndex = -1;
                cbUnitIngredient1.SelectedIndex = -1;
                nmQuantityFoodIngredient.Value = 1;

                //MessageBox.Show("Vui lòng chọn một món ăn hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dtgvFood1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.Validate();
                LoadFoodIngredientDetail_ByCurrentSelection();
                AddIngredientBinding1();
            }
        }

        #endregion


        #region các sự kiện click cho doanh thu và các nút reload lại dtgv
        private void btnXemDoanhThu_Click(object sender, EventArgs e)
        {
            LoadDoanhThu(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void dtgvDoanhThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                string loai = dtgvDoanhThu.Rows[e.RowIndex].Cells["Loại"].Value.ToString();
                string id = dtgvDoanhThu.Rows[e.RowIndex].Cells["Mã HĐ/PN"].Value.ToString();

                // Nếu là tổng kết thì bỏ qua
                if (loai == "Tổng kết" || string.IsNullOrEmpty(id))
                {
                    dtgvChiTiet.DataSource = null;
                    return;
                }

                LoadChiTiet(id, loai);
            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void btnShowFoodHot_Click(object sender, EventArgs e)
        {
            loadFoodHot(dtpkFromDate.Value, dtpkToDate.Value);
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

        private void btnShowImportReceiptANDDetail_Click(object sender, EventArgs e)
        {
            LoadImportReceiptAndDetail();
            txbSearchReceipt.Clear();
        }


        #endregion


        #region Binding Food, Category, Table, Account, Ingredient, Staff, Supplier
        //void AddFoodBinding()
        //{
        //    txbFoodID.DataBindings.Clear();
        //    txbFoodName.DataBindings.Clear();
        //    nmPriceFood.DataBindings.Clear();
        //    cbFoodCategory.DataBindings.Clear();

        //    txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "IdFood", true, DataSourceUpdateMode.Never));
        //    txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "FoodName", true, DataSourceUpdateMode.Never));
        //    nmPriceFood.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));

        //    // Binding IDCategory với SelectedValue của ComboBox
        //    cbFoodCategory.DataBindings.Add(new Binding("SelectedValue", dtgvFood.DataSource, "IdCategory", true, DataSourceUpdateMode.Never));
        //}

        void AddFoodBinding()
        {
            txbFoodID.DataBindings.Clear();
            txbFoodName.DataBindings.Clear();
            nmPriceFood.DataBindings.Clear();
            cbFoodCategory.DataBindings.Clear();

            txbFoodID.DataBindings.Add("Text", foodList, "IdFood", true, DataSourceUpdateMode.Never);
            txbFoodName.DataBindings.Add("Text", foodList, "FoodName", true, DataSourceUpdateMode.Never);
            nmPriceFood.DataBindings.Add("Value", foodList, "Price", true, DataSourceUpdateMode.Never);
            cbFoodCategory.DataBindings.Add("SelectedValue", foodList, "IdCategory", true, DataSourceUpdateMode.Never);
        }


        // khi đổi chọn món ăn trong DataGridView, cập nhật Category tương ứng
        //public void txbFoodID_TextChanged(object sender, EventArgs e)
        //{
        //    if (dtgvFood.SelectedCells.Count > 0)
        //    {
        //        int idCategory = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IdCategory"].Value;
        //        Category category = CategoryDAO.Instance.GetCategoryById(idCategory);
        //        cbFoodCategory.SelectedItem = category;

        //        int index = -1;
        //        int i = 0;
        //        foreach (Category item in cbFoodCategory.Items)
        //        {
        //            if (item.IdCategory == idCategory)
        //            {
        //                index = i;
        //                break;
        //            }
        //            i++;
        //        }
        //    }
        //}
        public void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count == 0)
                    return;

                DataGridViewRow selectedRow = dtgvFood.SelectedCells[0].OwningRow;
                if (selectedRow == null || selectedRow.Cells["IdCategory"].Value == null)
                    return;

                int idCategory;
                if (!int.TryParse(selectedRow.Cells["IdCategory"].Value.ToString(), out idCategory))
                    return;

                Category category = CategoryDAO.Instance.GetCategoryById(idCategory);
                if (category == null)
                    return;

                cbFoodCategory.SelectedItem = category;

                // Chỉ chọn nếu chưa đúng
                for (int i = 0; i < cbFoodCategory.Items.Count; i++)
                {
                    if (((Category)cbFoodCategory.Items[i]).IdCategory == idCategory)
                    {
                        cbFoodCategory.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn danh mục: " + ex.Message);
            }
        }


        //void AddCategoryBinding()
        //{
        //    txbIdCategory.DataBindings.Clear();
        //    txbCategoryName.DataBindings.Clear();

        //    txbIdCategory.DataBindings.Add("Text", dtgvCategory.DataSource, "idCategory", true, DataSourceUpdateMode.Never);
        //    txbCategoryName.DataBindings.Add("Text", dtgvCategory.DataSource, "categoryName", true, DataSourceUpdateMode.Never);
        //}
        void AddCategoryBinding()
        {
            txbIdCategory.DataBindings.Clear();
            txbCategoryName.DataBindings.Clear();

            // Dùng categoryList thay vì dtgvCategory.DataSource
            txbIdCategory.DataBindings.Add("Text", categoryList, "idCategory", true, DataSourceUpdateMode.Never);
            txbCategoryName.DataBindings.Add("Text", categoryList, "categoryName", true, DataSourceUpdateMode.Never);
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


        //void AddAccountBinding()
        //{
        //    txbUserNameAccount.DataBindings.Clear();
        //    cbTypeAccount.DataBindings.Clear();
        //    cbIsActive.DataBindings.Clear();

        //    cbTypeAccount.Items.Clear();
        //    cbIsActive.Items.Clear();

        //    txbUserNameAccount.DataBindings.Add("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never);

        //    // Thêm giá trị hiển thị tương ứng
        //    cbTypeAccount.Items.Add("Nhân viên"); // index 0
        //    cbTypeAccount.Items.Add("Quản lý"); // index 1

        //    cbIsActive.Items.Add("Không khả dụng");    // index 0
        //    cbIsActive.Items.Add("Khả dụng");     // index 1

        //    // Binding SelectedIndex với dữ liệu số nguyên
        //    cbTypeAccount.DataBindings.Add("SelectedIndex", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never);
        //    cbIsActive.DataBindings.Add("SelectedIndex", dtgvAccount.DataSource, "isActive", true, DataSourceUpdateMode.Never);
        //}

        void AddAccountBinding()
        {
            txbUserNameAccount.DataBindings.Clear();
            txbUserNameAccount.DataBindings.Add("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never);

            cbTypeAccount.Items.Clear();
            cbTypeAccount.Items.Add("Nhân viên"); // 0
            cbTypeAccount.Items.Add("Quản lý");   // 1

            cbIsActive.Items.Clear();
            cbIsActive.Items.Add("Không khả dụng"); // 0
            cbIsActive.Items.Add("Khả dụng");       // 1
            cbIsActive.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTypeAccount.DropDownStyle = ComboBoxStyle.DropDownList;

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

        //void AddIngredientBinding()
        //{
        //    txbIdIngredient.DataBindings.Clear();
        //    txbIngredientName.DataBindings.Clear();
        //    // Lấy danh sách các đơn vị duy nhất từ Ingredient
        //    List<string> units = IngredientDAO.Instance.GetAllUnits();

        //    // Xóa & thêm lại danh sách đơn vị
        //    cbUnitIngredient.Items.Clear();
        //    cbUnitIngredient.Items.AddRange(units.ToArray()); nmQuantity.DataBindings.Clear();
        //    txbIdIngredient.DataBindings.Add("Text", dtgvIngredient.DataSource, "idIngredient", true, DataSourceUpdateMode.Never);
        //    txbIngredientName.DataBindings.Add("Text", dtgvIngredient.DataSource, "ingredientName", true, DataSourceUpdateMode.Never);
        //    cbUnitIngredient.DataBindings.Add("SelectedItem", dtgvIngredient.DataSource, "unit", true, DataSourceUpdateMode.Never);
        //    nmQuantity.DataBindings.Add("Value", dtgvIngredient.DataSource, "quantity", true, DataSourceUpdateMode.Never);
        //}

        void AddIngredientBinding()
        {
            txbIdIngredient.DataBindings.Clear();
            txbIngredientName.DataBindings.Clear();
            cbUnitIngredient.DataBindings.Clear();
            nmQuantity.DataBindings.Clear();

            // Lấy danh sách các đơn vị duy nhất từ Ingredient
            List<string> units = IngredientDAO.Instance.GetAllUnits();
            cbUnitIngredient.Items.Clear();
            cbUnitIngredient.Items.AddRange(units.ToArray());

            txbIdIngredient.DataBindings.Add("Text", dtgvIngredient.DataSource, "idIngredient", true, DataSourceUpdateMode.Never);
            txbIngredientName.DataBindings.Add("Text", dtgvIngredient.DataSource, "ingredientName", true, DataSourceUpdateMode.Never);
            cbUnitIngredient.DataBindings.Add("SelectedItem", dtgvIngredient.DataSource, "unit", true, DataSourceUpdateMode.Never);
            nmQuantity.DataBindings.Add("Value", dtgvIngredient.DataSource, "quantity", true, DataSourceUpdateMode.Never);
        }

        #endregion


        #region Binding định lượng nguyên liệu của món ăn
        // cho định lượng nguyên liệu của món ăn (vào combobox tên món)
        void LoadFoodNameToComboBox()
        {
            List<Food> foodList = FoodDAO.Instance.GetListFood();
            cbNameFoodIngredient.DataSource = foodList;

            cbNameFoodIngredient.DisplayMember = "FoodName"; // Tên hiển thị
            cbNameFoodIngredient.ValueMember = "IdFood";     // Giá trị thực tế
        }

        // cho định lượng nguyên liệu của món ăn
        void AddFoodBinding1()
        {
            txbIdFoodIngredient.DataBindings.Clear();
            cbNameFoodIngredient.DataBindings.Clear();

            // Binding ID món
            txbIdFoodIngredient.DataBindings.Add("Text", dtgvFood1.DataSource, "IdFood", true, DataSourceUpdateMode.Never);

            cbNameFoodIngredient.DataBindings.Add("SelectedValue", dtgvFood1.DataSource, "IdFood", true, DataSourceUpdateMode.Never);
        }

        // cho định lượng nguyên liệu của món ăn
        void AddIngredientBinding1()
        {
            // Xóa tất cả binding cũ
            txbIdbingredient.DataBindings.Clear();
            cbNameIngredient.DataBindings.Clear();
            cbUnitIngredient1.DataBindings.Clear();
            nmQuantityFoodIngredient.DataBindings.Clear();

            // Reset ComboBox DataSource và Items
            cbNameIngredient.DataSource = null;
            cbUnitIngredient1.Items.Clear();

            // Load danh sách nguyên liệu và đơn vị vào ComboBox
            List<Ingredient> ingredientList = IngredientDAO.Instance.GetListIngredient();
            cbNameIngredient.DataSource = ingredientList;
            cbNameIngredient.DisplayMember = "IngredientName";
            cbNameIngredient.ValueMember = "IdIngredient";

            List<string> units = IngredientDAO.Instance.GetAllUnits();
            cbUnitIngredient1.Items.AddRange(units.ToArray());

            // Binding nếu DataSource không rỗng
            if (dtgvFoodIngredient.DataSource != null && dtgvFoodIngredient.Rows.Count > 0)
            {
                try
                {
                    txbIdbingredient.DataBindings.Add("Text", dtgvFoodIngredient.DataSource, "IdIngredient", true, DataSourceUpdateMode.Never);
                    cbNameIngredient.DataBindings.Add("SelectedValue", dtgvFoodIngredient.DataSource, "IdIngredient", true, DataSourceUpdateMode.Never);
                    cbUnitIngredient1.DataBindings.Add("SelectedItem", dtgvFoodIngredient.DataSource, "Unit", true, DataSourceUpdateMode.Never);
                    nmQuantityFoodIngredient.DataBindings.Add("Value", dtgvFoodIngredient.DataSource, "Quantity", true, DataSourceUpdateMode.Never);

                    // Đặt giá trị ban đầu cho ComboBox
                    if (dtgvFoodIngredient.CurrentRow != null)
                    {
                        cbNameIngredient.SelectedValue = dtgvFoodIngredient.CurrentRow.Cells["IdIngredient"].Value;
                        cbUnitIngredient1.SelectedItem = dtgvFoodIngredient.CurrentRow.Cells["Unit"].Value?.ToString();
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Lỗi binding: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Reset tất cả control về trạng thái trống khi không có dữ liệu
                txbIdbingredient.Text = "";
                cbNameIngredient.SelectedIndex = -1;  // Bỏ chọn nguyên liệu
                cbUnitIngredient1.SelectedIndex = -1; // Bỏ chọn đơn vị
                nmQuantityFoodIngredient.Value = 1;
                //MessageBox.Show("Không có dữ liệu nguyên liệu để binding!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Gắn sự kiện cho cbNameIngredient
            cbNameIngredient.SelectedIndexChanged += (sender, e) => LoadIngredientDetails();
        }

        private void LoadIngredientDetails()
        {
            if (cbNameIngredient.SelectedItem != null)
            {
                int idIngredient = (int)cbNameIngredient.SelectedValue;
                txbIdbingredient.Text = idIngredient.ToString();

                // Lấy danh sách nguyên liệu từ DAO
                List<Ingredient> ingredientList = IngredientDAO.Instance.GetListIngredient();
                var selectedIngredient = ingredientList.FirstOrDefault(i => i.IdIngredient == idIngredient);

                if (selectedIngredient != null)
                {
                    cbUnitIngredient1.SelectedItem = selectedIngredient.Unit;
                }
                else
                {
                    cbUnitIngredient1.SelectedIndex = -1;
                    MessageBox.Show("Không tìm thấy thông tin đơn vị cho nguyên liệu này!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "categoryName";  // hiển thị tên
            cb.ValueMember = "IdCategory";      // dùng giá trị Id để binding
        }

        #endregion


        #region Tải và binding phiếu nhập và chi tiết phiếu nhập
        private void LoadImportReceiptAndDetail()
        {
            LoadReceiptList();
            AddReceiptBinding();
            LoadSupplierComboBox();
            LoadIngredientComboBox();
            if (dtgvImportReceipt.Rows.Count > 0)
            {
                dtgvImportReceipt.Rows[0].Selected = true;
                LoadImportDetail_ByCurrentSelection();
            }
            else
            {
                detailBindingSource.DataSource = null;
                dtgvImportDetailAdmin.DataSource = null;
                txbTongChi.Text = "0"; // Reset tổng chi
            }
            AddDetailBinding();
        }

        private void LoadReceiptList()
        {
            receiptBindingSource.DataSource = ImportReceiptDAO.Instance.GetAllReceipts();
            dtgvImportReceipt.DataSource = receiptBindingSource;
            dtgvImportReceipt.Columns["IdReceipt"].HeaderText = "Mã Phiếu Nhập";
            dtgvImportReceipt.Columns["ImportDate"].HeaderText = "Ngày Nhập";
            dtgvImportReceipt.Columns["ImportedBy"].HeaderText = "Nhân Viên Nhập";
            dtgvImportReceipt.Columns["IdSupplier"].HeaderText = "Mã Nhà Cung Cấp";
        }

        private void AddReceiptBinding()
        {
            tbxMaNhapNguyenLieu.DataBindings.Clear();
            txbTaiKhoanNhanVien.DataBindings.Clear();
            cbTenNhaCungCap.DataBindings.Clear();

            tbxMaNhapNguyenLieu.DataBindings.Add("Text", receiptBindingSource, "IdReceipt", true, DataSourceUpdateMode.Never);
            txbTaiKhoanNhanVien.DataBindings.Add("Text", receiptBindingSource, "ImportedBy", true, DataSourceUpdateMode.Never);
            cbTenNhaCungCap.DataBindings.Add("SelectedValue", receiptBindingSource, "IdSupplier", true, DataSourceUpdateMode.Never);
        }

        private void LoadSupplierComboBox()
        {
            List<Supplier> supplierList = SupplierDAO.Instance.GetAllSuppliers();
            cbTenNhaCungCap.DataSource = supplierList;
            cbTenNhaCungCap.DisplayMember = "SupplierName";
            cbTenNhaCungCap.ValueMember = "IdSupplier";
        }

        private void LoadIngredientComboBox()
        {
            List<Ingredient> ingredientList = IngredientDAO.Instance.GetListIngredient();
            cbmanguyenlieu.DataSource = ingredientList;
            cbmanguyenlieu.DisplayMember = "IngredientName";
            cbmanguyenlieu.ValueMember = "IdIngredient";
        }

        private void LoadImportDetail_ByCurrentSelection()
        {
            if (!string.IsNullOrEmpty(tbxMaNhapNguyenLieu.Text) && int.TryParse(tbxMaNhapNguyenLieu.Text, out int idReceipt))
            {
                List<ImportDetail> detailList = ImportDetailDAO.Instance.GetDetailsByReceipt(idReceipt);
                if (detailList != null && detailList.Count > 0)
                {
                    detailBindingSource.DataSource = detailList;
                    dtgvImportDetailAdmin.DataSource = detailBindingSource;
                    dtgvImportDetailAdmin.Columns["IdReceipt"].HeaderText = "Mã Phiếu Nhập";
                    dtgvImportDetailAdmin.Columns["IdIngredient"].HeaderText = "Mã Nguyên Liệu";
                    dtgvImportDetailAdmin.Columns["Quantity"].HeaderText = "Số Lượng";
                    dtgvImportDetailAdmin.Columns["UnitPrice"].HeaderText = "Đơn Giá";
                    dtgvImportDetailAdmin.Columns["IdReceipt"].Visible = false;
                    // Hiển thị tổng chi phí
                    decimal totalCost = ImportDetailDAO.Instance.GetTotalCostByReceipt(idReceipt);
                    txbTongChi.Text = totalCost.ToString("N0"); // Định dạng số không thập phân
                }
                else
                {
                    dtgvImportDetailAdmin.Rows.Clear();
                    dtgvImportDetailAdmin.Refresh();
                    cbmanguyenlieu.SelectedIndex = -1;
                    nmSoLuongNhap.Value = 1;
                    txbUnit.Text = string.Empty; // Clear unit text box
                    nmGiaNhap.Value = 1;
                    txbTongChi.Text = "0"; // Reset tổng chi
                    MessageBox.Show("Không có chi tiết phiếu nhập nào!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                detailBindingSource.DataSource = null;
                dtgvImportDetailAdmin.DataSource = null;
                dtgvImportDetailAdmin.Rows.Clear();
                dtgvImportDetailAdmin.Refresh();
                cbmanguyenlieu.SelectedIndex = -1;
                txbUnit.Text = string.Empty; // Clear unit text box
                nmSoLuongNhap.Value = 1;
                nmGiaNhap.Value = 1;
                txbTongChi.Text = "0"; // Reset tổng chi
            }
        }

        private void AddDetailBinding()
        {
            cbmanguyenlieu.DataBindings.Clear();
            nmSoLuongNhap.DataBindings.Clear();
            nmGiaNhap.DataBindings.Clear();
            txbUnit.DataBindings.Clear();

            if (detailBindingSource.DataSource != null && dtgvImportDetailAdmin.Rows.Count > 0 && dtgvImportDetailAdmin.SelectedRows.Count > 0)
            {
                try
                {
                    cbmanguyenlieu.DataBindings.Add("SelectedValue", detailBindingSource, "IdIngredient", true, DataSourceUpdateMode.Never);
                    nmSoLuongNhap.DataBindings.Add("Value", detailBindingSource, "Quantity", true, DataSourceUpdateMode.Never);
                    nmGiaNhap.DataBindings.Add("Value", detailBindingSource, "UnitPrice", true, DataSourceUpdateMode.Never);

                    int selectedIndex = dtgvImportDetailAdmin.SelectedRows[0].Index;
                    if (selectedIndex >= 0 && selectedIndex < detailBindingSource.Count)
                    {
                        ImportDetail detail = detailBindingSource[selectedIndex] as ImportDetail;
                        if (detail != null && detail.IdIngredient > 0)
                        {
                            string unit = IngredientDAO.Instance.GetUnitByIdIngredient(detail.IdIngredient);
                            txbUnit.Text = unit ?? "";
                        }
                        else
                        {
                            txbUnit.Clear();
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Lỗi binding: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txbUnit.Clear();
            }

        }

        private void dtgvImportReceipt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.Validate();
                LoadImportDetail_ByCurrentSelection();
                AddDetailBinding();
            }
        }

        private void dtgvImportDetailAdmin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.Validate();
                dtgvImportDetailAdmin.Rows[e.RowIndex].Selected = true;
                AddDetailBinding();
            }
        }
        #endregion


        #region thêm sửa xóa món ăn
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            this.Validate(); // hoặc gọi WriteValue() từng cái
            string foodName = txbFoodName.Text;
            if (string.IsNullOrWhiteSpace(foodName))
            {
                MessageBox.Show("Tên món ăn không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var category = cbFoodCategory.SelectedItem as Category;
            if (category == null || category.IdCategory == 0)
            {
                MessageBox.Show("Danh mục không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Giá trị mặc định khi danh mục không hợp lệ
            }

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

            if (string.IsNullOrWhiteSpace(foodName))
            {
                MessageBox.Show("Tên món ăn không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var category = cbFoodCategory.SelectedItem as Category;
            if (category == null || category.IdCategory == 0)
            {
                MessageBox.Show("Danh mục không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Giá trị mặc định khi danh mục không hợp lệ
            }
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
                    MessageBox.Show("Đã xóa món ăn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFoodList();
                }
                else
                {
                    MessageBox.Show("Xóa món ăn thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Đã hủy thao tác xóa món ăn.", "Đã hủy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion


        #region thêm sửa xóa danh mục món ăn
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            this.Validate(); // hoặc gọi WriteValue() từng cái

            string categoryName = txbCategoryName.Text;
            if(categoryName == null)
            {
                MessageBox.Show("Tên danh mục không được để trống!");
                return;
            }

            if (CategoryDAO.Instance.InsertCategory(categoryName))
            {
                MessageBox.Show("Thêm doanh mục món ăn thành công!");
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
            if (categoryName == null)
            {
                MessageBox.Show("Tên danh mục không thể trể trống!");
                return;
            }
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
                    LoadCategoryList(); 
                }
                else
                {
                    MessageBox.Show("Không thể xóa danh mục vì còn món ăn thuộc danh mục này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion


        #region Thêm sửa xóa bàn
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
                MessageBox.Show("Tên bàn và trạng thái không được để trống hoặc trạng thái không đúng định dạng" + "!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #endregion


        #region Thêm sửa reset mật khẩu tài khoản
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserNameAccount.Text;
            if( string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!");
                return;
            }
            if (AccountDAO.Instance.GetAccountByUsername(username)!=null)
            {
                MessageBox.Show("Tài khoản đã tồn tại trong hệ thống vui lòng chọn tài khoản khác!");
                return;
            }
            int type = cbTypeAccount.SelectedIndex; // 1: Admin, 0: Staff
            if (type == -1)
            {
                MessageBox.Show("Vui lòng chọn đúng loại tài khoản!");
                return;
            }
            bool isActive = cbIsActive.SelectedIndex == 1; // True: 1, False: 0

            if (AccountDAO.Instance.InsertAccount(username, type, isActive))
            {
                MessageBox.Show("Thêm tài khoản thành công!");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại!");
            }
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserNameAccount.Text;
            if (AccountDAO.Instance.GetAccountByUsername(username) == null)
            {
                MessageBox.Show("Tài khoản chưa tồn tại không thể sửa!");
                return;
            }
            int type = cbTypeAccount.SelectedIndex; // 1: Admin, 0: Staff
            if (type == -1)
            {
                MessageBox.Show("Vui lòng chọn đúng loại tài khoản!");
                return;
            }
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

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = txbUserNameAccount.Text;
            if (AccountDAO.Instance.GetAccountByUsername(username) == null)
            {
                MessageBox.Show("Tài khoản chưa tồn tại không thể đặt lại mật khẩu!");
                return;
            }
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần reset mật khẩu!");
                return;
            }
            if (AccountDAO.Instance.ResetPassword(username))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công! Mật khẩu mới là '123456'.");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại!");
            }
        }

        #endregion


        #region Thêm sửa xóa nguyên liệu
        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            string name = txbIngredientName.Text.Trim();
            string unit = cbUnitIngredient.Text.Trim();
            decimal quantity = 0;

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


        #endregion


        #region Thêm sửa xóa nhân viên
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

            // Kiểm tra bắt buộc
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Họ và tên hoặc giới tính không được để trống!");
                return;
            }

            // Chỉ kiểm tra trùng tài khoản nếu accountUserName có giá trị
            if (!string.IsNullOrEmpty(accountUserName))
            {
                if (StaffDAO.Instance.isExitUserNameInStaff(accountUserName))
                {
                    MessageBox.Show("Tài khoản đã có nhân viên khác sở hữu! Không thể thêm cho nhân viên này");
                    return;
                }
            }

            // Thực hiện thêm nhân viên
            if (StaffDAO.Instance.InsertStaff(new Staff
            {
                FullName = fullName,
                Gender = gender,
                BirthDate = birthDate,
                Phone = phone,
                Email = email,
                AccountUserName = string.IsNullOrEmpty(accountUserName) ? null : accountUserName
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
            this.Validate(); // Đồng bộ binding

            int idStaff = Convert.ToInt32(txbIdStaff.Text);
            string fullName = txbFullName.Text;
            string gender = cbGender.SelectedItem?.ToString();
            DateTime birthDate = dtpkBirthDateStaff.Value;
            string phone = txbPhoneStaff.Text;
            string email = txbEmailStaff.Text;
            string accountUserName = cbAccountStaff.SelectedItem?.ToString();

            // Kiểm tra bắt buộc (không cần bắt buộc accountUserName)
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Họ và tên hoặc giới tính không được để trống!");
                return;
            }

            // Chỉ kiểm tra trùng tài khoản nếu accountUserName có giá trị
            if (!string.IsNullOrEmpty(accountUserName))
            {
                if (StaffDAO.Instance.isExitUserNameInStaff(accountUserName, idStaff))
                {
                    MessageBox.Show("Tài khoản đã có nhân viên khác sở hữu! Không thể cập nhật.");
                    return;
                }
            }

            // Gọi DAO update
            if (StaffDAO.Instance.UpdateStaffAdmin(new Staff
            {
                IdStaff = idStaff,
                FullName = fullName,
                Gender = gender,
                BirthDate = birthDate,
                Phone = phone,
                Email = email,
                AccountUserName = string.IsNullOrEmpty(accountUserName) ? null : accountUserName
            }))
            {
                MessageBox.Show("Sửa nhân viên thành công!");
                LoadStaff();
                AddBindingStaff();
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
                    AddBindingStaff();
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


        #endregion 


        #region Thêm sửa xóa nhà cung cấp

        // Hàm kiểm tra định dạng Email
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Hàm kiểm tra định dạng Phone
        private bool IsValidPhone(string phone)
        {
            string phonePattern = @"^\+?\d{9,15}$";
            return Regex.IsMatch(phone, phonePattern);
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
                MessageBox.Show("Tên nhà cung cấp không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng Email nếu được nhập
            if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
            {
                MessageBox.Show("Email không đúng định dạng! Vui lòng nhập email hợp lệ (ví dụ: example@domain.com).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng Phone nếu được nhập
            if (!string.IsNullOrEmpty(phone) && !IsValidPhone(phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Chỉ được chứa số và dấu '+' (nếu có).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Tên nhà cung cấp không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng Email nếu được nhập
            if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
            {
                MessageBox.Show("Email không đúng định dạng! Vui lòng nhập email hợp lệ (ví dụ: example@domain.com).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng Phone nếu được nhập
            if (!string.IsNullOrEmpty(phone) && !IsValidPhone(phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Chỉ được chứa số và dấu '+' (nếu có).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                "Việc xóa sẽ dẫn đến mất toàn bộ thông tin liên quan.",
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


        #endregion


        #region Thêm sửa xóa định lượng nguyên liệu của món ăn
        private void btnAddFoodIngredient_Click(object sender, EventArgs e)
        {
            this.Validate();
            // Kiểm tra IdFood
            if (string.IsNullOrWhiteSpace(txbIdFoodIngredient.Text))
            {
                MessageBox.Show("Mã món ăn không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txbIdFoodIngredient.Text, out int idFood))
            {
                MessageBox.Show("Mã món ăn không hợp lệ! Vui lòng nhập số nguyên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra IdIngredient
            if (string.IsNullOrWhiteSpace(txbIdbingredient.Text))
            {
                MessageBox.Show("Mã nguyên liệu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txbIdbingredient.Text, out int idIngredient))
            {
                MessageBox.Show("Mã nguyên liệu không hợp lệ! Vui lòng nhập số nguyên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra Quantity
            if (string.IsNullOrWhiteSpace(nmQuantityFoodIngredient.Text) || !decimal.TryParse(nmQuantityFoodIngredient.Text, out decimal quantity) || quantity <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ! Vui lòng nhập số lớn hơn 0.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var foodIngredient = new FoodIngredient
            {
                IdFood = idFood,
                IdIngredient = idIngredient,
                Quantity = quantity
            };

            if (FoodIngredientDAO.Instance.InsertFoodIngredient(foodIngredient))
            {
                MessageBox.Show("Thêm nguyên liệu thành công!");
                LoadFoodIngredient();
            }
            else
            {
                MessageBox.Show("Thêm nguyên liệu thất bại!");
            }
        }


        private void btnEditFoodIngredient_Click(object sender, EventArgs e)
        {
            this.Validate();
            if (string.IsNullOrWhiteSpace(txbIdFoodIngredient.Text))
            {
                MessageBox.Show("Mã món ăn không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txbIdFoodIngredient.Text, out int idFood))
            {
                MessageBox.Show("Mã món ăn không hợp lệ! Vui lòng nhập số nguyên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra IdIngredient
            if (string.IsNullOrWhiteSpace(txbIdbingredient.Text))
            {
                MessageBox.Show("Mã nguyên liệu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txbIdbingredient.Text, out int idIngredient))
            {
                MessageBox.Show("Mã nguyên liệu không hợp lệ! Vui lòng nhập số nguyên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra Quantity
            if (string.IsNullOrWhiteSpace(nmQuantityFoodIngredient.Text) || !decimal.TryParse(nmQuantityFoodIngredient.Text, out decimal quantity) || quantity <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ! Vui lòng nhập số lớn hơn 0.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var foodIngredient = new FoodIngredient
            {
                IdFood = idFood,
                IdIngredient = idIngredient,
                Quantity = quantity
            };

            if (FoodIngredientDAO.Instance.UpdateFoodIngredient(foodIngredient))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadFoodIngredient();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }


        private void btnDeleteFoodIngredient_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa nguyên liệu này không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (string.IsNullOrWhiteSpace(txbIdFoodIngredient.Text))
            {
                MessageBox.Show("Mã món ăn không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txbIdFoodIngredient.Text, out int idFood))
            {
                MessageBox.Show("Mã món ăn không hợp lệ! Vui lòng nhập số nguyên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra IdIngredient
            if (string.IsNullOrWhiteSpace(txbIdbingredient.Text))
            {
                MessageBox.Show("Mã nguyên liệu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txbIdbingredient.Text, out int idIngredient))
            {
                MessageBox.Show("Mã nguyên liệu không hợp lệ! Vui lòng nhập số nguyên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (FoodIngredientDAO.Instance.DeleteFoodIngredient(idFood, idIngredient))
            {
                MessageBox.Show("Xóa thành công!");
                LoadFoodIngredient();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }

        #endregion


        #region thêm sửa xóa phiếu nhập và chi tiết phiếu nhập
        private void btnAddReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(fLogin.LoggedInUserName))
                {
                    MessageBox.Show("Chưa đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string supplierName = cbTenNhaCungCap.Text.Trim(); // Lấy tên nhà cung cấp từ ComboBox
                int idSupplier = SupplierDAO.Instance.GetIdSupplierByName(supplierName);

                if (string.IsNullOrEmpty(supplierName))
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ImportReceipt receipt = new ImportReceipt
                {
                    ImportDate = DateTime.Now,
                    ImportedBy = fLogin.LoggedInUserName,
                    IdSupplier = idSupplier
                };
                int newId = ImportReceiptDAO.Instance.InsertReceipt(receipt);
                if (newId > 0)
                {
                    MessageBox.Show("Thêm phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReceiptList();
                }
                else
                    MessageBox.Show("Thêm phiếu nhập thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa phiếu nhập
        private void btnDeleteReceipt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxMaNhapNguyenLieu.Text) || !int.TryParse(tbxMaNhapNguyenLieu.Text, out int idReceipt))
            {
                MessageBox.Show("Chọn phiếu nhập hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Xác nhận xóa phiếu nhập?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (ImportReceiptDAO.Instance.DeleteReceipt(idReceipt))
                    {
                        MessageBox.Show("Xóa phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReceiptList();
                        LoadImportDetail_ByCurrentSelection();
                    }
                    else
                        MessageBox.Show("Xóa phiếu nhập thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Thêm chi tiết phiếu nhập
        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxMaNhapNguyenLieu.Text) || !int.TryParse(tbxMaNhapNguyenLieu.Text, out int idReceipt))
            {
                MessageBox.Show("Chọn phiếu nhập hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbmanguyenlieu.SelectedValue == null || nmSoLuongNhap.Value <= 0 || nmGiaNhap.Value <= 0)
            {
                MessageBox.Show("Nhập đủ thông tin chi tiết!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idIngredient = Convert.ToInt32(cbmanguyenlieu.SelectedValue);
            if (ImportDetailDAO.Instance.CheckDetailExists(idReceipt, idIngredient))
            {
                MessageBox.Show("Chi tiết đã tồn tại cho nguyên liệu này trong phiếu nhập!\nBạn có thể cập nhật thay vì thêm mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                ImportDetail detail = new ImportDetail
                {
                    IdReceipt = idReceipt,
                    IdIngredient = Convert.ToInt32(cbmanguyenlieu.SelectedValue),
                    Quantity = nmSoLuongNhap.Value,
                    UnitPrice = nmGiaNhap.Value
                };
                if (ImportDetailDAO.Instance.InsertDetail(detail))
                {
                    MessageBox.Show("Thêm chi tiết thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadImportDetail_ByCurrentSelection();
                }
                else
                    MessageBox.Show("Thêm chi tiết thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa chi tiết phiếu nhập
        private void btnUpdateDetail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxMaNhapNguyenLieu.Text) || !int.TryParse(tbxMaNhapNguyenLieu.Text, out int idReceipt))
            {
                MessageBox.Show("Chọn phiếu nhập hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbmanguyenlieu.SelectedValue == null || nmSoLuongNhap.Value <= 0 || nmGiaNhap.Value <= 0)
            {
                MessageBox.Show("Nhập đủ thông tin chi tiết!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                ImportDetail detail = new ImportDetail
                {
                    IdReceipt = idReceipt,
                    IdIngredient = Convert.ToInt32(cbmanguyenlieu.SelectedValue),
                    Quantity = nmSoLuongNhap.Value,
                    UnitPrice = nmGiaNhap.Value
                };
                if (ImportDetailDAO.Instance.UpdateDetail(detail))
                {
                    MessageBox.Show("Cập nhật chi tiết thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadImportDetail_ByCurrentSelection();
                }
                else
                    MessageBox.Show("Cập nhật chi tiết thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa chi tiết phiếu nhập
        private void btnDeleteDetail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxMaNhapNguyenLieu.Text) || !int.TryParse(tbxMaNhapNguyenLieu.Text, out int idReceipt))
            {
                MessageBox.Show("Chọn phiếu nhập hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtgvImportDetailAdmin.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn chi tiết để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idIngredient = Convert.ToInt32(dtgvImportDetailAdmin.SelectedRows[0].Cells["IdIngredient"].Value);
            if (MessageBox.Show("Xác nhận xóa chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (ImportDetailDAO.Instance.DeleteDetail(idReceipt, idIngredient))
                    {
                        MessageBox.Show("Xóa chi tiết thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadImportDetail_ByCurrentSelection();
                    }
                    else
                        MessageBox.Show("Xóa chi tiết thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbmanguyenlieu.SelectedValue != null && cbmanguyenlieu.SelectedValue is int idIngredient)
            {
                string unit = IngredientDAO.Instance.GetUnitByIdIngredient(idIngredient);
                txbUnit.Text = unit ?? "";
            }
            else
            {
                txbUnit.Clear();
            }
        }

        #endregion

        #region Các chức năng tìm kiếm

        void SearchDoanhThuMultiKeyword(string keyword)
        {
            string originalKeyword = keyword.Trim();
            DateTime parsedDate;
            DateTime today = DateTime.Today;

            // Thử parse với văn hóa Việt Nam
            if (DateTime.TryParseExact(keyword,
                new string[] { "hh:mm tt", "hh:mm", "HH:mm" },
                new System.Globalization.CultureInfo("vi-VN"),
                System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                keyword = parsedDate.ToString("HH:mm"); // đổi sang dạng 24h
            }

            // Kiểm tra nếu người dùng nhập khoảng thời gian (dùng '-' hoặc 'đến')
            if (originalKeyword.Contains("-") || originalKeyword.ToLower().Contains("đến"))
            {
                // Tách 2 phần
                char[] delimiters = { '-', 'đ', 'ế', 'n' }; // để tách "đến" hoặc "-"
                string[] parts = originalKeyword.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    DateTime fromDate, toDate;

                    if (DateTime.TryParse(parts[0].Trim(), out fromDate) && DateTime.TryParse(parts[1].Trim(), out toDate))
                    {
                        // Gọi proc tìm khoảng
                        string rangeQuery = "EXEC SearchDoanhThuByDateRange @FromDate, @ToDate";
                        DataTable rangeData = DataProvider.Instance.ExecuteQuery(rangeQuery, new object[] { fromDate, toDate });
                        dtgvDoanhThu.DataSource = rangeData;
                        return; // Dừng ở đây nếu là tìm khoảng
                    }
                }
            }

            // Nếu là ngày + giờ
            string[] formatsWithTime = { "dd/MM/yyyy HH:mm", "dd/MM HH:mm", "HH:mm" };

            if (DateTime.TryParseExact(originalKeyword, formatsWithTime, null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                if (originalKeyword.Contains("/") && originalKeyword.Contains(":")) // có ngày và giờ
                {
                    if (originalKeyword.Split('/').Length == 2) // thiếu năm
                    {
                        parsedDate = new DateTime(today.Year, parsedDate.Month, parsedDate.Day, parsedDate.Hour, parsedDate.Minute, 0);
                    }
                }
                keyword = parsedDate.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                // Xử lý các định dạng không có giờ
                if (DateTime.TryParseExact(originalKeyword, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    keyword = parsedDate.ToString("yyyy-MM-dd");
                }
                else if (DateTime.TryParseExact(originalKeyword, "dd/MM", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    parsedDate = new DateTime(today.Year, parsedDate.Month, parsedDate.Day);
                    keyword = parsedDate.ToString("yyyy-MM-dd");
                }
                else if (DateTime.TryParseExact(originalKeyword, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    keyword = parsedDate.ToString("yyyy-MM");
                }
                else if (DateTime.TryParseExact(originalKeyword, "yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    keyword = parsedDate.Year.ToString();
                }
                else
                {
                    // Không phải ngày giờ → giữ nguyên
                    keyword = originalKeyword;
                }
            }

            // Gọi proc tìm kiếm gần đúng
            string query = "EXEC SearchDoanhThuByKeyword @Keyword";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { keyword });
            dtgvDoanhThu.DataSource = data;

            // Highlight từ khóa trong DataGridView
            HighlightKeywordInGrid(dtgvDoanhThu, originalKeyword);
        }

        // Hàm highlight từ khóa trong DataGridView
        void HighlightKeywordInGrid(DataGridView grid, string keyword)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        cell.Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        cell.Style.BackColor = Color.White;
                    }
                }
            }
        }




        private void btnFindTable_Click(object sender, EventArgs e)
        {
            string keyword = txbSearchTable.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                tableList.DataSource = TableDAO.Instance.LoadTableList();
            }
            else
            {
                tableList.DataSource = TableDAO.Instance.SearchTableByName(keyword);
            }

            // Không cần gọi lại AddTableFoodBinding()
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            string keyword = txbSearchCategory.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
            }
            else
            {
                categoryList.DataSource = CategoryDAO.Instance.SearchCategoryByName(keyword);
            }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            string keyword = txbSearchFood.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                foodList.DataSource = FoodDAO.Instance.GetListFood(); // Hiển thị lại toàn bộ
            }
            else
            {
                foodList.DataSource = FoodDAO.Instance.SearchFood(keyword);
            }
        }

        private void btnSearchAccount_Click(object sender, EventArgs e)
        {
            string keyword = txbSearchAccount.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                accountList.DataSource = AccountDAO.Instance.GetListAccount(); // Hiển thị toàn bộ
            }
            else
            {
                accountList.DataSource = AccountDAO.Instance.SearchAccountByUsername(keyword);
            }

            dtgvAccount.DataSource = accountList;     // Cập nhật DataGridView
            AddAccountBinding();                      // Cập nhật lại Binding
        }

        private void DtgvAccount_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgvAccount.Columns.Contains("Password"))
            {
                dtgvAccount.Columns["Password"].Visible = false;
            }
            if (dtgvAccount.SelectedCells.Count > 0)
            {
                int rowIndex = dtgvAccount.SelectedCells[0].RowIndex;
                if (rowIndex < 0 || rowIndex >= dtgvAccount.Rows.Count) return;

                DataGridViewRow row = dtgvAccount.Rows[rowIndex];
                foreach (DataGridViewColumn col in dtgvAccount.Columns)
                {
                    Console.WriteLine(col.Name);
                }
                if (row.Cells["Type"].Value != DBNull.Value)
                {
                    int type = Convert.ToInt32(row.Cells["Type"].Value);
                    cbTypeAccount.SelectedIndex = type;
                }

                if (row.Cells["isActive"].Value != DBNull.Value)
                {
                    bool isActive = Convert.ToBoolean(row.Cells["isActive"].Value);
                    cbIsActive.SelectedIndex = isActive ? 1 : 0;
                }
            }
        }

        private void btnSearchStaff_Click(object sender, EventArgs e)
        {
            string name = txbSearchStaff.Text.Trim();
            dtgvStaff.DataSource = StaffDAO.Instance.SearchStaff(name);
            AddBindingStaff(); // cập nhật lại binding theo dữ liệu mới
        }

        private void btnSearchIngredient_Click(object sender, EventArgs e)
        {
            string name = txbSearchIngredient.Text.Trim();
            dtgvIngredient.DataSource = IngredientDAO.Instance.SearchIngredient(name);
            AddIngredientBinding(); // nếu bạn có binding thì nên cập nhật lại
        }


        private void btnSearchFoodIngredient_Click(object sender, EventArgs e)
        {
            string keyword = txbSearchFoodIngredient.Text.Trim();
            DataTable result = FoodDAO.Instance.SearchFoodIngredient(keyword);

            dtgvFood1.DataSource = result;
            dtgvFoodIngredient.DataSource = null;

            // Reset control
            txbIdFoodIngredient.Clear();
            cbNameFoodIngredient.SelectedIndex = -1;

            // GỌI LẠI BINDING
            AddFoodBinding1();
        }

        private void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            string name = txbSearchSupplier.Text.Trim();
            dtgvSuplier.DataSource = SupplierDAO.Instance.SearchSupplier(name);
            AddSupplierBinding(); // Binding lại sau khi gán DataSource
        }

        private void btnSearchReceipt_Click(object sender, EventArgs e)
        {
            string keyword = txbSearchReceipt.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                // Hiển thị lại toàn bộ nếu không nhập từ khóa
                LoadReceiptList();
            }
            else
            {
                DataTable result = ImportReceiptDAO.Instance.SearchImportReceipt(keyword);
                receiptBindingSource.DataSource = result;
                dtgvImportReceipt.DataSource = receiptBindingSource;

                if (dtgvImportReceipt.Rows.Count > 0)
                {
                    dtgvImportReceipt.Rows[0].Selected = true;
                    LoadImportDetail_ByCurrentSelection(); // Load chi tiết phiếu nhập đầu tiên
                }
                else
                {
                    dtgvImportDetailAdmin.DataSource = null;
                    txbTongChi.Text = "0";
                }
            }

            AddReceiptBinding(); // Giữ binding sau khi search
        }



        #endregion

        private void btnSearchDoanhThu_Click(object sender, EventArgs e)
        {
            string Key = txbSearchDoanhThu.Text.Trim();
            if (string.IsNullOrEmpty(Key))
            {
                LoadDoanhThu(DateTime.Today, DateTime.Now); // Hiển thị lại toàn bộ doanh thu nếu không có từ khóa
            }
            else
            {
                try
                {
                    SearchDoanhThuMultiKeyword(Key); // Tìm kiếm theo từ khóa
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi tìm kiếm doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

