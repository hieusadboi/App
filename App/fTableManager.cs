using App.DAO;
using App.DTO;
using App.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = App.DTO.Menu;

namespace App
{
    public partial class fTableManager : Form
    {

        private List<Menu> currentBillItems = new List<Menu>(); // biến lưu hóa đơn 

        private Button selectedTableButton; // Biến lưu button của bàn đang chọn

        private Account loginAccount;

        public Account LoginAccount { 
            get => loginAccount;
            set             {
                loginAccount = value;
                changeAccount(loginAccount.Type); // Cập nhật giao diện dựa trên loại tài khoản
            }
        }

        public fTableManager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategoryList(0); // Load danh mục ban đầu
        }

        void changeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1; // Chỉ hiển thị nếu là tài khoản quản trị
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }

        // Xử lý sự kiện khi nút "Thêm món ăn" được nhấn
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            // Nếu chưa có bàn, mặc định gán bàn "Mang Về"
            if (table == null)
            {
                table = TableDAO.Instance.GetTakeAwayTable();
                if (table == null)
                {
                    MessageBox.Show("Không tìm thấy bàn 'Mang Về' trong hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lsvBill.Tag = table;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int idFood = (cbFood.SelectedItem as Food).IdFood;
            int count = (int)nmFoodCount.Value;
            var notEnough = GetNotEnoughIngredients(idFood, count);
            if (notEnough.Count > 0)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine("❌ Không đủ nguyên liệu để chế biến:");
                foreach (var item in notEnough)
                {
                    msg.AppendLine($"• {item.IngredientName}: cần {item.Quantity} {item.Unit}");
                }
                MessageBox.Show(msg.ToString(), "Thiếu nguyên liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idBill == -1) // Nếu không có hóa đơn chưa thanh toán, tạo mới
            {
                string createdBy = fLogin.LoggedInUserName;
                BillDAO.Instance.InsertBill(table.ID, createdBy);

                // Lấy ID hóa đơn mới tạo
                idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }
            else
            {
                // Nếu đã có hóa đơn chưa thanh toán, chỉ cần thêm món ăn vào hóa đơn đó
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }

            ShowBill(table.ID);
            UpdateSingleTable(table.ID); // Cập nhật chỉ bàn đang chọn
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }


        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null) return;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            if (idBill != -1)
            {
                if (MessageBox.Show($"Thanh toán {table.Name}?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    float totalPrice = 0;
                    foreach (ListViewItem item in lsvBill.Items)
                    {
                        if (item.SubItems.Count >= 4)
                        {
                            totalPrice += float.Parse(item.SubItems[3].Text, NumberStyles.Currency, CultureInfo.CurrentCulture);
                        }
                    }

                    bool userWantsPrint = MessageBox.Show("Bạn có muốn in hóa đơn không?", "In hóa đơn", MessageBoxButtons.YesNo) == DialogResult.Yes;

                    if (userWantsPrint)
                    {
                        string filePath = InvoiceFileHelper.GetInvoiceFilePath(true);
                        string fullDisplayName = BillDAO.Instance.GetDisplayNameByBillID(idBill);
                        PDFExporter.ExportBillToPDF(lsvBill, table, fullDisplayName, txbtotalPrice.Text, filePath, true);
                    }

                    BillDAO.Instance.CheckOut(idBill);
                    UpdateSingleTable(table.ID);
                    ShowBill(table.ID);
                }
            }
            ShowLowStockWarning(); // Hiển thị cảnh báo kho nếu có nguyên liệu sắp hết
        }

        #region Method

        private void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            // Load ảnh bàn nếu có
            string defaultImagePath = Path.Combine(Application.StartupPath, "table.png");
            string vipImagePath = Path.Combine(Application.StartupPath, "tablevip.png");
            Image defaultTableImage = null;
            Image vipTableImage = null;
            if (File.Exists(defaultImagePath))
                defaultTableImage = Image.FromFile(defaultImagePath);
            if (File.Exists(vipImagePath))
                vipTableImage = Image.FromFile(vipImagePath);

            // Xóa bàn cũ
            flowLayoutNormalTables.Controls.Clear();
            flowLayoutVipTables.Controls.Clear();

            int buttonWidth = 110;
            int spacing = 15;

            foreach (Table item in tableList)
            {
                // Tạo panel chứa nút và tên bàn
                Panel panel = new Panel
                {
                    Width = buttonWidth + spacing,
                    Height = 150,
                    Margin = new Padding(10),
                    Tag = item.ID // Lưu ID bàn để tìm kiếm dễ hơn
                };

                Button btn = new Button
                {
                    Width = 110,
                    Height = 110,
                    FlatStyle = FlatStyle.Flat,
                    Text = "",
                    Tag = item,
                    BackColor = Color.White,
                    Location = new Point(0, 0)
                };

                // Làm nút tròn
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, btn.Width, btn.Height);
                btn.Region = new Region(path);

                // Vẽ bằng GDI+
                btn.Paint += (s, e) =>
                {
                    Table table = (s as Button).Tag as Table;
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    // Xác định màu viền
                    Color borderColor = table.Status == "Trống" ? Color.DodgerBlue : Color.Red;
                    if (s == selectedTableButton)
                        borderColor = Color.Green; // Bàn được chọn có viền xanh lá

                    using (Pen pen = new Pen(borderColor, 5))
                    {
                        g.DrawEllipse(pen, 1, 1, btn.Width - 3, btn.Height - 3);
                    }

                    // Dùng hình ảnh phù hợp
                    string imagePath = null;
                    if(item.Name.ToLower().Contains("vip"))
                        imagePath = vipImagePath;
                    else
                        imagePath = defaultImagePath;

                    Image tableImage = item.Name.ToLower().Contains("vip") ? vipTableImage : defaultTableImage;
                    if (File.Exists(imagePath))
                        tableImage = Image.FromFile(imagePath);

                    if (tableImage != null)
                    {
                        int imgSize = 80;
                        int imgX = (btn.Width - imgSize) / 2;
                        int imgY = (btn.Height - imgSize) / 2;
                        g.DrawImage(tableImage, new Rectangle(imgX, imgY, imgSize, imgSize));
                    }
                };

                btn.Click += Btn_Click;
                btn.Tag = item; // Gán đối tượng Table vào Tag của nút

                Label lblTableName = new Label
                {
                    Text = item.Name,
                    Width = buttonWidth,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(0, btn.Height + 5)
                };

                panel.Controls.Add(btn);
                panel.Controls.Add(lblTableName);

                // Gán đúng panel tương ứng
                if (item.Name.ToLower().Contains("vip"))
                    flowLayoutVipTables.Controls.Add(panel);
                else
                    flowLayoutNormalTables.Controls.Add(panel);
            }
        }

        private void UpdateSingleTable(int tableID)
        {
            // Tìm panel chứa bàn có ID tương ứng
            foreach (Control panel in flowLayoutNormalTables.Controls.Cast<Control>().Concat(flowLayoutVipTables.Controls.Cast<Control>()))
            {
                if (panel is Panel && (int)panel.Tag == tableID)
                {
                    // Tìm button trong panel
                    Button btn = panel.Controls.OfType<Button>().FirstOrDefault();
                    if (btn != null)
                    {
                        Table updatedTable = TableDAO.Instance.GetTableByID(tableID); // Sử dụng GetTableByID
                        if (updatedTable != null)
                        {
                            btn.Tag = updatedTable; // Cập nhật dữ liệu bàn
                            btn.Invalidate(); // Vẽ lại button để cập nhật giao diện
                        }
                        break;
                    }
                }
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            float totalPrice = 0; // Biến để lưu tổng giá tiền
            CultureInfo culture = new CultureInfo("vi-VN");

            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);

            currentBillItems = MenuDAO.Instance.GetListMenuByTable(id); // Gán Bill vào biến


            foreach (Menu item in listBillInfo)
            {
                // Cộng dồn tổng giá tiền
                totalPrice += item.TotalPrice;

                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString("c", culture)); // Hiển thị giá theo định dạng tiền tệ
                lsvItem.SubItems.Add(item.TotalPrice.ToString("c", culture)); // Hiển thị tổng giá theo định dạng tiền tệ
                lsvItem.SubItems.Add(item.IdBill.ToString()); // Thêm ID hóa đơn nếu cần
                lsvBill.Items.Add(lsvItem);
            }

            txbtotalPrice.Text = totalPrice.ToString("c", culture); // Hiển thị tổng giá tiền theo định dạng tiền tệ
        }

        // Xử lý sự kiện khi nút bàn được nhấn
        private void Btn_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int tableID = (clickedButton.Tag as Table).ID;
            lsvBill.Tag = clickedButton.Tag; // Lưu thông tin bàn vào lsvBill.Tag

            // Bỏ highlight bàn trước đó
            if (selectedTableButton != null)
            {
                selectedTableButton.BackColor = Color.White;
                selectedTableButton.Invalidate(); // Vẽ lại để dùng hình và viền mặc định
            }

            // Highlight bàn được chọn
            selectedTableButton = clickedButton;
            selectedTableButton.BackColor = Color.LightGreen; // Nền xanh nhạt cho bàn được chọn
            selectedTableButton.Invalidate(); // Vẽ lại để dùng hình và viền xanh lá

            ShowBill(tableID);
        }

        void LoadCategoryList(int id)
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "CategoryName"; // Hiển thị tên danh mục
        }

        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetListFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "foodName"; // Hiển thị tên món ăn
        }

        #endregion

        #region events handlers for menu items
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfle f = new fAccountProfle(loginAccount);
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        //private void nhậpNguyênLiệuToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
        //    fImport f = new fImport();
        //    f.ShowDialog();
        //}

        #endregion


        private void CbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return; // Kiểm tra nếu không có mục nào được chọn
            Category selectedCategory = cb.SelectedItem as Category;
            if (selectedCategory != null)
            {
                id = selectedCategory.IdCategory; // Lấy ID của danh mục được chọn
            }
            LoadFoodListByCategoryID(id);
        }


        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null) return;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            if (idBill == -1)
            {
                MessageBox.Show("Chưa có hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string filePath = InvoiceFileHelper.GetInvoiceFilePath(false);
            PDFExporter.ExportBillToPDF(lsvBill, table, fLogin.LoggedInUserName, txbtotalPrice.Text, filePath, false); // isPaid = false


            MessageBox.Show("Đã tạo hóa đơn tạm.", "Thông báo");
        }

        private void updatethongtincanhan_Click(object sender, EventArgs e)
        {
            Infomation f = new Infomation(LoginAccount.Username);
            f.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void nhậpNguyênLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fImport f = new fImport();
            f.ShowDialog();
        }


        private List<FoodIngredient> GetNotEnoughIngredients(int foodId, int count)
        {
            var requiredIngredients = FoodIngredientDAO.Instance.GetIngredientsByFoodId(foodId);
            List<FoodIngredient> notEnough = new List<FoodIngredient>();

            foreach (var usage in requiredIngredients)
            {
                Ingredient ingredient = IngredientDAO.Instance.GetIngredientByID(usage.IdIngredient);
                decimal requiredAmount = usage.Quantity * count;

                if (ingredient == null || ingredient.Quantity < requiredAmount)
                {
                    var clone = new FoodIngredient
                    {
                        IdFood = usage.IdFood,
                        IdIngredient = usage.IdIngredient,
                        IngredientName = usage.IngredientName,
                        Unit = usage.Unit,
                        Quantity = requiredAmount
                    };
                    notEnough.Add(clone);
                }
            }

            return notEnough;
        }



        public static void ShowLowStockWarning()
        {
            var lowStockList = IngredientDAO.Instance.GetLowStockIngredients();
            if (lowStockList.Count == 0) return;

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendLine("🔔 Các nguyên liệu sắp hết kho:\n");

            foreach (var item in lowStockList)
            {
                messageBuilder.AppendLine($"• {item.IngredientName,-20} : {item.Quantity,8:0.###} {item.Unit}");
            }

            MessageBox.Show(
                messageBuilder.ToString(),
                "⚠️ Cảnh báo nguyên liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }



    }
}