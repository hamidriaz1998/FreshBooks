
using System;
using System.Collections.Generic;

namespace Library.BL
{
    public class Order
    {
        private int ID;
        private List<Book> Books = new List<Book>();
        private int Quantity;
        private string Date;
        public Order(int id, List<Book> books, int quantity)
        {
            ID = id;
            Books = books;
            Quantity = quantity;
            Date = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public Order(List<Book> books, int quantity)
        {
            Books = books;
            Quantity = quantity;
            Date = DateTime.Now.ToString("dd-MM-yyyy");
        }
        // Getters and Setters
        public int GetID()
        {
            return ID;
        }
        public void SetID(int id)
        {
            ID = id;
        }
        public List<Book> GetBooks()
        {
            return Books;
        }
        public void SetBooks(List<Book> books)
        {
            Books = books;
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