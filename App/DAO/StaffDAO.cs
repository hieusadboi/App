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


        public List<Staff> GetAllStaff()
        {
            List<Staff> list = new List<Staff>();
            string query = "SELECT * FROM Staff";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new Staff(row));
            }

            return list;
        }


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

        public List<string> GetAccountsWithoutStaff()
        {
            string query = @"
        SELECT UserName 
        FROM Account 
        WHERE UserName NOT IN (SELECT accountUserName FROM Staff)";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<string> list = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                list.Add(row["UserName"].ToString());
            }
            return list;
        }

        public bool InsertStaff(Staff staff)
        {
            // Check nếu tài khoản đã có staff thì không cho thêm
            if (GetStaffByAccount(staff.AccountUserName) != null)
                return false;

            string query = @"
        INSERT INTO Staff ( fullName , gender , birthDate , phone , email , accountUserName )
        VALUES ( @fullName , @gender , @birthDate , @phone , @email , @accountUserName )";
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

        public bool DeleteStaffById(int id)
        {
            string query = "DELETE FROM Staff WHERE idStaff = @id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }


    }
}
