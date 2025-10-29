using Client;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManageApp.Models;
using UserManageApp.Networking;
using UserManageApp.Utils;

namespace UserManageApp.Forms
{
    public partial class LoginForm : Form
    {
        private TcpClientHelper _tcpClient;

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                _tcpClient = new TcpClientHelper();
                await _tcpClient.ConnectAsync("127.0.0.1", 8080); // ⚙️ Thay bằng IP/Port server thật
                Console.WriteLine("✅ Đã kết nối server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối tới server: " + ex.Message,
                    "Kết nối thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Cài đặt placeholder
            txtUsername.Text = "Tên đăng nhập";
            txtUsername.ForeColor = Color.Gray;

            txtPassword.Text = "Mật khẩu";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.UseSystemPasswordChar = false;

            txtUsername.Enter += TxtUsername_Enter;
            txtUsername.Leave += TxtUsername_Leave;
            txtPassword.Enter += TxtPassword_Enter;
            txtPassword.Leave += TxtPassword_Leave;
            this.ActiveControl = lblTitle;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || username == "Tên đăng nhập" ||
                string.IsNullOrEmpty(password) || password == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mã hóa mật khẩu trước khi gửi
            string hashedPassword = Security.HashPassword(password);
            string message = $"LOGIN|{username}|{hashedPassword}";

            try
            {
                string response = await _tcpClient.SendAsync(message);

                if (response == "OK")
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!",
                        "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi yêu cầu đến server: " + ex.Message,
                    "Lỗi mạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            reg.Show();
        }

        private void TxtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Tên đăng nhập")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }

        private void TxtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Tên đăng nhập";
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Mật khẩu")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "Mật khẩu";
                txtPassword.ForeColor = Color.Gray;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _tcpClient?.Dispose();
        }
    }
}
