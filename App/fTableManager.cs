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
            // Assuming 'Table' is a class with properties 'Name' and 'Status'
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                flpTable.Controls.Add(btn);
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightCoral;
                        break;
                }
            }

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
    }

    // Assuming the Table class exists

}
