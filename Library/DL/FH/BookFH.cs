using BookShopForms.BL;
using System.Collections.Generic;
using System.IO;

namespace BookShopForms.DL
{
    public class BookFH : IBookDL
    {
        // Implement this DL with file handling instead of a database
        private static BookFH Instance;
        private static List<Book> Books = new List<Book>();
        private string Path = "../../DataFiles/Books.txt";
        private BookFH() { }
        public static BookFH GetInstance()
        {
            if (Instance == null)
                Instance = new BookFH();
            return Instance;
        }
        public void AddBook(Book book)
        {
            if (BookExists(book))
            {
                return;
            }
            if (book.GetID() == 0)
            {
                book.SetID(Books.Count + 1);
            }
            Books.Add(book);
            StoreInFile(book);
        }
        public Book FindBook(int id)
        {
            foreach (Book b in Books)
            {
                if (b.GetID() == id)
                {
                    return b;
                }
            }
            return null;
        }
        public Book FindBook(string isbn)
        {
            foreach (Book b in Books)
            {
                if (b.GetISBN() == isbn)
                {
                    return b;
                }
            }
            return null;
        }
        public void UpdateBook(Book book)
        {
            if (BookExists(book))
            {
                UpdateInFile(book);
            }
        }
        public List<Book> GetBooks()
        {
            return Books;
        }
        public bool BookExists(Book book)
        {
            foreach (Book b in Books)
            {
                if (b.GetID() == book.GetID())
                {
                    return true;
                }
            }
            return false;
        }
        public bool BookExists(string isbn)
        {
            foreach (Book b in Books)
            {
                if (b.GetISBN() == isbn)
                {
                    return true;
                }
            }
            return false;
        }
        public void RemoveBook(Book book)
        {
            Books.Remove(book);
            RemoveFromFile(book);
        }
        public void LoadBooks()
        {
            StreamReader sr = new StreamReader(Path);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                Book book = new Book(int.Parse(data[0]), data[1], data[2], data[3], int.Parse(data[4]), int.Parse(data[5]), int.Parse(data[6]), int.Parse(data[7]));
                book.SetCopiesSold(int.Parse(data[8]));
                Books.Add(book);
            }
            sr.Close();
        }
        private void StoreInFile(Book book)
        {
            StreamWriter sw = new StreamWriter(Path, true);
            sw.WriteLine(book.GetID() + "," + book.GetTitle() + "," + book.GetAuthor() + "," + book.GetISBN() + "," + book.GetPublicationYear() + "," + book.GetPrice() + "," + book.GetStock() + "," + book.GetMinStock() + "," + book.GetCopiesSold());
            sw.Flush();
            sw.Close();
        }
        private void UpdateInFile(Book book)
        {
            string[] lines = File.ReadAllLines(Path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                if (data[0] == book.GetID().ToString())
                {
                    lines[i] = book.GetID() + "," + book.GetTitle() + "," + book.GetAuthor() + "," + book.GetISBN() + "," + book.GetPublicationYear() + "," + book.GetPrice() + "," + book.GetStock() + "," + book.GetMinStock() + "," + book.GetCopiesSold();
                    break;
                }
            }
            File.WriteAllLines(Path, lines);
        }
        private void RemoveFromFile(Book book)
        {
            // Remove from file
            string[] lines = File.ReadAllLines(Path);
            string[] newLines = new string[lines.Length - 1];
            int j = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');
                if (data[0] != book.GetID().ToString())
                {
                    newLines[j] = lines[i];
                    j++;
                }
            }
            File.WriteAllLines(Path, newLines);
        }


    }
}