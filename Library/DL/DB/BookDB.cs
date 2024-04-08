using System.Data.SqlClient;
using System.Collections.Generic;
using BookShopForms;
using BookShopForms.BL;

namespace BookShopForms.DL
{
    public class BookDB : IBookDL
    {
        private static DBConfig Db = DBConfig.GetInstance();
        private static BookDB Instance;
        private List<Book> Books = new List<Book>();
        private BookDB()
        {

        }
        public static BookDB GetInstance()
        {
            if (Instance == null)
                Instance = new BookDB();
            return Instance;
        }

        public void AddBook(Book book)
        {
            if (!BookExists(book))
            {
                Books.Add(book);
                StoreInDb(book);
            }
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
                UpdateInDb(book);
            }
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
            RemoveFromDB(book);
        }

        public bool BookExists(Book book)
        {
            foreach (Book b in Books)
            {
                if (b.GetISBN() == book.GetISBN())
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

        public List<Book> GetBooks()
        {
            return Books;
        }

        public void LoadBooks()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Book", Db.GetConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                string author = reader.GetString(2);
                string isbn = reader.GetString(3);
                int publicationYear = reader.GetInt32(4);
                int price = reader.GetInt32(5);
                int stock = reader.GetInt32(6);
                int minStock = reader.GetInt32(7);
                Book book = new Book(id, title, author, isbn, publicationYear, price, stock, minStock);
                Books.Add(book);
            }
            reader.Close();
        }

        private void StoreInDb(Book book)
        {
            string query = "INSERT INTO Book (Title, Author, ISBN, PublicationYear, Price, Stock, MinStock, CopiesSold) Output inserted.Id VALUES (@Title, @Author, @ISBN, @PublicationYear, @Price, @Stock, @MinStock, @CopiesSold)";
            SqlCommand cmd = new SqlCommand(query, Db.GetConnection());
            cmd.Parameters.AddWithValue("@Title", book.GetTitle());
            cmd.Parameters.AddWithValue("@Author", book.GetAuthor());
            cmd.Parameters.AddWithValue("@ISBN", book.GetISBN());
            cmd.Parameters.AddWithValue("@PublicationYear", book.GetPublicationYear());
            cmd.Parameters.AddWithValue("@Price", book.GetPrice());
            cmd.Parameters.AddWithValue("@Stock", book.GetStock());
            cmd.Parameters.AddWithValue("@MinStock", book.GetMinStock());
            cmd.Parameters.AddWithValue("@CopiesSold", book.GetCopiesSold());
            int id = (int)cmd.ExecuteScalar();
            book.SetID(id);
        }

        public void UpdateInDb(Book book)
        {
            string query = "UPDATE Book SET Title = @Title, Author = @Author, ISBN = @ISBN, PublicationYear = @PublicationYear, Price = @Price, Stock = @Stock, MinStock = @MinStock, CopiesSold = @CopiesSold WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, Db.GetConnection());
            cmd.Parameters.AddWithValue("@Title", book.GetTitle());
            cmd.Parameters.AddWithValue("@Author", book.GetAuthor());
            cmd.Parameters.AddWithValue("@ISBN", book.GetISBN());
            cmd.Parameters.AddWithValue("@PublicationYear", book.GetPublicationYear());
            cmd.Parameters.AddWithValue("@Price", book.GetPrice());
            cmd.Parameters.AddWithValue("@Stock", book.GetStock());
            cmd.Parameters.AddWithValue("@MinStock", book.GetMinStock());
            cmd.Parameters.AddWithValue("@CopiesSold", book.GetCopiesSold());
            cmd.Parameters.AddWithValue("@Id", book.GetID());
            cmd.ExecuteNonQuery();
        }

        private void RemoveFromDB(Book book)
        {
            string query = "DELETE FROM Book WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, Db.GetConnection());
            cmd.Parameters.AddWithValue("@Id", book.GetID());
            cmd.ExecuteNonQuery();
        }
    }
}