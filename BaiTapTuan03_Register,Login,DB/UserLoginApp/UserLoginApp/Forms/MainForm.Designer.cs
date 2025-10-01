using System;

namespace UserLoginApp.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

      

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grUserInfo = new System.Windows.Forms.GroupBox();
            this.lbEmail = new System.Windows.Forms.Label();
            this.lbUsername = new System.Windows.Forms.Label();
            this.lbWelcome = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.grUserInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grUserInfo
            // 
            this.grUserInfo.BackColor = System.Drawing.Color.Ivory;
            this.grUserInfo.Controls.Add(this.lbEmail);
            this.grUserInfo.Controls.Add(this.lbUsername);
            this.grUserInfo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grUserInfo.Location = new System.Drawing.Point(56, 93);
            this.grUserInfo.Name = "grUserInfo";
            this.grUserInfo.Size = new System.Drawing.Size(580, 180);
            this.grUserInfo.TabIndex = 4;
            this.grUserInfo.TabStop = false;
            this.grUserInfo.Text = "Thông tin đăng nhập";
            this.grUserInfo.Enter += new System.EventHandler(this.grUserInfo_Enter);
            // 
            // lbEmail
            // 
            this.lbEmail.Location = new System.Drawing.Point(30, 88);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(500, 30);
            this.lbEmail.TabIndex = 1;
            this.lbEmail.Text = "Email: ";
            // 
            // lbUsername
            // 
            this.lbUsername.Location = new System.Drawing.Point(30, 40);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(500, 30);
            this.lbUsername.TabIndex = 0;
            this.lbUsername.Text = "Tên đăng nhập: ";
            // 
            // lbWelcome
            // 
            this.lbWelcome.BackColor = System.Drawing.Color.Ivory;
            this.lbWelcome.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWelcome.Location = new System.Drawing.Point(20, 20);
            this.lbWelcome.Name = "lbWelcome";
            this.lbWelcome.Size = new System.Drawing.Size(400, 40);
            this.lbWelcome.TabIndex = 3;
            this.lbWelcome.Text = "Xin chào,";
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Location = new System.Drawing.Point(330, 330);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(140, 40);
            this.btnLogOut.TabIndex = 5;
            this.btnLogOut.Text = "Đăng xuất";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UserLoginApp.Properties.Resources.czNmcy1wcml2YXRlL3Jhd3BpeGVsX2ltYWdlcy93ZWJzaXRlX2NvbnRlbnQvdjExNTUtYi0wMTEteC5qcGc;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grUserInfo);
            this.Controls.Add(this.lbWelcome);
            this.Controls.Add(this.btnLogOut);
            this.Name = "MainForm";
            this.Text = "Trang chủ";
            this.grUserInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grUserInfo;
        private System.Windows.Forms.Label lbEmail;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label lbWelcome;
        private System.Windows.Forms.Button btnLogOut;
    }
}