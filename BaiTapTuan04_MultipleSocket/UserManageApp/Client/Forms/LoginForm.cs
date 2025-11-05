// LoginForm.cs
using Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManageApp.Networking;
using UserManageApp.Utils;

namespace UserManageApp.Forms
{
    public partial class LoginForm : Form
    {
        private const string SERVER_HOST = "127.0.0.1";
        private const int SERVER_PORT = 8080;

        private TcpClientHelper _tcpClient;

        public LoginForm()
        {
            InitializeComponent();

            _tcpClient = new TcpClientHelper();

            // nền gradient nhẹ
            this.Paint += LoginForm_Paint;

            // kéo form không viền
            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left) NativeDrag(this.Handle);
            };

            // Placeholder: khởi tạo ban đầu sẽ được cập nhật ở Load
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

        // --- Single Load handler (async)
        private async void LoginForm_Load(object sender, EventArgs e)
        {
            // Accept button
            this.AcceptButton = btnLogin;

            // Setup placeholders (use Tag from Designer)
            SetupPlaceholder(txtUsername, txtUsername.Tag?.ToString() ?? "Tên đăng nhập", false);
            SetupPlaceholder(txtPassword, txtPassword.Tag?.ToString() ?? "Mật khẩu", true);

            // Wire focus handlers for more UX (in case)
            txtUsername.Enter += TxtUsername_Enter;
            txtUsername.Leave += TxtUsername_Leave;
            txtPassword.Enter += TxtPassword_Enter;
            txtPassword.Leave += TxtPassword_Leave;

            this.ActiveControl = lblTitle;

            // Optionally connect to server in background (best effort)
            try
            {
                if (_tcpClient == null) _tcpClient = new TcpClientHelper();
                await _tcpClient.ConnectAsync(SERVER_HOST, SERVER_PORT);
                Console.WriteLine("✅ Đã kết nối server.");
            }
            catch
            {
                // Không crash UI nếu không kết nối được, chỉ show message mặc định vào lblError
                lblError.Text = "Không kết nối được server (chỉ thử kết nối khi mở).";
            }
        }

        // --- Unified login click (async)
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

            // Tạo JSON request (server mà bạn dùng trước đó)
            var reqObj = new
            {
                Action = "LOGIN",
                Data = new { Username = username, Password = Security.Sha256Hex(password) }
            };

            try
            {
                if (_tcpClient == null) _tcpClient = new TcpClientHelper();
                await _tcpClient.ConnectAsync(SERVER_HOST, SERVER_PORT);

                string reqJson = JsonSerializer.Serialize(reqObj);
                string respJson = await _tcpClient.SendAsync(reqJson);

                // parse response
                var resp = JsonDocument.Parse(respJson).RootElement;
                bool success = resp.GetProperty("Success").GetBoolean();
                string message = resp.TryGetProperty("Message", out var m) ? m.GetString() : "";

                if (!success)
                {
                    lblError.Text = "Đăng nhập thất bại: " + message;
                    return;
                }

                var data = resp.GetProperty("Data");
                string token = data.GetProperty("Token").GetString();
                string userJson = data.GetProperty("User").GetRawText();

                // Gọi Constructor phù hợp với MainForm của bạn
                var main = new MainForm(token, userJson, _tcpClient);
                this.Hide();

                main.FormClosed += (s2, e2) =>
                {
                    try { _tcpClient?.Dispose(); } catch { }
                    _tcpClient = new TcpClientHelper();
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
            using (var reg = new RegisterForm())
            {
                reg.ShowDialog();
            }
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
            if (tb == null) return;

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

            // Set initial placeholder only if empty
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                SetPh();
            }

            tb.Tag = placeholder;

            tb.GotFocus -= (s, e) => { }; // ensure not double-wired
            tb.LostFocus -= (s, e) => { };

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
            => tb == null ? string.Empty : (tb.ForeColor == Color.Gray ? string.Empty : tb.Text.Trim());

        // ---- Textbox enter/leave (for Designer placeholders fallback) ----
        private void TxtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == (string)txtUsername.Tag)
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.White;
            }
        }

        private void TxtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = (string)txtUsername.Tag;
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == (string)txtPassword.Tag)
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.White;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = (string)txtPassword.Tag;
                txtPassword.ForeColor = Color.Gray;
            }
        }

        // ---- Kéo form không viền ----
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private static void NativeDrag(IntPtr handle)
        {
            const int WM_NCLBUTTONDOWN = 0xA1;
            const int HTCAPTION = 0x2;
            ReleaseCapture();
            SendMessage(handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _tcpClient?.Dispose();
        }
    }
}
