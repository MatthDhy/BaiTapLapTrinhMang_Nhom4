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
            using (var loginForm = new LoginForm())
            {
                loginForm.ShowDialog();
            }
            this.Close();
        }

        private void grUserInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
