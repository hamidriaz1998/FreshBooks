using Library.BL;
using Library.DL;
namespace ConsoleUI.UI
{
    class UserUI
    {
        private static IUserDL userDL = ObjectHandler.GetUserDL();
        public static void ManageUsers()
        {
            while (true)
            {
                Utility.PrintBanner();
                Console.WriteLine("Manage Users");
                Console.WriteLine("Choose one of the following: ");
                Console.WriteLine("1. Add user");
                Console.WriteLine("2. Remove user");
                Console.WriteLine("3. Go back");
                Console.Write("Enter 1, 2 or 3: ");
                int choice = Utility.GetInt(1, 3);
                if (choice == 1)
                {
                    AddUser();
                }
                else if (choice == 2)
                {
                    RemoveUser();
                }
                else
                {
                    break;
                }
            }
        }
        public static void AddUser()
        {
            Utility.PrintBanner();
            string? username = Utility.GetValidUsername();
            if (userDL.UserExists(username))
            {
                Console.WriteLine("Username already exists");
                Utility.TryAgain();
                return;
            }
            string? password = Utility.GetValidPass();
            Console.Write("Enter Earnings: ");
            int earnings = Utility.GetInt();
            Console.Write("Enter Salary: ");
            int salary = Utility.GetInt();
            Console.Write("Enter Sales: ");
            int sales = Utility.GetInt();
            userDL.AddUser(new Salesman(username, password, earnings, salary, sales));
            Console.WriteLine("User added successfully");
            Utility.PressAnyKey();
        }
        public static void RemoveUser()
        {
            Utility.PrintBanner();
            Console.Write("Enter username: ");
            string? username = Console.ReadLine();
            if (!userDL.UserExists(username))
            {
                Console.WriteLine("User does not exist");
                Utility.TryAgain();
                return;
            }
            userDL.RemoveUser(username);
            Console.WriteLine("User removed successfully");
            Utility.PressAnyKey();
        }
    }
}