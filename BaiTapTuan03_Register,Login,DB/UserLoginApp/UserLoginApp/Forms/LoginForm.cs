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

            User loggedUser = db.Login(username, password);

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
    }
}
