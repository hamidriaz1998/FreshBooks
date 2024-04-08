using System.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using Library.BL;
using Library.Utils;
using Library.AbstractDLs;

namespace Library.DL
{
    public class UserDB : UserDL, IUserDL
    {
        private DBConfig Db = DBConfig.GetInstance();
        private static UserDB Instance;
        public static UserDB GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserDB();
            }
            return Instance;
        }

        protected override void RemoveUserFromSource(User u)
        {
            string query = "DELETE FROM Users WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Id", u.GetID());
            Db.ExecuteCommand(cmd);
        }
        protected override void RemoveUserFromSource(Salesman s)
        {
            string query = "DELETE FROM Salesman WHERE UserId = @Id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Id", s.GetID());
            Db.ExecuteCommand(cmd);
            RemoveUserFromSource((User)s);
        }
        public override void UpdateUser(User u)
        {
            string query = "UPDATE Users SET Username = @Username, Password = @Password, Currency = @Currency WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Username", u.GetUsername());
            cmd.Parameters.AddWithValue("@Password", u.GetPassword());
            cmd.Parameters.AddWithValue("@Currency", u.GetCurrency());
            cmd.Parameters.AddWithValue("@Id", u.GetID());
            Db.ExecuteCommand(cmd);
        }
        public override void UpdateUser(Salesman s)
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
        protected override bool StoreInSource(User u)
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
        protected override bool StoreInSource(Salesman s)
        {
            try
            {
                StoreInSource((User)s);
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