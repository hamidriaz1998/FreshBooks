using Library.BL;
using System.Collections.Generic;
interface ICustomerDL
{
    void AddCustomer(Customer customer);
    Customer FindCustomer(int id);
    Customer FindCustomer(string email);
    void RemoveCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    List<Customer> GetCustomers();
    void LoadCustomers();
    bool CustomerExists(int id);
    bool CustomerExists(Customer customer);
}