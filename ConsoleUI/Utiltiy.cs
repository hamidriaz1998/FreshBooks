using Library.Utils;

namespace ConsoleUI
{
    public class Utility
    {
        private static Validations validations = ObjectHandler.GetValidations();
        public static void PrintBanner()
        {
            Console.Clear();
            Console.WriteLine(@" _____              _       ____              _        ");
            Console.WriteLine(@"|  ___| __ ___  ___| |__   | __ )  ___   ___ | | _____ ");
            Console.WriteLine(@"| |_ | '__/ _ \/ __| '_ \  |  _ \ / _ \ / _ \| |/ / __|");
            Console.WriteLine(@"|  _|| | |  __/\__ \ | | | | |_) | (_) | (_) |   <\__ \");
            Console.WriteLine(@"|_|  |_|  \___||___/_| |_| |____/ \___/ \___/|_|\_\___/");
            Console.WriteLine();
        }
        public static void SourceMenu()
        {
            PrintBanner();
            Console.WriteLine("Choose one of the following: ");
            Console.WriteLine("1. Use DataBase");
            Console.WriteLine("2. Use File Handling");
            Console.WriteLine("3. Exit");
            Console.Write("Enter 1, 2 or 3: ");
        }
        // Validations
        public static string? GetValidUsername()
        {
            while (true)
            {
                Console.Write("Enter username: ");
                string? username = Console.ReadLine();
                if (!validations.IsUsernameValid(username))
                {
                    Console.WriteLine("Invalid username");
                    Console.WriteLine("Username must be between 5 and 20 characters long, can only contain letters and numbers.");
                    TryAgain();
                    continue;
                }
                return username;
            }
        }
        public static string? GetValidPass()
        {
            while (true)
            {
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();
                if (!validations.IsPasswordValid(password))
                {
                    Console.WriteLine("Invalid password");
                    Console.WriteLine("Password must be at least 6 characters and cannot contain ',' or ';'");
                    TryAgain();
                    continue;
                }
                return password;
            }
        }
        public static int GetInt()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int result))
                {
                    Console.WriteLine("Invalid input");
                    TryAgain();
                    continue;
                }
                return result;
            }
        }
        public static int GetInt(int min, int max)
        {
            while (true)
            {
                int input = GetInt();
                if (input < min || input > max)
                {
                    Console.WriteLine($"Input must be between {min} and {max}");
                    TryAgain();
                    continue;
                }
                return input;
            }
        }
        public static void TryAgain()
        {
            Console.WriteLine("Press any key to try again");
            Console.ReadKey();
        }
        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}