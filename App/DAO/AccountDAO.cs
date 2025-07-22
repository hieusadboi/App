using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAO
{
    internal class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }
        public int Login(string username, string password)
        {
            string query = "EXEC dbo.USP_Login @UserName , @Password";
            var result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });

            if (result.Rows.Count > 0)
            {
                return Convert.ToInt32(result.Rows[0]["ResultCode"]);
            }

            return -99; // Lỗi không xác định
        }

        public Account GetAccountByUsername(string username)
        {
            string query = "SELECT * FROM dbo.Account WHERE UserName = @UserName";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username });
            if (result.Rows.Count > 0)
            {
                return new Account(result.Rows[0]);
            }
            return null; // Không tìm thấy tài khoản
        }

        public DataTable GetListAccount()
        {
            string query = "SELECT UserName ,Type , isActive FROM dbo.Account";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            
            return data;
        }

        public bool InsertAccount(string username, int type, bool isActive)
        {
            string query = $"INSERT INTO Account (UserName, Type, isActive) " +
                           $"VALUES (N'{username}', {type}, {(isActive ? 1 : 0)})";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateAccount(string username, int type, bool isActive)
        {
            string query = $"UPDATE Account SET Type = {type}, isActive = {(isActive ? 1 : 0)} WHERE UserName = N'{username}'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool ResetPassword(string username) 
        {
            string query = $"UPDATE Account SET Password = N'123456' WHERE UserName = N'{username}'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

    }
}
