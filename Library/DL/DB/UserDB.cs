using System.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using Library.BL;

namespace Library.DL
{
    public class UserDB : IUserDL
    {
        private DBConfig Db = DBConfig.GetInstance();
        private static UserDB Instance;
        private List<User> Users = new List<User>();
        private UserDB() { }
        public static UserDB GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserDB();
            }
            return Instance;
        }
        public bool UserExists(string username)
        {
            foreach (User u in Users)
            {
                if (u.GetUsername() == username)
                {
                    return true;
                }
            }
            return false;
        }
        public bool AdminExists()
        {
            foreach (User u in Users)
            {
                if (u.GetType() == "admin")
                {
                    return true;
                }
            }
            return false;
        }
        public bool AddUser(User u)
        {
            Users.Add(u);
            if (StoreInDb(u))
            {
                return true;
            }
            Users.Remove(u);
            return false;
        }
        public bool AddUser(Salesman s)
        {
            Users.Add(s);
            if (StoreInDb(s))
            {
                return true;
            }
            Users.Remove(s);
            return false;
        }
        public User Login(string username, string password)
        {
            foreach (User u in Users)
            {
                if (u.GetUsername() == username && u.GetPassword() == password)
                {
                    return u;
                }
            }
            return null;
        }
        public User GetUser(string username)
        {
            foreach (User u in Users)
            {
                if (u.GetUsername() == username)
                {
                    return u;
                }
            }
            return null;
        }
        public User GetUser(int id)
        {
            foreach (User u in Users)
            {
                if (u.GetID() == id)
                {
                    return u;
                }
            }
            return null;
        }
        public float GetTotalEarnings()
        {
            float total = 0;
            foreach (Salesman s in GetSalesmen())
            {
                total += s.GetEarnings();
            }
            return total;
        }
        public List<User> GetUsers()
        {
            return Users;
        }
        public List<Salesman> GetSalesmen()
        {
            List<Salesman> salesmen = new List<Salesman>();
            foreach (User u in Users)
            {
                if (u.GetType() == "salesman")
                {
                    salesmen.Add((Salesman)u);
                }
            }
            return salesmen;
        }
        public void RemoveUser(User u)
        {
            Users.Remove(u);
            string query = "DELETE FROM Users WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Id", u.GetID());
            Db.ExecuteCommand(cmd);
        }
        public void RemoveUser(Salesman s)
        {
            Users.Remove(s);
            string query = "DELETE FROM Salesman WHERE UserId = @Id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Id", s.GetID());
            Db.ExecuteCommand(cmd);
            RemoveUser((User)s);
        }
        public void UpdateUser(User u)
        {
            string query = "UPDATE Users SET Username = @Username, Password = @Password, Currency = @Currency WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Username", u.GetUsername());
            cmd.Parameters.AddWithValue("@Password", u.GetPassword());
            cmd.Parameters.AddWithValue("@Currency", u.GetCurrency());
            cmd.Parameters.AddWithValue("@Id", u.GetID());
            Db.ExecuteCommand(cmd);
        }
        public void UpdateUser(Salesman s)
        {
            string query = "UPDATE Salesman SET Earnings = @Earnings, Salary = @Salary, Sales = @Sales WHERE UserId = @Id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Earnings", s.GetEarnings());
            cmd.Parameters.AddWithValue("@Salary", s.GetSalary());
            cmd.Parameters.AddWithValue("@Sales", s.GetSales());
            cmd.Parameters.AddWithValue("@Id", s.GetID());
            UpdateUser((User)s);
            Db.ExecuteCommand(cmd);
        }
        public bool StoreInDb(User u)
        {
            try
            {
                string query = "INSERT INTO Users (Username, Password, Currency,Type) OUTPUT INSERTED.Id VALUES (@Username, @Password, @Currency, @Type)";
                SqlCommand cmd = new SqlCommand(query, Db.GetConnection());
                cmd.Parameters.AddWithValue("@Username", u.GetUsername());
                cmd.Parameters.AddWithValue("@Password", u.GetPassword());
                cmd.Parameters.AddWithValue("@Currency", u.GetCurrency());
                cmd.Parameters.AddWithValue("@Type", u.GetType());
                int userId = (int)cmd.ExecuteScalar();
                u.SetID(userId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool StoreInDb(Salesman s)
        {
            try
            {
                StoreInDb((User)s);
                string query = "Insert into Salesman(Earnings,Salary,Sales,UserId) VALUES(@Earnings,@Salary,@Sales,@UserId)";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Earnings", s.GetEarnings());
                cmd.Parameters.AddWithValue("@Salary", s.GetSalary());
                cmd.Parameters.AddWithValue("@Sales", s.GetSales());
                cmd.Parameters.AddWithValue("@UserId", s.GetID());
                return Db.ExecuteCommand(cmd);
            }
            catch (Exception)
            {
                RemoveUser((User)s);
                return false;
            }
        }
        public void LoadUsers()
        {
            LoadAdminFromDb();
            LoadSalesmenFromDb();
        }
        private void LoadAdminFromDb()
        {
            string query = "SELECT * FROM Users where Type = 'admin'";
            SqlCommand cmd = new SqlCommand(query, Db.GetConnection());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string username = reader.GetString(1);
                string password = reader.GetString(2);
                Admin a = new Admin(username, password);
                a.SetID(reader.GetInt32(0));
                a.SetCurrency(reader.GetString(3));
                Users.Add(a);
            }
            reader.Close();
        }
        private void LoadSalesmenFromDb()
        {
            string query = "SELECT u.Id,u.Username,u.Password,u.Currency,s.Earnings,s.Salary,s.Sales FROM Users u Join Salesman s on u.Id = s.UserId where Type = 'salesman'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Db.GetConnection());
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                string username = row["Username"].ToString();
                string password = row["Password"].ToString();
                Salesman s = new Salesman(username, password);
                s.SetID(Convert.ToInt32(row["Id"]));
                s.SetCurrency(row["Currency"].ToString());
                s.SetEarnings(Convert.ToSingle(row["Earnings"]));
                s.SetSalary(Convert.ToSingle(row["Salary"]));
                s.SetSales(Convert.ToInt32(row["Sales"]));
                Users.Add(s);
            }
        }
    }
}