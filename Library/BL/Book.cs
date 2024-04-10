using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Library.BL
{
    public class Book
    {
        private int ID;
        private string Title;
        private string Author;
        private string ISBN;
        private int PublicationYear;
        private int Price;
        private int Stock;
        private int MinStock;
        private int CopiesSold;

        public Book(string title, string author, string isbn, int publicationYear, int price, int stock, int minStock)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            Price = price;
            Stock = stock;
            MinStock = minStock;
            CopiesSold = 0;
        }
        public Book(int id, string title, string author, string isbn, int publicationYear, int price, int stock, int minStock)
        {
            ID = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            Price = price;
            Stock = stock;
            MinStock = minStock;
            CopiesSold = 0;
        }      // Getters Setters
        public int GetID()
        {
            return ID;
        }
        public void SetID(int id)
        {
            ID = id;
        }
        public string GetTitle()
        {
            return Title;
        }
        public void SetTitle(string title)
        {
            Title = title;
        }
        public string GetAuthor()
        {
            return Author;
        }
        public void SetAuthor(string author)
        {
            Author = author;
        }
        public string GetISBN()
        {
            return ISBN;
        }
        public void SetISBN(string isbn)
        {
            ISBN = isbn;
        }
        public int GetPublicationYear()
        {
            return PublicationYear;
        }
        public void SetPublicationYear(int publicationYear)
        {
            PublicationYear = publicationYear;
        }
        public int GetPrice()
        {
            return Price;
        }
        public void SetPrice(int price)
        {
            Price = price;
        }
        public int GetStock()
        {
            return Stock;
        }
        public void SetStock(int stock)
        {
            Stock = stock;
        }
        public int GetMinStock()
        {
            return MinStock;
        }
        public void SetMinStock(int minStock)
        {
            MinStock = minStock;
        }
        public int GetCopiesSold()
        {
            return CopiesSold;
        }
        public void SetCopiesSold(int copiesSold)
        {
            CopiesSold = copiesSold;
        }
        // Methods
        public void Sell(int quantity)
        {
            if (quantity <= Stock)
            {
                Stock -= quantity;
                CopiesSold += quantity;
            }
        }
        public void Restock(int quantity)
        {
            Stock += quantity;
        }
        public bool IsLowStock()
        {
            return Stock < MinStock;
        }
    }
}