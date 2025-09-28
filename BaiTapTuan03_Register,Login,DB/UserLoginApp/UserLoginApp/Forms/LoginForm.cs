using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserLoginApp.Helpers;
using UserLoginApp.Repositories;
using System.Configuration;

namespace UserLoginApp.Forms
{
    public partial class LoginForm : Form
    {
        private readonly DatabaseHelper _db;
        private readonly UserRepository _repo;
        public LoginForm(DatabaseHelper db)
        {
            InitializeComponent();
            _db = db;
            _repo = new UserRepository(_db);
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            // TODO: Xử lý login
        }

        private void btnRegister_Click(object sender, System.EventArgs e)
        {
            // TODO: Mở form đăng ký
        }

        
    }
}
