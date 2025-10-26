using System;

namespace Client
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
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.grUserInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grUserInfo
            // 
            this.grUserInfo.BackColor = System.Drawing.Color.Ivory;
            this.grUserInfo.Controls.Add(this.lblEmail);
            this.grUserInfo.Controls.Add(this.lblUsername);
            this.grUserInfo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grUserInfo.Location = new System.Drawing.Point(56, 93);
            this.grUserInfo.Name = "grUserInfo";
            this.grUserInfo.Size = new System.Drawing.Size(580, 180);
            this.grUserInfo.TabIndex = 4;
            this.grUserInfo.TabStop = false;
            this.grUserInfo.Text = "Thông tin đăng nhập";
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(30, 88);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(500, 30);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email: ";
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(30, 40);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(500, 30);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Tên đăng nhập: ";
            // 
            // lblWelcome
            // 
            this.lblWelcome.BackColor = System.Drawing.Color.Ivory;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(400, 40);
            this.lblWelcome.TabIndex = 3;
            this.lblWelcome.Text = "Xin chào,";
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.Location = new System.Drawing.Point(298, 330);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(172, 40);
            this.btnLogOut.TabIndex = 5;
            this.btnLogOut.Text = "Đăng xuất";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Client.Properties.Resources.czNmcy1wcml2YXRlL3Jhd3BpeGVsX2ltYWdlcy93ZWJzaXRlX2NvbnRlbnQvdjExNTUtYi0wMTEteC5qcGc;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grUserInfo);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnLogOut);
            this.Name = "MainForm";
            this.Text = "Trang chủ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.MainForm_Load);
            this.grUserInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grUserInfo;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Label lblUsername;
    }
}