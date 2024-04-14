using Library.BL;
using Library.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopForms.Forms.Salesmen
{
    public partial class DashBoard : Form
    {
        private Salesman LoggedInUser = (Salesman)ObjectHandler.GetLoggedInUser();
        private ICustomerDL CustomerDL = ObjectHandler.GetCustomerDL();
        public DashBoard()
        {
            InitializeComponent();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            UserNameLabel.Text = "Welcome, " + LoggedInUser.GetUsername();
            EarningsLabel.Text = LoggedInUser.GetCurrency() + LoggedInUser.GetEarnings().ToString();
            CustomersLabel.Text = CustomerDL.GetCustomers().Count.ToString();
        }
    }
}
