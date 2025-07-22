using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO
{
    public class Account
    {
        public Account(string username, int type, string password = null)
        {
            this.username = username;
            this.password = password;
            this.type = type;
        }

        public Account(DataRow row)
        {
            this.username = row["UserName"].ToString();
            this.password = row["Password"].ToString();
            this.type = Convert.ToInt32(row["Type"]);
            this.isactive = Convert.ToBoolean(row["isActive"]);
        }

        private string username;
        private string password;
        private int type;
        private bool isactive;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int Type { get => type; set => type = value; }
        public bool Isactive { get => isactive; set => isactive = value; }
    }
}
