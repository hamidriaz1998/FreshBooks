using Library;
using Library.BL;
using Library.DL;
using Library.Utils;

namespace ConsoleUI.UI
{
    class LoginUI
    {
        private static IUserDL userDL = ObjectHandler.GetUserDL();
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
        public static void StartingPage()
        {
            PrintBanner();
            Console.WriteLine("Welcome");
            Console.WriteLine("Choose one of the following: ");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign up");
            Console.WriteLine("3. Exit");
            Console.Write("Enter 1, 2 or 3: ");
        }
        public static User Login()
        {
            while (true)
            {
                PrintBanner();
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                User u = userDL.Login(username, password);
                if (u == null)
                {
                    Console.WriteLine("Invalid username or password");
                    Utility.TryAgain();
                    continue;
                }
                return u;
            }
        }
        public static void SignUp()
        {
            while (true)
            {
                PrintBanner();
                string username = Utility.GetValidUsername();
                if (userDL.UserExists(username))
                {
                    Console.WriteLine("Username already exists");
                    Utility.TryAgain();
                    continue;
                }
                string password = Utility.GetValidPass();
                Console.WriteLine("Choose Role: 1) Admin 2) Salesman");
                Console.Write("Enter 1 or 2: ");
                int role = Utility.GetInt(1, 2);
                if (role == 1)
                {
                    if (userDL.AdminExists())
                    {
                        Console.WriteLine("System can only have 1 admin.");
                        Utility.TryAgain();
                        continue;
                    }
                    userDL.AddUser(new Admin(username, password));
                }
                else
                {
                    userDL.AddUser(new Salesman(username, password));
                }
            }
        }
    }
}