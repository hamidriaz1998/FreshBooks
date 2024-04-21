
using System.Text.RegularExpressions;

namespace Library.Utils
{
    public class Validations
    {
        private static Validations Instance;
        public static Validations GetInstance()
        {
            if (Instance == null)
                Instance = new Validations();
            return Instance;
        }
        public bool ValidInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            // Does not contain ',' or ';'
            Regex regex = new Regex(@"^[^,;]*$");
            return true;
        }
        public bool IsPasswordValid(string password)
        {
            // Password must be at least 6 characters long, can have special chars except ',' and ';', no spaces.
            // Password must contain both characters and numbers.
            if (string.IsNullOrEmpty(password))
                return false;
            Regex regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)[^\s,;]{6,}$");
            return regex.IsMatch(password);
        }
        public bool IsUsernameValid(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            // Username must be between 5 and 20 characters long, can only contain letters and numbers.
            Regex regex = new Regex(@"^[a-zA-Z0-9]{5,20}$");
            return regex.IsMatch(username);
        }
        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }
        public bool IsPhoneValid(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }
            Regex regex = new Regex(@"^03[0-4][0-9]{8}$");
            return regex.IsMatch(phone);
        }
    }
}