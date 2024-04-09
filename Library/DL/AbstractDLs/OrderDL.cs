using System.Collections.Generic;
using Library.BL;

namespace Library.AbstractDLs
{
    public abstract class OrderDL
    {
        protected List<Order> Orders = new List<Order>();
        public virtual void AddOrder(Order order)
        {
            Orders.Add(order);
            StoreInSource(order);
        }
        public Order FindOrder(int id)
        {
            foreach (Order o in Orders)
            {
                if (o.GetID() == id)
                {
                    return o;
                }
            }
            return null;
        }
        public void RemoveOrder(Order order)
        {
            Orders.Remove(order);
            RemoveFromSource(order);
        }
        public List<Order> GetOrders()
        {
            return Orders;
        }
        protected abstract void StoreInSource(Order order);
        protected abstract void RemoveFromSource(Order order);
    }
}