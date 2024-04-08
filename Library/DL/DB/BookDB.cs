using System.Data.SqlClient;
using System.Collections.Generic;
using Library.Utils;
using Library.BL;
using Library.AbstractDLs;

namespace Library.DL
{
    public class BookDB : BookDL, IBookDL
    {
        private static DBConfig Db = DBConfig.GetInstance();
        private static BookDB Instance;
        public static BookDB GetInstance()
        {
            if (Instance == null)
                Instance = new BookDB();
            return Instance;
        }
        public override void LoadBooks()
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

        protected override void StoreInSource(Book book)
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

        protected override void UpdateInSource(Book book)
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

        protected override void RemoveFromSource(Book book)
        {
            string query = "DELETE FROM Book WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, Db.GetConnection());
            cmd.Parameters.AddWithValue("@Id", book.GetID());
            cmd.ExecuteNonQuery();
        }
    }
}