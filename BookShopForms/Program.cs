using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShopForms.Forms;
using BookShopForms.Forms.AdminForms;
using BookShopForms.Forms.Common;
using Library;

namespace BookShopForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Uncomment to use Database
            AppSettings.UseDB();

            // Uncomment to use Files
            // AppSettings.UseFiles();

            ObjectHandler.GetUserDL().LoadUsers();
            ObjectHandler.GetBookDL().LoadBooks();
            ObjectHandler.GetCustomerDL().LoadCustomers();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Landing());
        }
    }
}
