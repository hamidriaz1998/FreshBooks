using Library.BL;
using Library.DL;

namespace ConsoleUI.UI
{
    class AdminUI
    {
        public static void MainMenu(Admin admin)
        {
            while (true)
            {
                Utility.PrintBanner();
                Console.WriteLine("Welcome " + admin.GetUsername());
                Console.WriteLine("Choose one of the following: ");
                Console.WriteLine("1. Manage users");
                Console.WriteLine("2. Manage books");
                Console.WriteLine("3. Manage customers");
                Console.WriteLine("4. Sign out");
                Console.Write("Enter 1, 2, 3 or 4: ");
                int choice = Utility.GetInt(1, 4);
                if (choice == 1)
                {
                    UserUI.ManageUsers();
                }
                else if (choice == 2)
                {
                    BooksUI.ManageBooks();
                }
                else if (choice == 3)
                {
                    // ManageCustomers();
                }
                else
                {
                    break;
                }
            }
        }
    }
}