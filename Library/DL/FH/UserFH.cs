using BookShopForms.DL;
using BookShopForms.BL;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace BookShopForms.DL
{
    public class UserFH : IUserDL
    {
        private static UserFH Instance;
        private List<User> Users = new List<User>();
        private string UserFile = "../../../DataFiles/users.txt";
        private string SalesmanFile = "../../../DataFiles/salesmen.txt";
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
        public float GetTotalEarnings()
        {
            float total = 0;
            foreach (Salesman s in GetSalesmen())
            {
                total += s.GetEarnings();
            }
            return total;
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
            if (!File.Exists(UserFile) || !File.Exists(SalesmanFile))
            {
                return;
            }
            LoadAdminFromFile();
            LoadSalesmenFromFile();
        }
        private void LoadAdminFromFile()
        {
            string[] lines = File.ReadAllLines(UserFile);
            foreach (string line in lines)
            {
                string[] data = line.Split(';');
                if (data[4] == "admin")
                {
                    User u = new Admin(data[1], data[2]);
                    u.SetID(int.Parse(data[0]));
                    u.SetCurrency(data[3]);
                    Users.Add(u);
                    return;
                }
            }
        }
        private void LoadSalesmenFromFile()
        {
            string[] UsersFileLines = File.ReadAllLines(UserFile);
            string[] SalesmenFileLines = File.ReadAllLines(SalesmanFile);
            Dictionary<int, string[]> salesmenData = new Dictionary<int, string[]>();
            foreach (string sline in SalesmenFileLines)
            {
                string[] sdata = sline.Split(';');
                salesmenData[int.Parse(sdata[0])] = sdata;
            }
            foreach (string line in UsersFileLines)
            {
                string[] data = line.Split(';');
                if (data[4] == "salesman")
                {
                    Salesman s = new Salesman(data[1], data[2]);
                    s.SetID(int.Parse(data[0]));
                    s.SetCurrency(data[3]);
                    if (salesmenData.TryGetValue(s.GetID(), out string[] sdata))
                    {
                        s.SetEarnings(float.Parse(sdata[1]));
                        s.SetSalary(float.Parse(sdata[2]));
                        s.SetSales(int.Parse(sdata[3]));
                    }
                    Users.Add(s);
                }
            }
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
            string[] lines = File.ReadAllLines(UserFile);
            lines = lines.Where(line => !line.StartsWith(u.GetID().ToString())).ToArray();
            File.WriteAllLines(UserFile, lines);
        }
        private void RemoveFromFile(Salesman s)
        {
            // Remove salesman from file
            RemoveFromFile((User)s);
            string[] lines = File.ReadAllLines(SalesmanFile);
            lines = lines.Where(line => !line.StartsWith(s.GetID().ToString())).ToArray();
            File.WriteAllLines(SalesmanFile, lines);
        }
    }
}