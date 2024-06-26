using Library.BL;
using System.Collections.Generic;
namespace Library.DL
{
    public interface ICustomerDL
    {
        void AddCustomer(Customer customer);
        Customer FindCustomer(int id);
        Customer FindCustomer(string email);
        void RemoveCustomer(Customer customer);
        void RemoveCustomer(int id);
        void UpdateCustomer(Customer customer);
        List<Customer> GetCustomers();
        void LoadCustomers();
        bool CustomerExists(string email);
        bool CustomerExists(Customer customer);
        void AddOrder(Customer customer, Order order);
    }
}