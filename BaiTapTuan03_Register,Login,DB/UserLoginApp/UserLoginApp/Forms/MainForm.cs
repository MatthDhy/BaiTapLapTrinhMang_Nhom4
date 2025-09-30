using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserLoginApp.Helpers;
using UserLoginApp.Models;

namespace UserLoginApp.Forms
{
    public partial class MainForm : Form
    {
        private User currentUser;
        private DatabaseHelper db;

        public MainForm(User user, DatabaseHelper db)
        {
            InitializeComponent();
            this.currentUser = user;
            this.db = db;
            // TODO: Hiển thị thông tin user
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                lbWelcome.Text = "Xin chào, " + this.currentUser.Username;
                lbUsername.Text = "Tên đăng nhập: " + this.currentUser.Username;
                lbEmail.Text = "Email: " + this.currentUser.Email;
            }
            grUserInfo.Text = "Thông tin đăng nhập";
            btnLogOut.Text = "Đăng xuất";
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var loginForm = new LoginForm(db))
            {
                loginForm.ShowDialog();
            }
            this.Close();
        }
        public static class SecurityHelper
        {
            public static string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(password);
                    byte[] hash = sha256.ComputeHash(bytes);

                    StringBuilder sb = new StringBuilder();
                    foreach (var b in hash)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
        }

    }
}
