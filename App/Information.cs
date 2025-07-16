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
    public partial class Infomation : Form
    {
        private string userName;

        public Infomation(string userName)
        {
            InitializeComponent();
            this.userName = userName;
            LoadStaffInfo();
        }

        private void LoadStaffInfo()
        {
            Staff staff = StaffDAO.Instance.GetStaffByAccount(userName);
            if (staff != null)
            {
                txbFullName.Text = staff.FullName;
                cbGender.Text = staff.Gender;
                dtpBirthDate.Value = staff.BirthDate;
                txbPhone.Text = staff.Phone;
                txbEmail.Text = staff.Email;
                txbAccountUserName.Text = staff.AccountUserName;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnUpdateInfomation_Click(object sender, EventArgs e)
        {
            Staff updatedStaff = new Staff()
            {
                FullName = txbFullName.Text,
                Gender = cbGender.Text,
                BirthDate = dtpBirthDate.Value,
                Phone = txbPhone.Text,
                Email = txbEmail.Text,
                AccountUserName = txbAccountUserName.Text
            };

            bool result = StaffDAO.Instance.UpdateStaff(updatedStaff);
            if (result) {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

