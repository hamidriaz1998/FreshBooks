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
                        while (true)
                        { // Admin Dashboard
                            AdminUI.MainMenu((Admin)u);
                            choice = Utility.GetInt(1, 4);
                            if (choice == 1)
                            {
                                while (true)
                                {
                                    // Manage Users
                                    UserUI.ManageUsers();
                                    choice = Utility.GetInt(1, 3);
                                    if (choice == 1)
                                    {
                                        Salesman s = UserUI.GetUserToAdd();
                                        userDL.AddUser(s);
                                        Console.WriteLine("User Added Successfully.");
                                        Utility.PressAnyKey();
                                    }
                                    else if (choice == 2)
                                    {
                                        string username = UserUI.GetUserToRemove();
                                        userDL.RemoveUser(username);
                                        Console.WriteLine("User Removed Successfully.");
                                        Utility.PressAnyKey();
                                    }
                                    else if (choice == 3)
                                    {
                                        Salesman s = UserUI.GetUpdatedUser();
                                        userDL.UpdateUser(s);
                                        Console.WriteLine("User Updated Successfully.");
                                        Utility.PressAnyKey();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Signed in as salesman");
                        Utility.PressAnyKey();
                    }
                }
                else if (choice == 2)
                {
                    User u = LoginUI.SignUp();
                    userDL.AddUser(u);
                    Console.WriteLine("Signed up successfully.");
                    Utility.PressAnyKey();
                }
                else
                {
                    break;
                }
            }
        }
    }
}