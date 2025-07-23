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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        public static string LoggedInUserName { get; private set; }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txbPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát?", "Thông báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                e.Cancel = true; // Cancel the form closing event
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUser.Text;
            string password = txbPass.Text;
            int loginResult = DAO.AccountDAO.Instance.Login(userName, password);

            switch (loginResult)
            {
                case 1: // Thành công
                    LoggedInUserName = userName;
                    Account loginAccount = DAO.AccountDAO.Instance.GetAccountByUsername(userName);
                    fTableManager f = new fTableManager(loginAccount); // Lưu tài khoản đăng nhập vào fTableManager
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                    break;

                case 0:
                    MessageBox.Show("Tài khoản này đã bị vô hiệu hóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case -1:
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Lỗi không xác định. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //    string userName = txbUser.Text;
        //    string password = txbPass.Text;
        //    int loginResult = AccountDAO.Instance.Login(userName, password);

        //    switch (loginResult)
        //    {
        //        case 1:
        //            LoggedInUserName = userName;
        //            Account loginAccount = AccountDAO.Instance.GetAccountByUsername(userName);
        //            fTableManager f = new fTableManager(loginAccount);
        //            this.Hide();
        //            f.ShowDialog();
        //            this.Show();
        //            break;
        //        case 0:
        //            MessageBox.Show("Tài khoản này đã bị vô hiệu hóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            break;
        //        case -1:
        //            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            break;
        //    }
        //}

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void btnGenerateHash_Click(object sender, EventArgs e)
        {
            string password = "admin"; // Hoặc lấy từ TextBox
            string hashed = BCrypt.Net.BCrypt.HashPassword(password);
            System.Diagnostics.Debug.WriteLine($"Hashed Password: {hashed}");
        }

    }
}
