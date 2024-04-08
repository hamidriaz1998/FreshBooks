
namespace Library.BL
{
    public abstract class User
    {
        protected int ID;
        protected string Username;
        protected string Password;
        protected string Currency = "$";
        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Currency = "$";
        }
        // Getters Setters
        public int GetID()
        {
            return ID;
        }
        public void SetID(int id)
        {
            ID = id;
        }
        public string GetUsername()
        {
            return Username;
        }
        public void SetUsername(string username)
        {
            Username = username;
        }
        public string GetPassword()
        {
            return Password;
        }
        public void SetPassword(string password)
        {
            Password = password;
        }
        public string GetCurrency()
        {
            return Currency;
        }
        public void SetCurrency(string currency)
        {
            Currency = currency;
        }
        // Methods
        public bool UsernamesMatch(string username)
        {
            return Username == username;
        }
        public bool PasswordsMatch(string password)
        {
            return Password == password;
        }
        public bool Login(string username, string password)
        {
            return UsernamesMatch(username) && PasswordsMatch(password);
        }

        public new abstract string GetType();
    }
}
