using BookShopForms.DL;
using BookShopForms.BL;
using System.Collections.Generic;
using System.IO;
namespace BookShopForms.DL
{
    class UserFH : IUserDL
    {
        private static UserFH Instance;
        private List<User> Users = new List<User>();
        private string UserFile = "../../users.txt";
        private string SalesmanFile = "../../salesmen.txt";
        private UserFH() { }
        public static UserFH GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserFH();
            }
            return Instance;
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
            if (u.GetID() == 0)
            {
                u.SetID(Users.Count + 1);
            }
            Users.Add(u);
            if (StoreInFile(u))
            {
                return true;
            }
            Users.Remove(u);
            return false;
        }
        public bool AddUser(Salesman s)
        {
            if (s.GetID() == 0)
            {
                s.SetID(Users.Count + 1);
            }
            Users.Add(s);
            if (StoreInFile(s))
            {
                return true;
            }
            Users.Remove(s);
            return false;
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
            RemoveFromFile(u);
        }
        public void RemoveUser(Salesman s)
        {
            Users.Remove(s);
            RemoveFromFile(s);
        }
        public void UpdateUser(User u)
        {
            RemoveFromFile(u);
            StoreInFile(u);
        }
        public void UpdateUser(Salesman s)
        {
            RemoveFromFile(s);
            StoreInFile(s);
        }
        public void LoadUsers()
        {
            // Load users from file
        }
        private void LoadUsersFromFile()
        {
            // Load users from file
        }
        private void LoadSalesmenFromFile()
        {
            // Load salesmen from file
        }
        private bool StoreInFile(User u)
        {
            StreamWriter sw = new StreamWriter(UserFile, true);
            sw.WriteLine(u.GetID() + ";" + u.GetUsername() + ";" + u.GetPassword() + ";" + u.GetCurrency() + ";" + u.GetType());
            sw.Close();
            return true;
        }
        private bool StoreInFile(Salesman s)
        {
            StoreInFile((User)s);
            StreamWriter sw = new StreamWriter(SalesmanFile, true);
            sw.WriteLine(s.GetID() + ";" + s.GetEarnings() + ";" + s.GetSalary() + ";" + s.GetSales());
            sw.Close();
            return true;
        }
        private void RemoveFromFile(User u)
        {
            // Remove user from file
        }
        private void RemoveFromFile(Salesman s)
        {
            // Remove salesman from file
        }
    }
}