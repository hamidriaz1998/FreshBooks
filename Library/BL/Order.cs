
using System;
using System.Collections.Generic;

namespace Library.BL
{
    public class Order
    {
        private int ID;
        private Book Book;
        private int Quantity;
        private string Date;
        private int CustomerId;
        public Order(int id, Book book, int quantity, int customerId)
        {
            ID = id;
            Book = book;
            Quantity = quantity;
            // Date in format of sql server
            Date = DateTime.Now.ToString("dd-MM-yyyy");
            CustomerId = customerId;
        }
        public Order(Book book, int quantity, int customerId)
        {
            Book = book;
            Quantity = quantity;
            Date = DateTime.Now.ToString("dd-MM-yyyy");
            CustomerId = customerId;
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
        public int GetCustomerId()
        {
            return CustomerId;
        }
        public void SetCustomerId(int customerId)
        {
            CustomerId = customerId;
        }
    }
}