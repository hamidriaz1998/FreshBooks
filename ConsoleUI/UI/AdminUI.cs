using Library.BL;
using Library.DL;

namespace ConsoleUI.UI
{
    class AdminUI
    {
        public static void MainMenu(Admin admin)
        {
            Utility.PrintBanner();
            Console.WriteLine("Welcome " + admin.GetUsername());
            Console.WriteLine("Choose one of the following: ");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage books");
            Console.WriteLine("3. Manage customers");
            Console.WriteLine("4. Sign out");
            Console.Write("Enter 1, 2, 3 or 4: ");
        }
    }
}