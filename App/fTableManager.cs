using App.DAO;
using App.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            // Load ảnh bàn
            string imagePath = Path.Combine(Application.StartupPath, "table.png");
            Image tableImage = null;
            if (File.Exists(imagePath))
                tableImage = Image.FromFile(imagePath);

            // Thiết lập FlowLayoutPanel cho đẹp
            flpTable.Controls.Clear();
            flpTable.WrapContents = true;
            flpTable.AutoScroll = true;
            flpTable.Padding = new Padding(10);
            flpTable.Margin = new Padding(10);

            foreach (Table item in tableList)
            {
                Button btn = new Button()
                {
                    Width = 110,
                    Height = 110,
                    FlatStyle = FlatStyle.Flat,
                    Text = "",
                    Tag = item,
                    BackColor = Color.White,
                    Margin = new Padding(10), // mỗi nút cách nhau 10px
                };

                // Làm hình tròn
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, btn.Width, btn.Height);
                btn.Region = new Region(path);

                // Vẽ bằng GDI+
                btn.Paint += (s, e) =>
                {
                    Table table = (s as Button).Tag as Table;
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    // Viền bàn (dày 5px)
                    using (Pen pen = new Pen(table.Status == "Trống" ? Color.DodgerBlue : Color.Red, 5))
                    {
                        g.DrawEllipse(pen, 1, 1, btn.Width - 3, btn.Height - 3);
                    }

                    // Ảnh ở giữa
                    if (tableImage != null)
                    {
                        int imgSize = 40;
                        int imgX = (btn.Width - imgSize) / 2;
                        int imgY = 10;
                        g.DrawImage(tableImage, new Rectangle(imgX, imgY, imgSize, imgSize));
                    }

                    // Tên bàn ở dưới
                    using (Font font = new Font("Segoe UI", 9, FontStyle.Bold))
                    using (Brush brush = new SolidBrush(Color.Black))
                    using (StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center })
                    {
                        RectangleF textRect = new RectangleF(0, 55, btn.Width, 30);
                        g.DrawString(table.Name, font, brush, textRect, sf);
                    }
                };

                // Xử lý khi click
                btn.Click += Btn_Click;

                // Thêm vào FlowLayoutPanel
                flpTable.Controls.Add(btn);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Table table = btn.Tag as Table;
            MessageBox.Show($"Bạn đã chọn {table.Name}");
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
    }

    // Assuming the Table class exists

}
