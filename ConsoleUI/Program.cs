using ConsoleUI.UI;
using Library;
using Library.BL;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            AppSettings.UseDB();
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
                        // AdminUI.MainMenu((Admin)u);
                    }
                    else
                    {
                        Console.WriteLine("Signed in as salesman");
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