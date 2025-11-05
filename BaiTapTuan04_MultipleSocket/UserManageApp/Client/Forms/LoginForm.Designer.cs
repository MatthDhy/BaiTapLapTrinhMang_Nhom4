// LoginForm.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UserManageApp.Forms
{
    partial class LoginForm
    {
        private IContainer components = null;

        private Panel panelShell;
        private Label lblTitle;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private LinkLabel lnkRegister;
        private LinkLabel lnkForgot;
        private Label lblError;
        private Button btnTogglePwd;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new Container();
            this.lblTitle = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lnkRegister = new LinkLabel();
            this.lnkForgot = new LinkLabel();
            this.lblError = new Label();
            this.btnTogglePwd = new Button();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new Point(229, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(342, 81);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đăng nhập";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new Font("Segoe UI", 14F);
            this.txtUsername.Location = new Point(165, 129);
            this.txtUsername.Multiline = false;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(420, 39);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Tag = "Tên đăng nhập";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new Font("Segoe UI", 14F);
            this.txtPassword.Location = new Point(165, 180);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(420, 39);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Tag = "Mật khẩu";
            // 
            // btnTogglePwd
            // 
            this.btnTogglePwd.Font = new Font("Segoe UI", 10F);
            this.btnTogglePwd.Location = new Point(595, 180);
            this.btnTogglePwd.Name = "btnTogglePwd";
            this.btnTogglePwd.Size = new Size(42, 39);
            this.btnTogglePwd.TabIndex = 7;
            this.btnTogglePwd.Text = "👁";
            this.btnTogglePwd.UseVisualStyleBackColor = true;
            this.btnTogglePwd.Click += new EventHandler(this.btnTogglePwd_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = Color.RoyalBlue;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(165, 240);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(472, 58);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            // 
            // lnkRegister
            // 
            this.lnkRegister.AutoSize = true;
            this.lnkRegister.BackColor = Color.Transparent;
            this.lnkRegister.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            this.lnkRegister.LinkColor = Color.Black;
            this.lnkRegister.Location = new Point(294, 313);
            this.lnkRegister.Name = "lnkRegister";
            this.lnkRegister.Size = new Size(63, 20);
            this.lnkRegister.TabIndex = 4;
            this.lnkRegister.TabStop = true;
            this.lnkRegister.Text = "Đăng ký";
            this.lnkRegister.Click += new EventHandler(this.btnRegister_Click);
            // 
            // lnkForgot
            // 
            this.lnkForgot.AutoSize = true;
            this.lnkForgot.BackColor = Color.Transparent;
            this.lnkForgot.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            this.lnkForgot.LinkColor = Color.Black;
            this.lnkForgot.Location = new Point(386, 313);
            this.lnkForgot.Name = "lnkForgot";
            this.lnkForgot.Size = new Size(109, 20);
            this.lnkForgot.TabIndex = 5;
            this.lnkForgot.TabStop = true;
            this.lnkForgot.Text = "Quên mật khẩu";
            // 
            // lblError
            // 
            this.lblError.AutoSize = false;
            this.lblError.BackColor = Color.Transparent;
            this.lblError.Font = new Font("Segoe UI", 9F);
            this.lblError.ForeColor = Color.Red;
            this.lblError.Location = new Point(165, 205);
            this.lblError.Name = "lblError";
            this.lblError.Size = new Size(472, 25);
            this.lblError.TabIndex = 6;
            this.lblError.Text = "";
            // 
            // LoginForm (form)
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.LightGray;
            this.ClientSize = new Size(804, 440);
            this.Controls.Add(this.btnTogglePwd);
            this.Controls.Add(this.lnkForgot);
            this.Controls.Add(this.lnkRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.Load += new EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
