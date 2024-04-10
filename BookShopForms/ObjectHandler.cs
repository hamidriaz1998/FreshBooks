using Library.DL;
using Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopForms
{
    internal class ObjectHandler
    {
        private static Validations validations = Validations.GetInstance();

        // Uncomment the following lines to use the database handler

        //private static IUserDL UserDL = UserDB.GetInstance();
        //private static IBookDL BookDL = BookDB.GetInstance();
        //private static ICustomerDL CustomerDL = CustomerDB.GetInstance();

        // Uncomment the following lines to use the file handler

        private static IBookDL BookDL = BookFH.GetInstance();
        private static IUserDL UserDL = UserFH.GetInstance();
        private static ICustomerDL CustomerDL = CustomerFH.GetInstance();

        public static IUserDL GetUserDL()
        {
            return UserDL;
        }
        public static IBookDL GetBookDL()
        {
            return BookDL;
        }
        public static ICustomerDL GetCustomerDL()
        {
            return CustomerDL;
        }
        public static Validations GetValidations()
        {
            return validations;
        }
    }
}
