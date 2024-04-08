using System.Collections.Generic;
using Library.BL;

namespace Library.AbstractDLs
{
    public abstract class BookDL
    {
        protected List<Book> Books = new List<Book>();
        public void AddBook(Book book)
        {
            if (!BookExists(book))
            {
                Books.Add(book);
                StoreInSource(book);
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
                UpdateInSource(book);
            }
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
            RemoveFromSource(book);
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
        protected abstract void StoreInSource(Book book);
        protected abstract void UpdateInSource(Book book);
        protected abstract void RemoveFromSource(Book book);
    }
}