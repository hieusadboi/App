using System;
using System.Collections.Generic;
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
        public bool Login(string username, string password)
        {
            string query = "EXEC dbo.USP_Login @UserName , @Password";
            var result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return result.Rows.Count > 0;
        }
    }
}
