using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using UserLoginApp.Models;

namespace UserLoginApp
{
    public class DatabaseHelper
    {
        private string connectionString = "Server=localhost;Database=UserDB;Trusted_Connection=True;";

        public bool Register(User user)
        {
            // TODO: Thêm logic insert user vào DB
            return false;
        }

        public User? Login(string username, string password)
        {
            // TODO: Thêm logic kiểm tra login trong DB
            return null;
        }
    }
}
