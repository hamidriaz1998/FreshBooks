using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library;
using Library.DL;
using Library.Utils;

namespace ConsoleUI
{
    public class ObjectHandler
    {
        private static Validations validations = Validations.GetInstance();
        private static IUserDL userDL = AppSettings.GetUserDL();
        private static IBookDL bookDL = AppSettings.GetBookDL();
        private static ICustomerDL customerDL = AppSettings.GetCustomerDL();
        public static IUserDL GetUserDL()
        {
            return userDL;
        }
        public static IBookDL GetBookDL()
        {
            return bookDL;
        }
        public static ICustomerDL GetCustomerDL()
        {
            return customerDL;
        }
        public static Validations GetValidations()
        {
            return validations;
        }
    }
}