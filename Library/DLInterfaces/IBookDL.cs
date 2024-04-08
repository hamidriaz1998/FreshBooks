using System.Collections.Generic;
using BookShopForms.BL;

namespace BookShopForms.DL
{
    public interface IBookDL
    {
        List<Book> GetBooks();
        void AddBook(Book book);
        void RemoveBook(Book book);
        void UpdateBook(Book book);
        Book FindBook(string isbn);
        Book FindBook(int id);
        void LoadBooks();
        bool BookExists(Book book);
        bool BookExists(string isbn);
    }
}