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

        private int currentStaffId; // Biến lưu Id của nhân viên hiện tại
        private void LoadStaffInfo()
        {
            Staff staff = StaffDAO.Instance.GetStaffByAccount(userName);
            if (staff != null)
            {
                currentStaffId = staff.IdStaff; // Gán Id

                txbFullName.Text = staff.FullName;
                dtpBirthDate.Value = staff.BirthDate;
                txbPhone.Text = staff.Phone;
                txbEmail.Text = staff.Email;
                txbAccountUserName.Text = staff.AccountUserName;

                // Chuẩn bị ComboBox giới tính
                cbGender.Items.Clear(); // Xóa tránh trùng
                cbGender.Items.Add("Nam");
                cbGender.Items.Add("Nữ");
                cbGender.DropDownStyle = ComboBoxStyle.DropDownList;

                // Gán giá trị giới tính
                if (cbGender.Items.Contains(staff.Gender))
                    cbGender.SelectedItem = staff.Gender;
                else
                    cbGender.SelectedIndex = 0; // Mặc định chọn Nam nếu không khớp
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
                IdStaff = currentStaffId, // Sử dụng Id của nhân viên hiện tại
                FullName = txbFullName.Text,
                Gender = cbGender.Text,
                BirthDate = dtpBirthDate.Value,
                Phone = txbPhone.Text,
                Email = txbEmail.Text,
                AccountUserName = txbAccountUserName.Text
            };

            bool result = StaffDAO.Instance.UpdateStaffAdmin(updatedStaff);
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

