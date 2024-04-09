using Library.BL;
using Library.AbstractDLs;
using System.Data;
using System.Data.SqlClient;
using Library.Utils;

namespace Library.DL
{
    public class CustomerDB : CustomerDL, ICustomerDL
    {
        private DBConfig dB = DBConfig.GetInstance();
        private static CustomerDB Instance;
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
            string query = "Select * from Customers";
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
            }
        }
        protected override void StoreInSource(Customer customer)
        {
            string query = "Insert into Customers (Name, Email, Phone, Address) Output Inserted.ID values (@Name, @Email, @Phone, @Address)";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@Name", customer.GetName());
            command.Parameters.AddWithValue("@Email", customer.GetEmail());
            command.Parameters.AddWithValue("@Phone", customer.GetPhone());
            command.Parameters.AddWithValue("@Address", customer.GetAddress());
            int customerId = (int)command.ExecuteScalar();
            customer.SetID(customerId);
        }
        protected override void RemoveFromSource(Customer customer)
        {
            string query = "Delete from Customers where ID = @ID";
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            command.Parameters.AddWithValue("@ID", customer.GetID());
            command.ExecuteNonQuery();
        }
        protected override void UpdateInSource(Customer customer)
        {
            string query = "Update Customers set Name = @Name, Email = @Email, Phone = @Phone, Address = @Address where ID = @ID";
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