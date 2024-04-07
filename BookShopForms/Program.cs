using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShopForms.Forms;
using BookShopForms.Forms.Admin;
using BookShopForms.DL;

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
            IBookDL BookDL = BookFH.GetInstance();

            UserDL.LoadFromDb();
            BookDL.Load();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Landing());
        }
    }
}
