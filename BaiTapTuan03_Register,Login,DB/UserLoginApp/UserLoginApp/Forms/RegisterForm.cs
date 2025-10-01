using System;
using System.Windows.Forms;
using UserLoginApp.Models;
using UserLoginApp.Utils;      // để dùng Validator + Security
using UserLoginApp.Helpers;   // để dùng DatabaseHelper

namespace UserLoginApp.Forms
{
    public partial class RegisterForm : Form
    {
        private readonly DatabaseHelper _db;

        public RegisterForm()
        {
            InitializeComponent();
            _db = new DatabaseHelper(); // lấy connection từ App.config
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text.Trim();

            string errorMessage;

            // Validate username
            if (!Validator.IsValidUsername(username, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate password
            if (!Validator.IsValidPassword(password, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm password
            if (!Validator.IsPasswordConfirmed(password, confirmPassword, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate email
            if (!Validator.IsValidEmail(email, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo user object
            User newUser = new User
            {
                Username = username,
                PasswordHash = Security.HashPassword(password), // hash trước khi lưu
                Email = email
            };

            // Gọi DB
            if (_db.Register(newUser, out string dbError))
            {
                MessageBox.Show("Đăng ký thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // đóng RegisterForm
            }
            else
            {
                MessageBox.Show(dbError, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
