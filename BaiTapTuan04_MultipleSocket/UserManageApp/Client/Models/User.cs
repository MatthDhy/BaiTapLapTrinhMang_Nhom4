using System;

namespace UserManageApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Constructor mặc định
        public User()
        {
            CreatedAt = DateTime.Now;
        }

        // Constructor tiện dụng khi cần tạo nhanh 1 user mới
        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            CreatedAt = DateTime.Now;
        }

        // Tùy chọn: format để gửi qua TCP (nếu cần)
        public override string ToString()
        {
            return $"{UserID}|{Username}|{Email}|{CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
