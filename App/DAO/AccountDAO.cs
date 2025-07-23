using App.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using BCrypt.Net;

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

        // ✅ Hàm hash mật khẩu
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // ✅ Hàm kiểm tra mật khẩu
        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

        /// <summary>
        /// Login kiểm tra hash
        /// </summary>
        public int Login(string username, string password)
        {
            Account acc = GetAccountByUsername(username);
            if (acc == null) return -1; // Không tìm thấy tài khoản

            if (!acc.Isactive) return 0; // Tài khoản bị khóa

            bool check = VerifyPassword(password, acc.Password);
            return check ? 1 : -1; // 1: Đúng mật khẩu, -1: Sai mật khẩu
        }

        public Account GetAccountByUsername(string username)
        {
            string query = "SELECT * FROM dbo.Account WHERE UserName = @UserName";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username });
            if (result.Rows.Count > 0)
            {
                return new Account(result.Rows[0]);
            }
            return null;
        }

        public DataTable GetListAccount()
        {
            string query = "SELECT UserName ,Type , isActive FROM dbo.Account";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Thêm tài khoản mới với mật khẩu mặc định "123456" (hash)
        /// </summary>
        public bool InsertAccount(string username, int type, bool isActive)
        {
            string hashedPassword = HashPassword("123456"); // Mật khẩu mặc định
            string query = $"INSERT INTO Account (UserName, PassWord, Type, isActive) " +
                           $"VALUES (N'{username}', N'{hashedPassword}', {type}, {(isActive ? 1 : 0)})";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool UpdateAccount(string username, int type, bool isActive)
        {
            string query = $"UPDATE Account SET Type = {type}, isActive = {(isActive ? 1 : 0)} WHERE UserName = N'{username}'";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        /// <summary>
        /// Reset mật khẩu về mặc định "123456" (hash)
        /// </summary>
        public bool ResetPassword(string username)
        {
            string hashedPassword = HashPassword("123456");
            string query = $"UPDATE Account SET PassWord = N'{hashedPassword}' WHERE UserName = N'{username}'";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public List<Account> SearchAccountByUsername(string keyword)
        {
            List<Account> list = new List<Account>();
            string query = "SELECT * FROM Account WHERE UserName LIKE @name";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { "%" + keyword + "%" });

            foreach (DataRow row in data.Rows)
            {
                list.Add(new Account(row));
            }

            return list;
        }

        public bool UpdatePassword(string username, string oldPassword, string newPassword)
        {
            string query = "SELECT * FROM Account WHERE userName = @username";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username });

            if (data.Rows.Count == 0) return false;

            string hashedPassword = data.Rows[0]["password"].ToString();
            if (!VerifyPassword(oldPassword, hashedPassword)) return false; // sai mật khẩu cũ

            string newHashedPassword = HashPassword(newPassword);
            string updateQuery = "UPDATE Account SET password = @password WHERE userName = @username";
            int result = DataProvider.Instance.ExecuteNonQuery(updateQuery, new object[] { newHashedPassword, username });
            return result > 0;
        }

    }
}
