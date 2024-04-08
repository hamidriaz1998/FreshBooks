using BookShopForms.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopForms
{
    internal class ObjectHandler
    {
        private static IUserDL UserDL = UserDB.GetInstance();
        private static IBookDL BookDL = BookFH.GetInstance();
        // private static IBookDL BookDL = BookDB.GetInstance();
        public static IUserDL GetUserDL()
        {
            return UserDL;
        }
        public static IBookDL GetBookDL()
        {
            return BookDL;
        }
    }
}
