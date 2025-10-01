using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using UserLoginApp.Models;

namespace UserLoginApp.Helpers
{
    public class DatabaseHelper
    {
        
        private readonly string _connStr;

        public DatabaseHelper(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
                _connStr = connectionString;
            else
                _connStr = ConfigurationManager.ConnectionStrings["CaroAppDB"].ConnectionString;
        }

        // Thực thi query trả về DataTable
        public DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // Thực thi query insert/update/delete
        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Trả về giá trị đơn (count, identity, scalar)
        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        // Đăng ký tài khoản mới
        public bool Register(User user, out string errorMessage)
        {
            try
            {
                // Kiểm tra trùng username hoặc email
                string sqlCheck = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
                object result = ExecuteScalar(sqlCheck,
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@Email", user.Email));

                if (Convert.ToInt32(result) > 0)
                {
                    errorMessage = "Username hoặc Email đã tồn tại.";
                    return false;
                }

                // Insert user
                string sqlInsert = "INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)";
                int rows = ExecuteNonQuery(sqlInsert,
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@Password", user.PasswordHash), // đã hash
                    new SqlParameter("@Email", user.Email));

                if (rows > 0)
                {
                    errorMessage = string.Empty;
                    return true;
                }
                else
                {
                    errorMessage = "Đăng ký thất bại.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi DB: " + ex.Message;
                return false;
            }
        }

        // Đăng nhập
        public User Login(string username, string hashedPassword)
        {
            try
            {
                string sql = "SELECT TOP 1 UserId, Username, Email FROM Users WHERE Username = @Username AND Password = @Password";
                DataTable dt = ExecuteQuery(sql,
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", hashedPassword));

                if (dt.Rows.Count > 0)
                {
                    return new User
                    {
                        UserID = Convert.ToInt32(dt.Rows[0]["UserId"]),
                        Username = dt.Rows[0]["Username"].ToString(),
                        Email = dt.Rows[0]["Email"].ToString(),
                        PasswordHash = hashedPassword
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
