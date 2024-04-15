using Library.BL;
using Library.DL;

namespace ConsoleUI.UI
{
    class BooksUI
    {
        private static IBookDL bookDL = ObjectHandler.GetBookDL();
        public static void ManageBooks()
        {
            while (true)
            {
                Utility.PrintBanner();
                Console.WriteLine("Manage Books");
                Console.WriteLine("Choose one of the following: ");
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. Update book");
                Console.WriteLine("3. Low Stock Books");
                Console.WriteLine("4. Remove book");
                Console.WriteLine("5. Go back");
                Console.Write("Enter 1, 2, 3, 4 or 5: ");
                int choice = Utility.GetInt(1, 5);
                if (choice == 1)
                {
                    // AddBook();
                }
                else if (choice == 2)
                {
                    // RemoveBook();
                }
                else if (choice == 3)
                {
                    // UpdateBook();
                }
                else if (choice == 4)
                {
                    // LowStockBooks();
                }
                else
                {
                    break;
                }
            }
        }
    }
}