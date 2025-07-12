using App.DAO;
using App.DTO;
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
        public fTableManager()
        {
            InitializeComponent();
            LoadTable();
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void nmDiscount_ValueChanged(object sender, EventArgs e)
        {

        }
        #region Method

        private void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            // Load ảnh bàn nếu có
            string imagePath = Path.Combine(Application.StartupPath, "table.png");
            Image tableImage = null;
            if (File.Exists(imagePath))
                tableImage = Image.FromFile(imagePath);

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
                    Margin = new Padding(10)
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

                    using (Pen pen = new Pen(table.Status == "Trống" ? Color.DodgerBlue : Color.Red, 5))
                    {
                        g.DrawEllipse(pen, 1, 1, btn.Width - 3, btn.Height - 3);
                    }

                    if (tableImage != null)
                    {
                        int imgSize = 40;
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


        // Hiển thị tin hóa đơn 
        //void ShowBill(int id)
        //{
        //    lsvBill.Items.Clear();

        //    List<BillInfo> listBillInfo = BillInfoDAO.Instance.GetListBillInfo(BillDAO.Instance.GetUncheckBillIDByTableID(id));

        //    foreach (BillInfo item in listBillInfo)
        //    {
        //        ListViewItem lsvItem = new ListViewItem(item.FoodID.ToString());
        //        lsvItem.SubItems.Add(item.Count.ToString());

        //        lsvBill.Items.Add(lsvItem);
        //    }
        //}

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            float totalPrice = 0; // Biến để lưu tổng giá tiền
            CultureInfo culture = new CultureInfo("vi-VN");

            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);

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

        // Xlý sự kiện khi nút bàn được nhấn
        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            ShowBill(tableID);
        }

        #endregion

        #region events handlers for menu items
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfle f = new fAccountProfle();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        private void nhậpNguyênLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fImport f = new fImport();
            f.ShowDialog();
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    // Assuming the Table class exists

}
