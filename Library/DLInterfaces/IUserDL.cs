using System.Collections.Generic;
using Library.BL;

namespace Library.DL
{
    public interface IUserDL
    {
        bool UserExists(string username);
        bool AdminExists();
        bool AddUser(User u);
        bool AddUser(Salesman s);
        User GetUser(string username);
        User GetUser(int id);
        float GetTotalEarnings();
        List<User> GetUsers();
        List<Salesman> GetSalesmen();
        void RemoveUser(User u);
        void RemoveUser(Salesman s);
        void RemoveUser(string username);
        void UpdateUser(User u);
        void UpdateUser(Salesman s);
        void LoadUsers();
        User Login(string username, string password);
    }
}
