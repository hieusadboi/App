using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTO
{
    public class Staff
    {
        public int IdStaff { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AccountUserName { get; set; }

        public Staff() { }

        public Staff(DataRow row)
        {
            IdStaff = Convert.ToInt32(row["idStaff"]);
            FullName = row["fullName"].ToString();
            Gender = row["gender"].ToString();
            BirthDate = Convert.ToDateTime(row["birthDate"]);
            Phone = row["phone"].ToString();
            Email = row["email"].ToString();
            AccountUserName = row["accountUserName"].ToString();
        }
    }
}
