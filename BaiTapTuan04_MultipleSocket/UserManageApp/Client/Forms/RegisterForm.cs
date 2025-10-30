using System;
using System.Windows.Forms;
using UserManageApp.Models;
using UserManageApp.Networking;   // để dùng DatabaseHelper
using UserManageApp.Utils;      // để dùng Validator + Security

namespace UserManageApp.Forms
{
    public partial class RegisterForm : Form
    {
        private readonly TcpClientHelper _client;

        public RegisterForm()
        {
            InitializeComponent();
            _client = new TcpClientHelper();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text.Trim();

            string errorMessage;

            // Kiểm tra username
            if (!Validator.IsValidUsername(username, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra password
            if (!Validator.IsValidPassword(password, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xác nhận mật khẩu
            if (!Validator.IsPasswordConfirmed(password, confirmPassword, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra email
            if (!Validator.IsValidEmail(email, out errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Kết nối tới server TCP (đổi host/port nếu cần)
                await _client.ConnectAsync("127.0.0.1", 8080);

                // Hash mật khẩu trước khi gửi
                string hashedPassword = Security.HashPassword(password);

                // Tạo message theo định dạng mà server của bạn hiểu
                string message = $"REGISTER|{username}|{hashedPassword}|{email}";

                // Gửi đến server và nhận phản hồi
                string response = await _client.SendAsync(message);

                if (response.Equals("OK", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Đăng ký thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // đóng form đăng ký
                }
                else
                {
                    MessageBox.Show($"Đăng ký thất bại: {response}", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kết nối server: {ex.Message}", "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _client?.Dispose();
            base.OnFormClosed(e);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // có thể thêm auto-check username trống tại đây nếu muốn
        }
    }
}
