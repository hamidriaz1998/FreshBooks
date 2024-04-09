using Library.BL;
using System.Collections.Generic;

namespace Library.AbstractDLs
{
    public abstract class CustomerDL
    {
        protected List<Customer> Customers = new List<Customer>();
        public virtual void AddCustomer(Customer customer)
        {
            if (CustomerExists(customer))
                return;
            Customers.Add(customer);
            StoreInSource(customer);
        }
        public bool CustomerExists(int id)
        {
            foreach (Customer c in Customers)
            {
                if (c.GetID() == id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CustomerExists(Customer customer)
        {
            return Customers.Contains(customer);
        }
        public Customer FindCustomer(int id)
        {
            foreach (Customer c in Customers)
            {
                if (c.GetID() == id)
                {
                    return c;
                }
            }
            return null;
        }
        public Customer FindCustomer(string email)
        {
            foreach (Customer c in Customers)
            {
                if (c.GetEmail() == email)
                {
                    return c;
                }
            }
            return null;
        }
        public void UpdateCustomer(Customer customer)
        {
            if (CustomerExists(customer))
            {
                UpdateInSource(customer);
            }
        }
        public void RemoveCustomer(Customer customer)
        {
            Customers.Remove(customer);
            RemoveFromSource(customer);
        }
        public List<Customer> GetCustomers()
        {
            return Customers;
        }
        protected abstract void StoreInSource(Customer customer);
        protected abstract void RemoveFromSource(Customer customer);
        protected abstract void UpdateInSource(Customer customer);
        public abstract void LoadCustomers();
        protected abstract void DeleteOrders(Customer customer);
        protected abstract void LoadOrders(Customer customer);
        protected abstract void StoreOrder(Order order, int customerID);
        protected abstract void StoreOrders(Customer customer);
    }
}