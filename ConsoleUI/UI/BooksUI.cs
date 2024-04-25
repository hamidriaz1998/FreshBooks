using Library.BL;
using Library.DL;

namespace ConsoleUI.UI
{
    class BooksUI
    {
        private static IBookDL bookDL = ObjectHandler.GetBookDL();
        public static void ManageBooks()
        {
            Utility.PrintBanner();
            Console.WriteLine("Manage Books");
            Console.WriteLine("Choose one of the following: ");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Update book");
            Console.WriteLine("3. Low Stock Books");
            Console.WriteLine("4. Remove book");
            Console.WriteLine("5. List All Books");
            Console.WriteLine("6. Go back");
            Console.Write("Enter 1, 2, 3, 4, 5 or 6: ");
        }
        public static Book GetBookData()
        {
            while (true)
            {
                Utility.PrintBanner();
                Console.Write("Enter Title: ");
                string title = Console.ReadLine();
                if (bookDL.BookExists(title))
                {
                    Console.WriteLine("Book already exists");
                    Utility.TryAgain();
                    continue;
                }
                Console.Write("Enter Author: ");
                string author = Console.ReadLine();
                Console.Write("Enter ISBN: ");
                string isbn = Console.ReadLine();
                Console.Write("Enter Publication Year: ");
                int year = Utility.GetInt();
                Console.Write("Enter Price: ");
                int price = Utility.GetInt();
                Console.Write("Enter Stock: ");
                int stock = Utility.GetInt();
                Console.Write("Enter MinStock: ");
                int MinStock = Utility.GetInt();
                return new Book(title, author, isbn, year, price, stock, MinStock);
            }
        }
        public static void ListBooks(List<Book> books)
        {
            Utility.PrintBanner();
            if (books.Count == 0)
            {
                Console.WriteLine("No books found");
                return;
            }
            Console.WriteLine("ID".PadRight(5) + "Title".PadRight(20) + "Author".PadRight(20) + "ISBN".PadRight(20) + "Year".PadRight(10) + "Price".PadRight(10) + "Stock".PadRight(10) + "MinStock".PadRight(10));
            foreach (Book b in books)
            {
                Console.WriteLine(b.GetID().ToString().PadRight(5) + b.GetTitle().PadRight(20) + b.GetAuthor().PadRight(20) + b.GetISBN().PadRight(20) + b.GetPublicationYear().ToString().PadRight(10) + b.GetPrice().ToString().PadRight(10) + b.GetStock().ToString().PadRight(10) + b.GetMinStock().ToString().PadRight(10));
            }
        }
        public static Book GetUpdatedBook()
        {
            while (true)
            {
                Book b = GetBook();
                Console.WriteLine("Enter new values for the book:");
                Console.Write("Enter Title: ");
                string title = Console.ReadLine();
                Console.Write("Enter Author: ");
                string author = Console.ReadLine();
                Console.Write("Enter ISBN: ");
                string isbn = Console.ReadLine();
                Console.Write("Enter Publication Year: ");
                int year = Utility.GetInt();
                Console.Write("Enter Price: ");
                int price = Utility.GetInt();
                Console.Write("Enter Stock: ");
                int stock = Utility.GetInt();
                Console.Write("Enter MinStock: ");
                int MinStock = Utility.GetInt();
                b.SetTitle(title);
                b.SetAuthor(author);
                b.SetISBN(isbn);
                b.SetPublicationYear(year);
                b.SetPrice(price);
                b.SetStock(stock);
                b.SetMinStock(MinStock);
                return b;
            }
        }
        public static void ListLowStockBooks()
        {
            List<Book> books = bookDL.GetLowStockBooks();
            ListBooks(books);
        }
        public static Book GetBook()
        {
            while (true)
            {
                List<Book> books = bookDL.GetBooks();
                ListBooks(books);
                Console.Write("Enter ID of book: ");
                int id = Utility.GetInt();
                Book b = bookDL.FindBook(id);
                if (b == null)
                {
                    Console.WriteLine("Book does not exist");
                    Utility.TryAgain();
                    continue;
                }
                return b;
            }
        }
    }
}