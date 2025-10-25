using Server.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class DbHelper
    {
        private readonly string _connStr;
        public DbHelper(string connStr) => _connStr = connStr;

        public bool UsernameExists(string username)
        {
            using (var cn = new SqlConnection(_connStr))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Username=@u", cn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public bool CreateUser(User user)
        {
            using (var cn = new SqlConnection(_connStr))
            {
                cn.Open();
                using (var cmd = new SqlCommand("INSERT INTO Users (Username,Password,Email,FullName,Birthday) VALUES(@u,@p,@e,@f,@b);", cn))
                {
                    cmd.Parameters.AddWithValue("@u", user.Username);
                    cmd.Parameters.AddWithValue("@p", user.Password);
                    cmd.Parameters.AddWithValue("@e", (object)user.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@f", (object)user.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@b", (object)user.Birthday ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public User GetUserByCredentials(string username, string password)
        {
            using (var cn = new SqlConnection(_connStr))
            { 
                cn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM Users WHERE Username=@u AND Password=@p", cn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return null;
                        return new User
                        {
                            UserId = (int)r["UserId"],
                            Username = r["Username"].ToString(),
                            Password = r["Password"].ToString(),
                            Email = r["Email"] as string,
                            FullName = r["FullName"] as string,
                            Birthday = r["Birthday"] as DateTime?
                        };
                    }
                }
            }
        }
    }
}
