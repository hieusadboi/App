using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    public class StaffDAO
    {
        private static StaffDAO instance;
        public static StaffDAO Instance
        {
            get { if (instance == null) instance = new StaffDAO(); return instance; }
            private set { instance = value; }
        }

        private StaffDAO() { }

        public Staff GetStaffByAccount(string userName)
        {
            string query = "SELECT * FROM Staff WHERE accountUserName = @userName";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });

            if (data.Rows.Count > 0)
                return new Staff(data.Rows[0]);

            return null;
        }

        public bool UpdateStaff(Staff staff)
        {
            string query = "EXEC USP_UpdateStaff @fullName , @gender , @birthDate , @phone , @email , @accountUserName";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[]
            {
                staff.FullName,
                staff.Gender,
                staff.BirthDate,
                staff.Phone,
                staff.Email,
                staff.AccountUserName
            });

            return result > 0;
        }
    }
}
