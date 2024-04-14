using Library.DL;
using Library.BL;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Library.AbstractDLs;
namespace Library.DL
{
    public class UserFH : UserDL, IUserDL
    {
        private static UserFH Instance;
        private string UserFile = AppSettings.GetUsersPath();
        private string SalesmanFile = AppSettings.GetSalesmanPath();
        private UserFH() { }
        public static UserFH GetInstance()
        {
            if (Instance == null)
            {
                Instance = new UserFH();
            }
            return Instance;
        }
        public override bool AddUser(User u)
        {
            if (u.GetID() == 0)
            {
                u.SetID(Users.Count + 1);
            }
            return base.AddUser(u);
        }
        public override bool AddUser(Salesman s)
        {
            if (s.GetID() == 0)
            {
                s.SetID(Users.Count + 1);
            }
            return base.AddUser(s);
        }

        public override void UpdateUser(User u)
        {
            RemoveUserFromSource(u);
            StoreInSource(u);
        }
        public override void UpdateUser(Salesman s)
        {
            RemoveUserFromSource(s);
            StoreInSource(s);
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
        protected override bool StoreInSource(User u)
        {
            StreamWriter sw = new StreamWriter(UserFile, true);
            sw.WriteLine(u.GetID() + ";" + u.GetUsername() + ";" + u.GetPassword() + ";" + u.GetCurrency() + ";" + u.GetType());
            sw.Close();
            return true;
        }
        protected override bool StoreInSource(Salesman s)
        {
            StoreInSource((User)s);
            StreamWriter sw = new StreamWriter(SalesmanFile, true);
            sw.WriteLine(s.GetID() + ";" + s.GetEarnings() + ";" + s.GetSalary() + ";" + s.GetSales());
            sw.Close();
            return true;
        }
        protected override void RemoveUserFromSource(User u)
        {
            string[] lines = File.ReadAllLines(UserFile);
            lines = lines.Where(line => !line.StartsWith(u.GetID().ToString())).ToArray();
            File.WriteAllLines(UserFile, lines);
        }
        protected override void RemoveUserFromSource(Salesman s)
        {
            RemoveUserFromSource((User)s);
            string[] lines = File.ReadAllLines(SalesmanFile);
            lines = lines.Where(line => !line.StartsWith(s.GetID().ToString())).ToArray();
            File.WriteAllLines(SalesmanFile, lines);
        }
    }
}