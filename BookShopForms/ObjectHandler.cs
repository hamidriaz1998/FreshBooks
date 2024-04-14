using Library;
using Library.BL;
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
        private static User LoggedInUser = null;
       
        private static IBookDL BookDL = AppSettings.GetBookDL();
        private static IUserDL UserDL = AppSettings.GetUserDL();
        private static ICustomerDL CustomerDL = AppSettings.GetCustomerDL();

        public static User GetLoggedInUser()
        {
            return LoggedInUser;
        }
        public static void SetLoggedInUser(User user)
        {
            LoggedInUser = user;
        }
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
