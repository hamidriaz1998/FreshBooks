using Library.BL;
using Library.AbstractDLs;
using Library.Utils;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Library.DL
{
    public class OrderDB : OrderDL, IOrderDL
    {
        // Change this line to use file handling
        private static IBookDL bookDL = BookDB.GetInstance();
        private static DBConfig dB = DBConfig.GetInstance();
        private static OrderDB Instance;
        public static OrderDB GetInstance()
        {
            if (Instance == null)
            {
                Instance = new OrderDB();
            }
            return Instance;
        }
        public override void AddOrder(Order order)
        {
            if (order.GetID() == 0)
            {
                order.SetID(Orders.Count + 1);
            }
            base.AddOrder(order);
        }
        public void LoadOrders()
        {
            string query = "Select * from Orders";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                int id = int.Parse(row["ID"].ToString());
                int bookID = int.Parse(row["BookID"].ToString());
                int quantity = int.Parse(row["Quantity"].ToString());
                string date = row["Date"].ToString();
                int customerID = int.Parse(row["CustomerID"].ToString());
                Book book = bookDL.FindBook(bookID);
                Order order = new Order(id, book, quantity, customerID);
                order.SetDate(date);
                Orders.Add(order);
            }
        }
        protected override void StoreInSource(Order order)
        {
            string query = "Insert into Orders (BookID, Quantity, Date, CustomerID) values (@BookID, @Quantity, @Date, @CustomerID)";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@BookID", order.GetBook().GetID());
            command.Parameters.AddWithValue("@Quantity", order.GetQuantity());
            command.Parameters.AddWithValue("@Date", DateTime.Parse(order.GetDate()));
            command.Parameters.AddWithValue("@CustomerID", order.GetCustomerId());
            command.ExecuteNonQuery();
        }
        protected override void RemoveFromSource(Order order)
        {
            string query = "Delete from Orders where ID = @ID";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@ID", order.GetID());
            command.ExecuteNonQuery();
        }
    }
}