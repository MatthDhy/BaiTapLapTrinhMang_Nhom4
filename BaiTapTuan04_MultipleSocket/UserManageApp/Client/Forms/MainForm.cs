using Client;
using System;
using System.Text.Json;
using System.Windows.Forms;
using UserManageApp.Forms;
using UserManageApp.Networking;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client
{
    public partial class MainForm : Form
    {
        private const string ServerIP = "127.0.0.1"; // đổi sang IP máy khác khi cần
        private const int ServerPort = 8080;

        private readonly string _token;
        private readonly string _userJson;
        private readonly TcpClientHelper _tcpClient;

        public MainForm(string token, string userJson, TcpClientHelper tcpClient)
        {
            InitializeComponent();
            _token = token;
            _userJson = userJson;
            _tcpClient = tcpClient;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
 

            var u = JsonSerializer.Deserialize<JsonElement>(_userJson);
            lblWelcome.Text = $"Xin chào, {u.GetProperty("Username").GetString()}!";
            lblUsername.Text = $"Tên đăng nhập: {u.GetProperty("Username").GetString()}";
            lblEmail.Text = $"Email: {u.GetProperty("Email").GetString()}";
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            var req = new { Action = "LOGOUT", Data = new { Token = _token } };
            string json = JsonSerializer.Serialize(req);

            try
            {
                // Kết nối nếu chưa kết nối
                if (!_tcpClient.IsConnected)
                {
                    await _tcpClient.ConnectAsync(ServerIP, ServerPort);
                }

                // Gửi yêu cầu logout và nhận phản hồi
                var response = await _tcpClient.SendAsync(json);

                if (response == "OK")
                {
                    MessageBox.Show("Đăng xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Đăng xuất thất bại: " + response, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi yêu cầu logout: " + ex.Message,
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Hide();
                using (var loginForm = new LoginForm())
                {
                    loginForm.ShowDialog();
                }
                this.Close();
            }
        }


        public class UserModel
        {
            public string Username { get; set; }
            public string Email { get; set; }
        }
    }
}


