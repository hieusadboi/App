using App.DAO;
using App.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class fImport : Form
    {
        BindingSource supplierList = new BindingSource();
        BindingSource receiptBindingSource = new BindingSource();
        BindingSource detailBindingSource = new BindingSource();

        public fImport()
        {
            InitializeComponent();
            LoadSupplier();
            AddSupplierBinding();
            LoadImportReceiptAndDetail();
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

        #region Thêm sửa xóa nhà cung cấp
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

        private void btnShowSuplier_Click(object sender, EventArgs e)
        {
            LoadSupplier();
            AddSupplierBinding();
        }
        #endregion

        #region Tải và binding phiếu nhập và chi tiết phiếu nhập
        private void LoadImportReceiptAndDetail()
        {
            LoadReceiptList();
            AddReceiptBinding();
            LoadSupplierComboBox();
            LoadUnitComboBox();
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

        private void LoadUnitComboBox()
        {
            List<IngredientUnit> units = IngredientDAO.Instance.GetUnitsWithId();
            cbUnit.DataSource = null;
            cbUnit.DataSource = units;
            cbUnit.DisplayMember = "Unit";
            cbUnit.ValueMember = "IdIngredient";
            cbUnit.SelectedIndex = -1;
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
                    cbUnit.SelectedIndex = -1;
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
                cbUnit.SelectedIndex = -1;
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
            cbUnit.DataBindings.Clear();
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
                            cbUnit.SelectedValue = detail.IdIngredient;
                        }
                        else
                        {
                            cbUnit.SelectedIndex = -1;
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
                cbUnit.SelectedIndex = -1;
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

        #region Thêm sửa xóa phiếu nhập và chi tiết phiếu nhập
        private void btnAddReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(fLogin.LoggedInUserName))
                {
                    MessageBox.Show("Chưa đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string supplierName = cbTenNhaCungCap.Text.Trim();
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
                    txbTongChi.Text = "0"; // Reset tổng chi khi thêm phiếu nhập mới
                }
                else
                    MessageBox.Show("Thêm phiếu nhập thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                        txbTongChi.Text = "0"; // Reset tổng chi
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
                    LoadImportDetail_ByCurrentSelection(); // Cập nhật tổng chi phí
                }
                else
                    MessageBox.Show("Thêm chi tiết thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                    LoadImportDetail_ByCurrentSelection(); // Cập nhật tổng chi phí
                }
                else
                    MessageBox.Show("Cập nhật chi tiết thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                        LoadImportDetail_ByCurrentSelection(); // Cập nhật tổng chi phí
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
                cbUnit.SelectedValue = idIngredient;
            }
            else
            {
                cbUnit.SelectedIndex = -1;
            }
        }
        #endregion
    }
}