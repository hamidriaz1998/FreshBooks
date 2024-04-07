using BookShopForms.BL;

namespace BookShopForms.DL
{
    public interface IUserDL
    {
        bool UserExists(string username);
        bool AdminExists();
        bool AddUser(User u);
        User Login(string username, string password);
    }
}
