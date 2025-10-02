using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserLoginApp.Helpers;
using UserLoginApp.Models;
using UserLoginApp.Utils;
using UserLoginApp.Repositories;

namespace UserLoginApp.Forms
{
    public partial class LoginForm : Form
    {
        private DatabaseHelper db;

        public LoginForm()
        {
            InitializeComponent();
            db = new DatabaseHelper();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Hash password trước khi truyền vào DB
            string hashedPassword = Security.HashPassword(password);

            User loggedUser = db.Login(username, hashedPassword);

            if (loggedUser != null)
            {
                MessageBox.Show("Đăng nhập thành công!");
                MainForm main = new MainForm(loggedUser, db);
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            reg.Show();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

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

        private void LoginForm_Load(object sender, EventArgs e)
        {
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
        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Mật khẩu")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true; // ẩn mật khẩu khi gõ
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
    }
}