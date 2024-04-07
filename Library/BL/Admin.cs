
namespace BookShopForms.BL
{
    public class Admin : User
    {
        public Admin(string username, string password) : base(username, password)
        {
        }
        public override string GetType()
        {
            return "admin";
        }
    }
}