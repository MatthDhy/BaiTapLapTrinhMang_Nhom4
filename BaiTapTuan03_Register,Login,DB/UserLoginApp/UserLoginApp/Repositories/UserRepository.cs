using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using UserLoginApp.Helpers;
using UserLoginApp.Models;

namespace UserLoginApp.Repositories
{
    public class UserRepository
    {
        private readonly DatabaseHelper _db;
        public UserRepository(DatabaseHelper db) => _db = db;

        public int AddUser(User u)
        {
            string sql = "INSERT INTO Users (Username, PasswordHash, Email) VALUES (@u,@p,@e); SELECT SCOPE_IDENTITY();";
            object idObj = _db.ExecuteScalar(sql,
                new SqlParameter("@u", u.Username),
                new SqlParameter("@p", u.PasswordHash),
                new SqlParameter("@e", string.IsNullOrEmpty(u.Email) ? (object)DBNull.Value : u.Email)
            );
            return Convert.ToInt32(idObj);
        }

        public bool CheckLogin(string username, string passwordHash)
        {
            string sql = "SELECT COUNT(1) FROM Users WHERE Username=@u AND PasswordHash=@p";
            object obj = _db.ExecuteScalar(sql,
                new SqlParameter("@u", username),
                new SqlParameter("@p", passwordHash)
            );
            return Convert.ToInt32(obj) > 0;
        }

        public User GetByUsername(string username)
        {
            string sql = "SELECT TOP 1 * FROM Users WHERE Username=@u";
            DataTable dt = _db.ExecuteQuery(sql, new SqlParameter("@u", username));
            if (dt.Rows.Count == 0) return null;
            var r = dt.Rows[0];
            return new User
            {
                UserID = Convert.ToInt32(r["UserID"]),
                Username = r["Username"].ToString() ?? "",
                PasswordHash = r["PasswordHash"].ToString() ?? "",
                Email = r["Email"]?.ToString() ?? "",
                CreatedAt = Convert.ToDateTime(r["CreatedAt"])
            };
        }
    }
}
