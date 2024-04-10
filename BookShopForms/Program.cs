using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShopForms.Forms;
using BookShopForms.Forms.AdminForms;

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
            ObjectHandler.GetUserDL().LoadUsers();
            ObjectHandler.GetBookDL().LoadBooks();
            ObjectHandler.GetCustomerDL().LoadCustomers();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Landing());
        }
    }
}
