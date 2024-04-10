
using System;
using System.Collections.Generic;

namespace Library.BL
{
    public class Order
    {
        private Book Book;
        private int Quantity;
        private int Total;
        private string Date;
        public Order(Book book, int quantity)
        {
            Book = book;
            Quantity = quantity;
            Date = DateTime.Now.ToString("dd-MM-yyyy");
            CalculateTotal();
        }
        public Order(Book book, int quantity, string date)
        {
            Book = book;
            Quantity = quantity;
            Date = date;
            CalculateTotal();
        }
        public Order(Book book, int quantity, int total, string date)
        {
            Book = book;
            Quantity = quantity;
            Total = total;
            Date = date;
        }
        public void CalculateTotal()
        {
            Total = Book.GetPrice() * Quantity;
        }
        // Getters and Setters
        public int GetTotal()
        {
            return Total;
        }
        public void SetTotal(int total)
        {
            Total = total;
        }
        public Book GetBook()
        {
            return Book;
        }
        public void SetBook(Book book)
        {
            Book = book;
        }
        public int GetQuantity()
        {
            return Quantity;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
        public string GetDate()
        {
            return Date;
        }
        public void SetDate(string date)
        {
            Date = date;
        }
    }
}