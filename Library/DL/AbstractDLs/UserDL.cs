using Library.BL;
using System.Collections.Generic;
using Library.Utils;
using System.Runtime;

namespace Library.AbstractDLs
{
    public abstract class UserDL
    {
        protected List<User> Users = new List<User>();
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
            if (StoreInSource(u))
            {
                return true;
            }
            Users.Remove(u);
            return false;
        }
        public bool AddUser(Salesman s)
        {
            Users.Add(s);
            if (StoreInSource(s))
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
            RemoveUserFromSource(u);
        }
        public void RemoveUser(Salesman s)
        {
            Users.Remove(s);
            RemoveUserFromSource(s);
        }
        protected abstract void RemoveUserFromSource(User u);
        protected abstract void RemoveUserFromSource(Salesman s);
        public abstract void UpdateUser(User u);
        public abstract void UpdateUser(Salesman s);
        protected abstract bool StoreInSource(User u);
        protected abstract bool StoreInSource(Salesman s);
    }
}