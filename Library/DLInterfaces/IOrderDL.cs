using System.Collections.Generic;
using Library.BL;

namespace Library.DL
{
    public interface IOrderDL
    {
        void AddOrder(Order order);
        Order FindOrder(int id);
        void RemoveOrder(Order order);
        List<Order> GetOrders();
        List<Order> GetOrdersByCustomer(int customerId);
        List<Order> GetOrdersByBook(int bookId);
        void LoadOrders();
        bool OrderExists(int id);
    }
}