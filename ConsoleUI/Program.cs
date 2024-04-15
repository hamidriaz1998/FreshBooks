using ConsoleUI.UI;
using Library;
using Library.BL;
using Library.DL;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            AppSettings.UseDB(); // Use this line to switch to DB
            // AppSettings.UseFH(); // Use this line to switch to File Handling

            IUserDL userDL = ObjectHandler.GetUserDL();
            IBookDL bookDL = ObjectHandler.GetBookDL();
            ICustomerDL customerDL = ObjectHandler.GetCustomerDL();

            userDL.LoadUsers();
            bookDL.LoadBooks();
            customerDL.LoadCustomers();

            while (true)
            {
                LoginUI.StartingPage();
                int choice = Utility.GetInt(1, 3);
                if (choice == 1)
                {
                    User u = LoginUI.Login();
                    if (u.GetType() == "admin")
                    {
                        Console.WriteLine("Signed in as admin");
                        Utility.PressAnyKey();
                        // AdminUI.MainMenu((Admin)u);
                    }
                    else
                    {
                        Console.WriteLine("Signed in as salesman");
                        Utility.PressAnyKey();
                        // SalesmanUI.MainMenu((Salesman)u);
                    }
                }
                else if (choice == 2)
                {
                    LoginUI.SignUp();
                }
                else
                {
                    break;
                }
            }
        }
    }
}