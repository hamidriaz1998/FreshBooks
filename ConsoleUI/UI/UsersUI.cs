using Library.AbstractDLs;
using Library.BL;
using Library.DL;
namespace ConsoleUI.UI
{
    class UserUI
    {
        private static IUserDL userDL = ObjectHandler.GetUserDL();
        public static void ManageUsers()
        {
            Utility.PrintBanner();
            Console.WriteLine("Manage Users");
            Console.WriteLine("Choose one of the following: ");
            Console.WriteLine("1. Add user");
            Console.WriteLine("2. Remove user");
            Console.WriteLine("3. Update user");
            Console.WriteLine("4. List All users");
            Console.WriteLine("5. Go back");
            Console.Write("Enter 1, 2, 3, 4 or 5: ");
        }
        public static Salesman GetUserToAdd()
        {
            while (true)
            {
                Utility.PrintBanner();
                string? username = Utility.GetValidUsername();
                if (userDL.UserExists(username))
                {
                    Console.WriteLine("Username already exists");
                    Utility.TryAgain();
                    continue;
                }
                string? password = Utility.GetValidPass();
                Console.Write("Enter Earnings: ");
                int earnings = Utility.GetInt();
                Console.Write("Enter Salary: ");
                int salary = Utility.GetInt();
                Console.Write("Enter Sales: ");
                int sales = Utility.GetInt();
                return new Salesman(username, password, earnings, salary, sales);
            }
        }
        public static Salesman GetUserToRemove()
        {
            while (true)
            {
                ListUsers();
                Console.Write("Enter ID of user to update: ");
                int id = Utility.GetInt();
                Salesman s = (Salesman)userDL.GetUser(id);
                if (s == null)
                {
                    Console.WriteLine("User does not exist");
                    Utility.TryAgain();
                    continue;
                }
                return s;
            }
        }
        public static void ListUsers()
        {
            Utility.PrintBanner();
            Console.WriteLine("List of Salesmen.");
            Console.WriteLine("ID".PadRight(20) + "Username".PadRight(20) + "Earnings".PadRight(20) + "Salary".PadRight(20) + "Sales".PadRight(20));
            foreach (Salesman s in userDL.GetSalesmen())
            {
                Console.WriteLine(s.GetID().ToString().PadRight(20) + s.GetUsername().PadRight(20) + s.GetEarnings().ToString().PadRight(20) + s.GetSalary().ToString().PadRight(20) + s.GetSales().ToString().PadRight(20));
            }
        }
        public static Salesman GetUpdatedUser()
        {
            while (true)
            {
                ListUsers();
                Console.Write("Enter ID of user to update: ");
                int id = Utility.GetInt();
                Salesman s = (Salesman)userDL.GetUser(id);
                if (s == null)
                {
                    Console.WriteLine("User does not exist");
                    Utility.TryAgain();
                    continue;
                }
                Console.WriteLine("Enter new values for the user.");
                string? username = Utility.GetValidUsername();
                if (userDL.UserExists(username))
                {
                    Console.WriteLine("Username already exists");
                    Utility.TryAgain();
                    continue;
                }
                string? password = Utility.GetValidPass();
                Console.Write("Enter Earnings: ");
                int earnings = Utility.GetInt();
                Console.Write("Enter Salary: ");
                int salary = Utility.GetInt();
                Console.Write("Enter Sales: ");
                int sales = Utility.GetInt();
                s.SetUsername(username);
                s.SetPassword(password);
                s.SetEarnings(earnings);
                s.SetSalary(salary);
                s.SetSales(sales);
                return s;
            }
        }
    }
}