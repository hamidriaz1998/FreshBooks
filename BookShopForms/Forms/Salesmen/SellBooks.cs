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
    public partial class SellBooks : Form
    {
        private ICustomerDL CustomerDL = ObjectHandler.GetCustomerDL();
        private IBookDL BookDL = ObjectHandler.GetBookDL();
        private DataTable CustomerDt = new DataTable();
        private DataTable BookDt = new DataTable();
        private int BookSelectedRow = -1;
        private int CustomerSelectedRow = -1;
        public SellBooks()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            List<Customer> customers = CustomerDL.GetCustomers();
            foreach (Customer c in customers)
            {
                AddDataRow(c);
            }
            List<Book> books = BookDL.GetBooks();
            foreach (Book b in books)
            {
                AddDataRow(b);
            }
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
        private void AddDataRow(Book b)
        {
            DataRow dr = BookDt.NewRow();
            dr["Id"] = b.GetID();
            dr["Title"] = b.GetTitle();
            dr["Author"] = b.GetAuthor();
            dr["ISBN"] = b.GetISBN();
            dr["PublicationYear"] = b.GetPublicationYear();
            dr["Price"] = b.GetPrice();
            dr["Stock"] = b.GetStock();
            BookDt.Rows.Add(dr);
        }

        private void SellBooks_Load(object sender, EventArgs e)
        {
            // Configure Customer Grid
            CustomerDt.Columns.Add("Id", typeof(int));
            CustomerDt.Columns.Add("Name", typeof(string));
            CustomerDt.Columns.Add("Email", typeof(string));
            CustomerDt.Columns.Add("Phone", typeof(string));
            CustomerDt.Columns.Add("Address", typeof(string));
            CustomerGrid.ReadOnly = true;
            CustomerGrid.DataSource = CustomerDt;
           // Configure Books Grid
            BookDt.Columns.Add("Id", typeof(int));
            BookDt.Columns.Add("Title", typeof(string));
            BookDt.Columns.Add("Author", typeof(string));
            BookDt.Columns.Add("ISBN", typeof(string));
            BookDt.Columns.Add("PublicationYear", typeof(int));
            BookDt.Columns.Add("Price", typeof(string));
            BookDt.Columns.Add("Stock", typeof(int));
            BookGrid.ReadOnly = true;
            BookGrid.DataSource = BookDt;
            // LoadData
           LoadData();
        }

        private void BookGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BookSelectedRow = BookGrid.CurrentCell.RowIndex;
            if (BookSelectedRow < 0 || BookSelectedRow >= BookDt.Rows.Count)
            {
                BookSelectedRow = -1;
            }
        }

        private void CustomerGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerSelectedRow = CustomerGrid.CurrentCell.RowIndex;
            if (CustomerSelectedRow < 0 || CustomerSelectedRow >= CustomerDt.Rows.Count)
            {
                CustomerSelectedRow = -1;
            }
        }

        private void SellButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QuantityBox.Text))
            {
                MessageBox.Show("Please enter a quantity");
                return;
            }
            if (!int.TryParse(QuantityBox.Text, out _))
            {
                MessageBox.Show("Please enter a valid number");
                return;
            }
            if (int.Parse(QuantityBox.Text) < 0)
            {
                MessageBox.Show("Quantity cannot be negative");
                return;
            }
            if (BookSelectedRow == -1)
            {
                MessageBox.Show("Please select a book");
                return;
            }
            if (CustomerSelectedRow == -1)
            {
                MessageBox.Show("Please select a customer");
                return;
            }
            int quantity = int.Parse(QuantityBox.Text);
            int bookID = int.Parse(BookDt.Rows[BookSelectedRow]["Id"].ToString());
            int customerID = int.Parse(CustomerDt.Rows[CustomerSelectedRow]["Id"].ToString());
            Customer customer = CustomerDL.FindCustomer(customerID);
            Book book = BookDL.FindBook(bookID);
            if (book.GetStock() < quantity)
            {
                MessageBox.Show("Not enough stock");
                return;
            }
            book.Sell(quantity);
            BookDL.UpdateBook(book);
            Order order = new Order(book,quantity);
            CustomerDL.AddOrder(customer, order);

            BookDt.Rows[BookSelectedRow]["Stock"] = book.GetStock();
            MessageBox.Show("Book sold successfully");
        }
    }
}
