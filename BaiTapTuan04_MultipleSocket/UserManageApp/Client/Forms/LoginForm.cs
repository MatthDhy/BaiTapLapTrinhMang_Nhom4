using Client; // TcpClientHelper + Security
using Exercise3_Client.Utils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using SecurityUtil = Exercise3_Client.Utils.Security;

namespace Client.Forms
{
    public partial class LoginForm : Form
    {
        private const string SERVER_HOST = "127.0.0.1";
        private const int SERVER_PORT = 9000;

        private TcpClientHelper _tcpClient;

        public LoginForm()
        {
            InitializeComponent();

            _tcpClient = new TcpClientHelper();

            // nền gradient nhẹ (OK với Designer)
            this.Paint += LoginForm_Paint;

            // kéo form không viền
            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left) NativeDrag(this.Handle);
            };

            // placeholder thủ công cho .NET Framework
            SetupPlaceholder(txtUsername, (string)txtUsername.Tag, false);
            SetupPlaceholder(txtPassword, (string)txtPassword.Tag, true);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnLogin;
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            using (var br = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(18, 24, 36),
                Color.FromArgb(10, 14, 22),
                90f))
            {
                e.Graphics.FillRectangle(br, this.ClientRectangle);
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;

            var username = GetValue(txtUsername);
            var password = GetValue(txtPassword);

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Vui lòng nhập tên đăng nhập và mật khẩu.";
                return;
            }

            var reqObj = new
            {
                Action = "LOGIN",
                Data = new { Username = username, Password = Security.Sha256Hex(password) }
            };

            try
            {
                await _tcpClient.ConnectAsync(SERVER_HOST, SERVER_PORT);

                string reqJson = JsonSerializer.Serialize(reqObj);
                string respJson = await _tcpClient.SendAsync(reqJson);

                var resp = JsonDocument.Parse(respJson).RootElement;
                bool success = resp.GetProperty("Success").GetBoolean();
                string message = resp.GetProperty("Message").GetString();

                if (!success)
                {
                    lblError.Text = "Đăng nhập thất bại: " + message;
                    return;
                }

                var data = resp.GetProperty("Data");
                string token = data.GetProperty("Token").GetString();
                string userJson = data.GetProperty("User").GetRawText();

                var main = new MainForm(token, userJson, _tcpClient);
                this.Hide();

                main.FormClosed += (s2, e2) =>
                {
                    try { _tcpClient?.Dispose(); } catch { }
                    _tcpClient = new TcpClientHelper(); // để đăng nhập lại nếu cần
                    this.Show();
                };

                main.Show();
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi kết nối: " + ex.Message;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            new RegisterForm().ShowDialog();
        }

        private void btnTogglePwd_Click(object sender, EventArgs e)
        {
            bool isPlaceholder = txtPassword.ForeColor == Color.Gray &&
                                 txtPassword.Text == (string)txtPassword.Tag;
            if (isPlaceholder) return;

            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            btnTogglePwd.Text = txtPassword.UseSystemPasswordChar ? "👁" : "🚫";
            txtPassword.Focus();
        }

        // ---- Placeholder helpers ----
        private static void SetupPlaceholder(TextBox tb, string placeholder, bool isPassword)
        {
            void SetPh()
            {
                tb.ForeColor = Color.Gray;
                tb.Text = placeholder;
                if (isPassword) tb.UseSystemPasswordChar = false;
            }
            void UnsetPh()
            {
                tb.ForeColor = Color.White;
                tb.Text = string.Empty;
                if (isPassword) tb.UseSystemPasswordChar = true;
            }

            SetPh();
            tb.GotFocus += (s, e) =>
            {
                if (tb.ForeColor == Color.Gray && tb.Text == placeholder) UnsetPh();
            };
            tb.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text)) SetPh();
            };
        }

        private static string GetValue(TextBox tb)
            => tb.ForeColor == Color.Gray ? string.Empty : tb.Text.Trim();

        // ---- Kéo form không viền ----
        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private static void NativeDrag(IntPtr handle)
        {
            const int WM_NCLBUTTONDOWN = 0xA1;
            const int HTCAPTION = 0x2;
            ReleaseCapture();
            SendMessage(handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
    }
}