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
    public partial class fAccountProfle : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get => loginAccount;
            set
            {
                loginAccount = value;
                changeAccount(loginAccount.Type); // Cập nhật giao diện dựa trên loại tài khoản
            }
        }

        public fAccountProfle(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc; // Gán tài khoản đăng nhập
        }

        void changeAccount(int type)
        {
            txbUserName.Text = LoginAccount.Username; // Gán tên đăng nhập (readonly textbox)

            // Không nên gán password vì lý do bảo mật
            txbPassWord.Text = string.Empty;
            txbNewPass.Text = string.Empty;
            txbReEnterPass.Text = string.Empty;
        }

        private void UpdateAccount()
        {
            string userName = txbUserName.Text;
            string currentPassword = txbPassWord.Text;
            string newPassword = txbNewPass.Text;
            string reEnterPassword = txbReEnterPass.Text;

            if (string.IsNullOrWhiteSpace(currentPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(reEnterPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != reEnterPassword)
            {
                MessageBox.Show("Mật khẩu mới nhập lại không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool result = AccountDAO.Instance.UpdatePassword(userName, currentPassword, newPassword);
            if (result)
            {
                MessageBox.Show("Cập nhật mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai mật khẩu hiện tại hoặc lỗi cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void btnUpdatePass_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }
    }
}
