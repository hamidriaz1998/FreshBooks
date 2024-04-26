using Library.BL;
using Library.AbstractDLs;
using System.IO;
using System.Linq;

namespace Library.DL
{
    public class CustomerFH : CustomerDL, ICustomerDL
    {
        private static CustomerFH Instance;
        private IBookDL bookDL = BookFH.GetInstance();
        private string CustomerPath = AppSettings.GetCustomersPath();
        private string OrderPath = AppSettings.GetOrdersPath();
        public static CustomerFH GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CustomerFH();
            }
            return Instance;
        }
        private CustomerFH() { }
        public override void AddCustomer(Customer customer)
        {
            if (customer.GetID() == 0)
            {
                if (Customers.Count == 0)
                {
                    customer.SetID(1);
                }
                else
                {
                    customer.SetID(Customers.Last().GetID() + 1);
                }
            }
            base.AddCustomer(customer);
        }
        protected override void StoreInSource(Customer customer)
        {
            StreamWriter sw = new StreamWriter(CustomerPath, true);
            string line = customer.GetID() + ";" + customer.GetName() + ";" + customer.GetEmail() + ";" + customer.GetPhone() + ";" + customer.GetAddress();
            sw.WriteLine(line);
            sw.Flush();
            sw.Close();
            StoreOrders(customer);
        }
        protected override void StoreOrder(Order order, int customerID)
        {
            StreamWriter sw = new StreamWriter(OrderPath, true);
            string line = customerID + ";" + order.GetBook().GetID() + ";" + order.GetQuantity() + ";" + order.GetTotal() + ";" + order.GetDate();
            sw.WriteLine(line);
            sw.Flush();
            sw.Close();
        }
        protected override void StoreOrders(Customer customer)
        {
            foreach (Order order in customer.GetOrders())
            {
                StoreOrder(order, customer.GetID());
            }
        }
        public override void LoadCustomers()
        {
            if (!File.Exists(CustomerPath) || !File.Exists(OrderPath))
            {
                return;
            }
            string[] lines = File.ReadAllLines(CustomerPath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                int id = int.Parse(parts[0]);
                string name = parts[1];
                string email = parts[2];
                string phone = parts[3];
                string address = parts[4];
                Customer customer = new Customer(id, name, email, phone, address);
                Customers.Add(customer);
                LoadOrders(customer);
            }
        }
        protected override void LoadOrders(Customer customer)
        {
            string[] lines = File.ReadAllLines(OrderPath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (int.Parse(parts[0]) == customer.GetID())
                {
                    int bookID = int.Parse(parts[1]);
                    int quantity = int.Parse(parts[2]);
                    int total = int.Parse(parts[3]);
                    string date = parts[4];
                    Book book = bookDL.FindBook(bookID);
                    Order order = new Order(book, quantity, total, date);
                    customer.AddOrder(order);
                }
            }
        }
        protected override void RemoveFromSource(Customer customer)
        {
            if (!File.Exists(CustomerPath) || !File.Exists(OrderPath))
            {
                return;
            }
            string[] lines = File.ReadAllLines(CustomerPath);
            StreamWriter sw = new StreamWriter(CustomerPath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (int.Parse(parts[0]) != customer.GetID())
                {
                    sw.WriteLine(line);
                }
            }
            sw.Flush();
            sw.Close();
            DeleteOrders(customer);
        }
        protected override void DeleteOrders(Customer customer)
        {
            string[] lines = File.ReadAllLines(OrderPath);
            StreamWriter sw = new StreamWriter(OrderPath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (int.Parse(parts[0]) != customer.GetID())
                {
                    sw.WriteLine(line);
                }
            }
            sw.Flush();
            sw.Close();
        }
        protected override void UpdateInSource(Customer customer)
        {
            RemoveFromSource(customer);
            StoreInSource(customer);
        }
    }
}