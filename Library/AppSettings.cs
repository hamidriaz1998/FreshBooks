using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DL;

namespace Library
{
    public class AppSettings
    {
        private static string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BookShop;Trusted_Connection=True;";
        private static string DataFilesPath = "../../../DataFiles/";
        private static string BooksPath = DataFilesPath + "Books.txt";
        private static string CustomersPath = DataFilesPath + "Customers.txt";
        private static string OrdersPath = DataFilesPath + "Orders.txt";
        private static string UsersPath = DataFilesPath + "Users.txt";
        private static ICustomerDL customerDL;
        private static IBookDL bookDL;
        private static IUserDL userDL;
        public static void UseDB()
        {
            customerDL = CustomerDB.GetInstance();
            bookDL = BookDB.GetInstance();
            userDL = UserDB.GetInstance();
        }
        public static void UseFH()
        {
            customerDL = CustomerFH.GetInstance();
            bookDL = BookFH.GetInstance();
            userDL = UserFH.GetInstance();
        }
        // Getters
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
        public static string GetRootPath()
        {
            return DataFilesPath;
        }
        public static string GetBooksPath()
        {
            return BooksPath;
        }
        public static string GetCustomersPath()
        {
            return CustomersPath;
        }
        public static string GetOrdersPath()
        {
            return OrdersPath;
        }
        public static string GetUsersPath()
        {
            return UsersPath;
        }
        public static ICustomerDL GetCustomerDL()
        {
            return customerDL;
        }
        public static IBookDL GetBookDL()
        {
            return bookDL;
        }
        public static IUserDL GetUserDL()
        {
            return userDL;
        }

    }
}