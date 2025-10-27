using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server.Models;

namespace Server
{
    public partial class FormServer : Form
    {
        private ServerCore server;
        public FormServer()
        {
            InitializeComponent();
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormServer_Load(object sender, EventArgs e)
        {

        }

        private  void btnStart_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                int port = 9000;
                string connStr = "Data Source=.;Initial Catalog=UserDB;Integrated Security=True"; // hoặc chuỗi kết nối thật

                server = new ServerCore(port, connStr);

                server.OnLog += LogMessage;
                server.OnClientConnected += ip => LogMessage($"Client connected: {ip}");
                server.OnClientDisconnected += ip => LogMessage($"Client disconnected: {ip}");

                server.Start();

                LogMessage($"✅ Server started on port {port}");
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server?.Stop();
            LogMessage("🛑 Server stopped.");
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void LogMessage(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(LogMessage), msg);
            }
            else
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
            }
        }
    }
}
