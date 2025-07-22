namespace App
{
    partial class fTableManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinTàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatethongtincanhan = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinCáNhânToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhậpNguyênLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lsvBill = new System.Windows.Forms.ListView();
            this.TenMon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SoLuong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DonGia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ThanhTien = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdBill = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrintBill = new System.Windows.Forms.Button();
            this.txbtotalPrice = new System.Windows.Forms.TextBox();
            this.textboxtongtien = new System.Windows.Forms.Label();
            this.ptnCheckOut = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.nmFoodCount = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFood = new System.Windows.Forms.ComboBox();
            this.bnAddFood = new System.Windows.Forms.Button();
            this.flpTable = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxNormalTables = new System.Windows.Forms.GroupBox();
            this.flowLayoutNormalTables = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxVipTables = new System.Windows.Forms.GroupBox();
            this.flowLayoutVipTables = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmFoodCount)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.flpTable.SuspendLayout();
            this.groupBoxNormalTables.SuspendLayout();
            this.groupBoxVipTables.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminToolStripMenuItem,
            this.thôngTinTàiKhoảnToolStripMenuItem,
            this.nhậpNguyênLiệuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1532, 31);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(85, 27);
            this.adminToolStripMenuItem.Text = "Quản lý";
            this.adminToolStripMenuItem.Click += new System.EventHandler(this.adminToolStripMenuItem_Click);
            // 
            // thôngTinTàiKhoảnToolStripMenuItem
            // 
            this.thôngTinTàiKhoảnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updatethongtincanhan,
            this.thôngTinCáNhânToolStripMenuItem,
            this.đăngXuấtToolStripMenuItem});
            this.thôngTinTàiKhoảnToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thôngTinTàiKhoảnToolStripMenuItem.Name = "thôngTinTàiKhoảnToolStripMenuItem";
            this.thôngTinTàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(102, 27);
            this.thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin";
            // 
            // updatethongtincanhan
            // 
            this.updatethongtincanhan.Name = "updatethongtincanhan";
            this.updatethongtincanhan.Size = new System.Drawing.Size(238, 28);
            this.updatethongtincanhan.Text = "Thông tin cá nhân";
            this.updatethongtincanhan.Click += new System.EventHandler(this.updatethongtincanhan_Click);
            // 
            // thôngTinCáNhânToolStripMenuItem
            // 
            this.thôngTinCáNhânToolStripMenuItem.Name = "thôngTinCáNhânToolStripMenuItem";
            this.thôngTinCáNhânToolStripMenuItem.Size = new System.Drawing.Size(238, 28);
            this.thôngTinCáNhânToolStripMenuItem.Text = "Đổi mật khẩu";
            this.thôngTinCáNhânToolStripMenuItem.Click += new System.EventHandler(this.thôngTinCáNhânToolStripMenuItem_Click);
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(238, 28);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // nhậpNguyênLiệuToolStripMenuItem
            // 
            this.nhậpNguyênLiệuToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nhậpNguyênLiệuToolStripMenuItem.Name = "nhậpNguyênLiệuToolStripMenuItem";
            this.nhậpNguyênLiệuToolStripMenuItem.Size = new System.Drawing.Size(172, 27);
            this.nhậpNguyênLiệuToolStripMenuItem.Text = "Nhập Nguyên Liệu";
            this.nhậpNguyênLiệuToolStripMenuItem.Click += new System.EventHandler(this.nhậpNguyênLiệuToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lsvBill);
            this.panel2.Location = new System.Drawing.Point(823, 209);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(696, 468);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(218, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thông tin hóa đơn";
            // 
            // lsvBill
            // 
            this.lsvBill.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TenMon,
            this.SoLuong,
            this.DonGia,
            this.ThanhTien,
            this.IdBill});
            this.lsvBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvBill.FullRowSelect = true;
            this.lsvBill.GridLines = true;
            this.lsvBill.HideSelection = false;
            this.lsvBill.Location = new System.Drawing.Point(-1, 78);
            this.lsvBill.Margin = new System.Windows.Forms.Padding(4);
            this.lsvBill.Name = "lsvBill";
            this.lsvBill.Size = new System.Drawing.Size(696, 390);
            this.lsvBill.TabIndex = 0;
            this.lsvBill.UseCompatibleStateImageBehavior = false;
            this.lsvBill.View = System.Windows.Forms.View.Details;
            // 
            // TenMon
            // 
            this.TenMon.Text = "Tên Món Ăn";
            this.TenMon.Width = 230;
            // 
            // SoLuong
            // 
            this.SoLuong.Text = "Số Lượng";
            this.SoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SoLuong.Width = 99;
            // 
            // DonGia
            // 
            this.DonGia.Text = "Đơn Giá";
            this.DonGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DonGia.Width = 101;
            // 
            // ThanhTien
            // 
            this.ThanhTien.Text = "Thành Tiền";
            this.ThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ThanhTien.Width = 117;
            // 
            // IdBill
            // 
            this.IdBill.Text = "Mã Hóa Đơn";
            this.IdBill.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IdBill.Width = 133;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnPrintBill);
            this.panel3.Controls.Add(this.txbtotalPrice);
            this.panel3.Controls.Add(this.textboxtongtien);
            this.panel3.Controls.Add(this.ptnCheckOut);
            this.panel3.Location = new System.Drawing.Point(823, 686);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(696, 107);
            this.panel3.TabIndex = 3;
            // 
            // btnPrintBill
            // 
            this.btnPrintBill.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrintBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintBill.Location = new System.Drawing.Point(38, 8);
            this.btnPrintBill.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintBill.Name = "btnPrintBill";
            this.btnPrintBill.Size = new System.Drawing.Size(150, 86);
            this.btnPrintBill.TabIndex = 8;
            this.btnPrintBill.Text = "In Hóa Đơn ";
            this.btnPrintBill.UseVisualStyleBackColor = false;
            this.btnPrintBill.Click += new System.EventHandler(this.btnPrintBill_Click);
            // 
            // txbtotalPrice
            // 
            this.txbtotalPrice.BackColor = System.Drawing.Color.Thistle;
            this.txbtotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbtotalPrice.ForeColor = System.Drawing.SystemColors.MenuText;
            this.txbtotalPrice.Location = new System.Drawing.Point(246, 60);
            this.txbtotalPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txbtotalPrice.Name = "txbtotalPrice";
            this.txbtotalPrice.Size = new System.Drawing.Size(270, 30);
            this.txbtotalPrice.TabIndex = 7;
            this.txbtotalPrice.Text = "0 VND";
            this.txbtotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxtongtien
            // 
            this.textboxtongtien.AllowDrop = true;
            this.textboxtongtien.BackColor = System.Drawing.Color.Thistle;
            this.textboxtongtien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxtongtien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxtongtien.Location = new System.Drawing.Point(246, 8);
            this.textboxtongtien.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.textboxtongtien.Name = "textboxtongtien";
            this.textboxtongtien.Size = new System.Drawing.Size(270, 50);
            this.textboxtongtien.TabIndex = 6;
            this.textboxtongtien.Text = "Tổng tiền cần thanh toán ";
            this.textboxtongtien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ptnCheckOut
            // 
            this.ptnCheckOut.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ptnCheckOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ptnCheckOut.Location = new System.Drawing.Point(522, 4);
            this.ptnCheckOut.Margin = new System.Windows.Forms.Padding(4);
            this.ptnCheckOut.Name = "ptnCheckOut";
            this.ptnCheckOut.Size = new System.Drawing.Size(150, 86);
            this.ptnCheckOut.TabIndex = 3;
            this.ptnCheckOut.Text = "Thanh toán";
            this.ptnCheckOut.UseVisualStyleBackColor = false;
            this.ptnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.bnAddFood);
            this.panel4.Location = new System.Drawing.Point(823, 41);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(696, 160);
            this.panel4.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.nmFoodCount);
            this.panel5.Location = new System.Drawing.Point(2, 106);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(490, 46);
            this.panel5.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Số Lượng:";
            // 
            // nmFoodCount
            // 
            this.nmFoodCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmFoodCount.Location = new System.Drawing.Point(141, 4);
            this.nmFoodCount.Margin = new System.Windows.Forms.Padding(4);
            this.nmFoodCount.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nmFoodCount.Name = "nmFoodCount";
            this.nmFoodCount.Size = new System.Drawing.Size(292, 28);
            this.nmFoodCount.TabIndex = 3;
            this.nmFoodCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmFoodCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmFoodCount.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbCategory);
            this.panel1.Location = new System.Drawing.Point(2, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 44);
            this.panel1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Doanh Mục:";
            // 
            // cbCategory
            // 
            this.cbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(141, 4);
            this.cbCategory.Margin = new System.Windows.Forms.Padding(4);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(292, 30);
            this.cbCategory.TabIndex = 0;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.CbCategory_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.cbFood);
            this.panel6.Location = new System.Drawing.Point(2, 58);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(490, 43);
            this.panel6.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tên Món Ăn:";
            // 
            // cbFood
            // 
            this.cbFood.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFood.FormattingEnabled = true;
            this.cbFood.Location = new System.Drawing.Point(142, 4);
            this.cbFood.Margin = new System.Windows.Forms.Padding(4);
            this.cbFood.Name = "cbFood";
            this.cbFood.Size = new System.Drawing.Size(292, 30);
            this.cbFood.TabIndex = 1;
            // 
            // bnAddFood
            // 
            this.bnAddFood.BackColor = System.Drawing.Color.NavajoWhite;
            this.bnAddFood.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnAddFood.Location = new System.Drawing.Point(522, 34);
            this.bnAddFood.Margin = new System.Windows.Forms.Padding(4);
            this.bnAddFood.Name = "bnAddFood";
            this.bnAddFood.Size = new System.Drawing.Size(150, 84);
            this.bnAddFood.TabIndex = 2;
            this.bnAddFood.Text = "Thêm món";
            this.bnAddFood.UseVisualStyleBackColor = false;
            this.bnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // flpTable
            // 
            this.flpTable.AllowDrop = true;
            this.flpTable.AutoScroll = true;
            this.flpTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTable.Controls.Add(this.groupBoxNormalTables);
            this.flpTable.Controls.Add(this.groupBoxVipTables);
            this.flpTable.Location = new System.Drawing.Point(20, 41);
            this.flpTable.Margin = new System.Windows.Forms.Padding(4);
            this.flpTable.Name = "flpTable";
            this.flpTable.Size = new System.Drawing.Size(786, 751);
            this.flpTable.TabIndex = 5;
            // 
            // groupBoxNormalTables
            // 
            this.groupBoxNormalTables.Controls.Add(this.flowLayoutNormalTables);
            this.groupBoxNormalTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxNormalTables.Location = new System.Drawing.Point(2, 2);
            this.groupBoxNormalTables.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxNormalTables.Name = "groupBoxNormalTables";
            this.groupBoxNormalTables.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxNormalTables.Size = new System.Drawing.Size(772, 440);
            this.groupBoxNormalTables.TabIndex = 1;
            this.groupBoxNormalTables.TabStop = false;
            this.groupBoxNormalTables.Text = "Bàn Thường";
            // 
            // flowLayoutNormalTables
            // 
            this.flowLayoutNormalTables.AutoScroll = true;
            this.flowLayoutNormalTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutNormalTables.Location = new System.Drawing.Point(2, 25);
            this.flowLayoutNormalTables.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutNormalTables.Name = "flowLayoutNormalTables";
            this.flowLayoutNormalTables.Size = new System.Drawing.Size(768, 413);
            this.flowLayoutNormalTables.TabIndex = 0;
            // 
            // groupBoxVipTables
            // 
            this.groupBoxVipTables.Controls.Add(this.flowLayoutVipTables);
            this.groupBoxVipTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxVipTables.Location = new System.Drawing.Point(2, 446);
            this.groupBoxVipTables.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxVipTables.Name = "groupBoxVipTables";
            this.groupBoxVipTables.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxVipTables.Size = new System.Drawing.Size(772, 286);
            this.groupBoxVipTables.TabIndex = 0;
            this.groupBoxVipTables.TabStop = false;
            this.groupBoxVipTables.Text = "Bàn VIP";
            // 
            // flowLayoutVipTables
            // 
            this.flowLayoutVipTables.AutoScroll = true;
            this.flowLayoutVipTables.Location = new System.Drawing.Point(6, 26);
            this.flowLayoutVipTables.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutVipTables.Name = "flowLayoutVipTables";
            this.flowLayoutVipTables.Size = new System.Drawing.Size(764, 254);
            this.flowLayoutVipTables.TabIndex = 0;
            // 
            // fTableManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1532, 808);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.flpTable);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fTableManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm quản lý quán ăn";
            this.Load += new System.EventHandler(this.fTableManager_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmFoodCount)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.flpTable.ResumeLayout(false);
            this.groupBoxNormalTables.ResumeLayout(false);
            this.groupBoxVipTables.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thôngTinTàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView lsvBill;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button bnAddFood;
        private System.Windows.Forms.ComboBox cbFood;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.NumericUpDown nmFoodCount;
        private System.Windows.Forms.FlowLayoutPanel flpTable;
        private System.Windows.Forms.Button ptnCheckOut;
        private System.Windows.Forms.ToolStripMenuItem thôngTinCáNhânToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatethongtincanhan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxVipTables;
        private System.Windows.Forms.GroupBox groupBoxNormalTables;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutNormalTables;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutVipTables;
        private System.Windows.Forms.ColumnHeader TenMon;
        private System.Windows.Forms.ColumnHeader SoLuong;
        private System.Windows.Forms.ColumnHeader DonGia;
        private System.Windows.Forms.ColumnHeader ThanhTien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ColumnHeader IdBill;
        private System.Windows.Forms.Label textboxtongtien;
        private System.Windows.Forms.TextBox txbtotalPrice;
        private System.Windows.Forms.Button btnPrintBill;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhậpNguyênLiệuToolStripMenuItem;
    }
}