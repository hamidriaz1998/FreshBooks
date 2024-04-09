
using System.Collections.Generic;

namespace Library.BL
{
    public class Customer
    {
        private int ID;
        private string Name;
        private string Email;
        private string Phone;
        private string Address;
        private List<Order> Orders;
        public Customer(int id, string name, string email, string phone, string address)
        {
            ID = id;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Orders = new List<Order>();
        }
        public Customer(string name, string email, string phone, string address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Orders = new List<Order>();
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
        public string GetName()
        {
            return Name;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public string GetEmail()
        {
            return Email;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public string GetPhone()
        {
            return Phone;
        }
        public void SetPhone(string phone)
        {
            Phone = phone;
        }
        public string GetAddress()
        {
            return Address;
        }
        public void SetAddress(string address)
        {
            Address = address;
        }
        public List<Order> GetOrders()
        {
            return Orders;
        }
        public void SetOrders(List<Order> orders)
        {
            Orders = orders;
        }
        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }
        public void RemoveOrder(Order order)
        {
            Orders.Remove(order);
        }
    }
}