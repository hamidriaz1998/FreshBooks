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
using Library.BL;

namespace BookShopForms.Forms.Salesmen
{
    public partial class OrderHistory : Form
    {
        private ICustomerDL CustomerDL = ObjectHandler.GetCustomerDL();
        private DataTable CustomerDt = new DataTable();
        private DataTable OrderDt = new DataTable();
        private int OrderSelectedRow = -1;
        private int CustomerSelectedRow = -1;
        public OrderHistory()
        {
            InitializeComponent();
        }
        private void AddDataRow(Customer c)
        {
            DataRow dr = CustomerDt.NewRow();
            dr["Id"] = c.GetID();
            dr["Name"] = c.GetName();
            dr["Email"] = c.GetEmail();
            dr["Phone"] = c.GetPhone();
            dr["Address"] = c.GetAddress();
            CustomerDt.Rows.Add(dr);
        }
        private void AddDataRow(Order o)
        {
            Book b = o.GetBook();
            DataRow dr = OrderDt.NewRow();
            dr["Book name"] = b.GetTitle();
            dr["Author"] = b.GetAuthor();
            dr["ISBN"] = b.GetISBN();
            dr["Price"] = b.GetPrice();
            dr["Quantity"] = o.GetQuantity();
            dr["Total"] = o.GetTotal();
            dr["Date"] = DateTime.Parse(o.GetDate());
        }
        private void LoadData()
        {
            List<Customer> customers = CustomerDL.GetCustomers();
            foreach (Customer c in customers)
            {
                AddDataRow(c);
            }
        }

        private void OrderHistory_Load(object sender, EventArgs e)
        {   // Configure Customer Grid
            CustomerDt.Columns.Add("Id", typeof(int));
            CustomerDt.Columns.Add("Name", typeof(string));
            CustomerDt.Columns.Add("Email", typeof(string));
            CustomerDt.Columns.Add("Phone", typeof(string));
            CustomerDt.Columns.Add("Address", typeof(string));
            CustomerGrid.ReadOnly = true;
            CustomerGrid.DataSource = CustomerDt;
            // Configure Order Grid
            OrderDt.Columns.Add("Book name", typeof(string));
            OrderDt.Columns.Add("Author", typeof(string));
            OrderDt.Columns.Add("ISBN", typeof(string));
            OrderDt.Columns.Add("Price", typeof(int));
            OrderDt.Columns.Add("Quantity", typeof(int));
            OrderDt.Columns.Add("Total", typeof(int));
            OrderDt.Columns.Add("Date", typeof(DateTime));
            OrdersGrid.ReadOnly = true;
            OrdersGrid.DataSource = OrderDt;
            LoadData();
        }
    }
}
