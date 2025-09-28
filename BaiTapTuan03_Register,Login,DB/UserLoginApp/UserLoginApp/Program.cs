using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserLoginApp.Helpers;

namespace UserLoginApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string connStr = ConfigurationManager.ConnectionStrings["CaroAppDB"].ConnectionString;
            var db = new DatabaseHelper(connStr);

            Application.Run(new Forms.LoginForm(db));
        }
    }
}
