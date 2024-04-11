using Library.BL;
using Library.AbstractDLs;
using System.Data;
using System.Data.SqlClient;
using Library.Utils;
using System;

namespace Library.DL
{
    public class CustomerDB : CustomerDL, ICustomerDL
    {
        private IBookDL bookDL = BookDB.GetInstance();
        private DBConfig dB = DBConfig.GetInstance();
        private static CustomerDB Instance;
        private CustomerDB() { }
        public static CustomerDB GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CustomerDB();
            }
            return Instance;
        }
        public override void LoadCustomers()
        {
            string query = "Select * from Customer";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                int id = int.Parse(row["ID"].ToString());
                string name = row["Name"].ToString();
                string email = row["Email"].ToString();
                string phone = row["Phone"].ToString();
                string address = row["Address"].ToString();
                Customer customer = new Customer(id, name, email, phone, address);
                Customers.Add(customer);
                LoadOrders(customer);
            }
        }
        protected override void StoreInSource(Customer customer)
        {
            string query = "Insert into Customer (Name, Email, Phone, Address) Output Inserted.ID values (@Name, @Email, @Phone, @Address)";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@Name", customer.GetName());
            command.Parameters.AddWithValue("@Email", customer.GetEmail());
            command.Parameters.AddWithValue("@Phone", customer.GetPhone());
            command.Parameters.AddWithValue("@Address", customer.GetAddress());
            int customerId = (int)command.ExecuteScalar();
            customer.SetID(customerId);
            StoreOrders(customer);
        }
        protected override void LoadOrders(Customer customer)
        {
            string query = "Select * from Orders where CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@CustomerID", customer.GetID());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                int bookID = int.Parse(row["BookID"].ToString());
                int quantity = int.Parse(row["Quantity"].ToString());
                string date = row["Date"].ToString();
                Book book = bookDL.FindBook(bookID);
                Order order = new Order(book, quantity, date);
                customer.AddOrder(order);
            }
        }
        protected override void StoreOrders(Customer customer)
        {
            foreach (Order order in customer.GetOrders())
            {
                StoreOrder(order, customer.GetID());
            }
        }
        protected override void StoreOrder(Order order, int customerID)
        {
            string query = "Insert into Orders (CustomerID, BookID, Quantity,Total, Date) values (@CustomerID, @BookID,@Total, @Quantity, @Date)";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@CustomerID", customerID);
            command.Parameters.AddWithValue("@BookID", order.GetBook().GetID());
            command.Parameters.AddWithValue("@Quantity", order.GetQuantity());
            command.Parameters.AddWithValue("@Total", order.GetTotal());
            command.Parameters.AddWithValue("@Date", DateTime.Parse(order.GetDate()));
            command.ExecuteNonQuery();
        }
        protected override void DeleteOrders(Customer customer)
        {
            string query = "Delete from Orders where CustomerID = @CustomerID";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@CustomerID", customer.GetID());
            command.ExecuteNonQuery();
        }
        protected override void RemoveFromSource(Customer customer)
        {
            DeleteOrders(customer);
            string query = "Delete from Customer where ID = @ID";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@ID", customer.GetID());
            command.ExecuteNonQuery();
        }
        protected override void UpdateInSource(Customer customer)
        {
            string query = "Update Customer set Name = @Name, Email = @Email, Phone = @Phone, Address = @Address where ID = @ID";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@Name", customer.GetName());
            command.Parameters.AddWithValue("@Email", customer.GetEmail());
            command.Parameters.AddWithValue("@Phone", customer.GetPhone());
            command.Parameters.AddWithValue("@Address", customer.GetAddress());
            command.Parameters.AddWithValue("@ID", customer.GetID());
            command.ExecuteNonQuery();
        }

    }
}