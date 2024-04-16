using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Library.BL;
using Library.DL;

namespace BookShopForms.Forms.AdminForms
{
    public partial class LowStockBooks : Form
    {
        private User LoggedInUser = ObjectHandler.GetLoggedInUser();
        private IBookDL BookDL = ObjectHandler.GetBookDL();
        private DataTable dt = new DataTable();
        int SelectedRow = 0;
        public LowStockBooks()
        {
            InitializeComponent();
        }
        private void SetTabIndices()
        {
            dataGridView1.TabIndex = 0;
            StockBox.TabIndex = 1;
            AddButton.TabIndex = 2;
        }
        private void AddDataRow(Book book)
        {
            DataRow dr = dt.NewRow();
            dr["Id"] = book.GetID();
            dr["Title"] = book.GetTitle();
            dr["Author"] = book.GetAuthor();
            dr["ISBN"] = book.GetISBN();
            dr["PublicationYear"] = book.GetPublicationYear();
            dr["Price"] = LoggedInUser.GetCurrency() + book.GetPrice().ToString();
            dr["Stock"] = book.GetStock();
            dr["MinStock"] = book.GetMinStock();
            dr["CopiesSold"] = book.GetCopiesSold();
            dt.Rows.Add(dr);
        }
        private void LoadData()
        {
           List<Book> books = BookDL.GetLowStockBooks();
            foreach (Book book in books)
            {
                AddDataRow(book);
            }
        }

        private void LowStockBooks_Load(object sender, EventArgs e)
        {
            SetTabIndices();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Author", typeof(string));
            dt.Columns.Add("ISBN", typeof(string));
            dt.Columns.Add("PublicationYear", typeof(int));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("MinStock", typeof(int));
            dt.Columns.Add("CopiesSold", typeof(int));
            LoadData();
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book");
                return;
            }
             SelectedRow = dataGridView1.CurrentCell.RowIndex;
            Book book = BookDL.FindBook(Convert.ToInt32(dt.Rows[SelectedRow]["Id"]));
            book.SetStock(book.GetStock() + Convert.ToInt32(StockBox.Text));
            BookDL.UpdateBook(book);
            dt.Rows.RemoveAt(SelectedRow);
            MessageBox.Show("Stock updated successfully");  
        }

        private void StockBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox box = (Guna2TextBox)sender;
            if (box.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Please enter a stock amount");
            }
            else if (!int.TryParse(box.Text, out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Please enter a valid number");
            }
            else if (Convert.ToInt32(box.Text) < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Stock amount cannot be negative");
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
