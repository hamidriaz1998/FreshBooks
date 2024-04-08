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
using Library.DL;

namespace BookShopForms.Forms.AdminForms
{
    public partial class BookForm : Form
    {
        private DataTable dt = new DataTable();
        private IBookDL BookDL = ObjectHandler.GetBookDL();
        int SelectedRow = 0;
        public BookForm()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            List<Book> books = BookDL.GetBooks();
            foreach (Book book in books)
            {
                AddDataRow(book);
            }
        }
        private void AddDataRow(Book book)
        {
            DataRow row = dt.NewRow();
            row["Id"] = book.GetID();
            row["Title"] = book.GetTitle();
            row["Author"] = book.GetAuthor();
            row["ISBN"] = book.GetISBN();
            row["PublicationYear"] = book.GetPublicationYear();
            row["Price"] = book.GetPrice();
            row["Stock"] = book.GetStock();
            row["MinStock"] = book.GetMinStock();
            row["CopiesSold"] = book.GetCopiesSold();
            dt.Rows.Add(row);
        }
        private void BookForm_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Id",typeof(int));
            dt.Columns.Add("Title",typeof(string));
            dt.Columns.Add("Author",typeof(string));
            dt.Columns.Add("ISBN", typeof(string));
            dt.Columns.Add("PublicationYear", typeof(int));
            dt.Columns.Add("Price", typeof(int));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("MinStock",typeof(int));
            dt.Columns.Add("CopiesSold",typeof(int));
            LoadData();
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
        }
        private bool CheckEmpty()
        {
            if (string.IsNullOrEmpty(TitleBox.Text) || string.IsNullOrEmpty(AuthorBox.Text) || string.IsNullOrEmpty(IsbnBox.Text) || string.IsNullOrEmpty(YearBox.Text) || string.IsNullOrEmpty(priceBox.Text) || string.IsNullOrEmpty(stockBox.Text) || string.IsNullOrEmpty(minStockBox.Text))
            {
                return true;
            }
            return false;
        }
        private bool CheckInt()
        {
            if (int.TryParse(YearBox.Text, out _) && int.TryParse(priceBox.Text, out _) && int.TryParse(stockBox.Text, out _) && int.TryParse(minStockBox.Text, out _))
            {
                return true;
            }
            return false;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (CheckEmpty())
            {
                MessageBox.Show("Please fill all fields");
                return;
            }
            if (!CheckInt())
            {
                MessageBox.Show("Please enter valid numbers");
                return;
            }
            if (BookDL.BookExists(IsbnBox.Text))
            {
                MessageBox.Show("Book already exists");
                return;
            }
            Book book = new Book(TitleBox.Text, AuthorBox.Text, IsbnBox.Text, int.Parse(YearBox.Text), int.Parse(priceBox.Text), int.Parse(stockBox.Text), int.Parse(minStockBox.Text));
            if (BookDL.BookExists(book))
            {
                MessageBox.Show("Book already exists");
                return;
            }   
            BookDL.AddBook(book);
            AddDataRow(book);
            ClearFields();
                dataGridView1.ClearSelection();
        }
        private void ClearFields()
        {
            TitleBox.Text = "";
            AuthorBox.Text = "";
            IsbnBox.Text = "";
            YearBox.Text = "";
            priceBox.Text = "";
            stockBox.Text = "";
            minStockBox.Text = "";
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (CheckEmpty())
            {
                MessageBox.Show("Please fill all fields");
                return;
            }
            if (!CheckInt())
            {
                MessageBox.Show("Please enter valid numbers");
                return;
            }
            SelectedRow = dataGridView1.CurrentCell.RowIndex;
            if (SelectedRow >= 0 && SelectedRow <= dt.Rows.Count)
            {
                int id = int.Parse(dt.Rows[SelectedRow]["Id"].ToString());
                Book book = BookDL.FindBook(id);
                book.SetTitle(TitleBox.Text);
                book.SetAuthor(AuthorBox.Text);
                book.SetISBN(IsbnBox.Text);
                book.SetPublicationYear(int.Parse(YearBox.Text));
                book.SetPrice(int.Parse(priceBox.Text));
                book.SetStock(int.Parse(stockBox.Text));
                book.SetMinStock(int.Parse(minStockBox.Text));
                BookDL.UpdateBook(book);
                dt.Rows[SelectedRow]["Title"] = TitleBox.Text;
                dt.Rows[SelectedRow]["Author"] = AuthorBox.Text;
                dt.Rows[SelectedRow]["ISBN"] = IsbnBox.Text;
                dt.Rows[SelectedRow]["PublicationYear"] = int.Parse(YearBox.Text);
                dt.Rows[SelectedRow]["Price"] = int.Parse(priceBox.Text);
                dt.Rows[SelectedRow]["Stock"] = int.Parse(stockBox.Text);
                dt.Rows[SelectedRow]["MinStock"] = int.Parse(minStockBox.Text);
                ClearFields();
                dataGridView1.ClearSelection();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = e.RowIndex;
            if (SelectedRow < 0 || SelectedRow >= dt.Rows.Count)
            {
                return;
            }
            TitleBox.Text = dt.Rows[SelectedRow]["Title"].ToString();
            AuthorBox.Text = dt.Rows[SelectedRow]["Author"].ToString();
            IsbnBox.Text = dt.Rows[SelectedRow]["ISBN"].ToString();
            YearBox.Text = dt.Rows[SelectedRow]["PublicationYear"].ToString();
            priceBox.Text = dt.Rows[SelectedRow]["Price"].ToString();
            stockBox.Text = dt.Rows[SelectedRow]["Stock"].ToString();
            minStockBox.Text = dt.Rows[SelectedRow]["MinStock"].ToString();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SelectedRow = dataGridView1.CurrentCell.RowIndex;
            if (SelectedRow > 0 && SelectedRow <= dt.Rows.Count)
            {
                int id = int.Parse(dt.Rows[SelectedRow]["Id"].ToString());
                Book book = BookDL.FindBook(id);
                BookDL.RemoveBook(book);
                dt.Rows.RemoveAt(SelectedRow);
                ClearFields();
                dataGridView1.ClearSelection();
            }
        }
    }
}
