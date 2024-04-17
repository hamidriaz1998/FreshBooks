using Library.AbstractDLs;
using Library.BL;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Library.DL
{
    public class BookFH : BookDL, IBookDL
    {
        // Implement this DL with file handling instead of a database
        private static BookFH Instance;
        private string Path = AppSettings.GetBooksPath();
        public static BookFH GetInstance()
        {
            if (Instance == null)
                Instance = new BookFH();
            return Instance;
        }
        private BookFH() { }
        public override void AddBook(Book book)
        {
            if (book.GetID() == 0)
            {
                if (Books.Count == 0)
                    book.SetID(1);
                else
                    book.SetID(Books.Last().GetID() + 1);
            }
            base.AddBook(book);
        }
        public override void LoadBooks()
        {
            if (!File.Exists(Path))
                return;
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
        protected override void StoreInSource(Book book)
        {
            StreamWriter sw = new StreamWriter(Path, true);
            sw.WriteLine(book.GetID() + "," + book.GetTitle() + "," + book.GetAuthor() + "," + book.GetISBN() + "," + book.GetPublicationYear() + "," + book.GetPrice() + "," + book.GetStock() + "," + book.GetMinStock() + "," + book.GetCopiesSold());
            sw.Flush();
            sw.Close();
        }
        protected override void UpdateInSource(Book book)
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
        protected override void RemoveFromSource(Book book)
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