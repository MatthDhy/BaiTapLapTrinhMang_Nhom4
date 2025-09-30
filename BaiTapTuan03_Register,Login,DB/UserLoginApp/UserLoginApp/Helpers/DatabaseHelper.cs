using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.Models;
using System.Configuration;

namespace UserLoginApp.Helpers
{
    public class DatabaseHelper
    {
        public User Login(string username, string password)
        {
            // TODO: thay bằng kết nối SQL sau
            if (username == "admin" && password == "123")
            {
                return new User
                {
                    Username = username,
                    Email = "admin@example.com"
                };
            }
            return null;
        }
        private readonly string _connStr;

        public DatabaseHelper(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
                _connStr = connectionString;
            else
                _connStr = ConfigurationManager.ConnectionStrings["CaroAppDB"].ConnectionString;
        }

        //Hàm ExecuteQuery này dùng để thực thi cái query á nha
        public DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
