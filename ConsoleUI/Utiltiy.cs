using Library.Utils;

namespace ConsoleUI
{
    public class Utility
    {
        private static Validations validations = ObjectHandler.GetValidations();
        // Validations
        public static string GetValidUsername()
        {
            while (true)
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                if (!validations.IsUsernameValid(username))
                {
                    Console.WriteLine("Invalid username");
                    Console.WriteLine("Username must be between 5 and 20 characters long, can only contain letters and numbers.");
                    continue;
                }
                return username;
            }
        }
        public static string GetValidPass()
        {
            while (true)
            {
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                if (!validations.IsPasswordValid(password))
                {
                    Console.WriteLine("Invalid password");
                    Console.WriteLine("Password must be at least 6 characters and cannot contain ',' or ';'");
                    continue;
                }
                return password;
            }
        }
        public static int GetInt(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int result))
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                if (result < min || result > max)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                return result;
            }
        }
    }
}