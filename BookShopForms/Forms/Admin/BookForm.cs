﻿using System;
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
using Library.Utils;

namespace BookShopForms.Forms.AdminForms
{
    public partial class BookForm : Form
    {
        private Validations Validations = new Validations();
        private User LoggedInUser = ObjectHandler.GetLoggedInUser();
        private DataTable dt = new DataTable();
        private IBookDL BookDL = ObjectHandler.GetBookDL();
        int SelectedRow = 0;
        public BookForm()
        {
            InitializeComponent();
        }
        private void SetTabIndices()
        {
            TitleBox.TabIndex = 0;
            AuthorBox.TabIndex = 1;
            IsbnBox.TabIndex = 2;
            YearBox.TabIndex = 3;
            priceBox.TabIndex = 4;
            stockBox.TabIndex = 5;
            minStockBox.TabIndex = 6;
            AddButton.TabIndex = 7;
            updateBtn.TabIndex = 8;
            DeleteButton.TabIndex = 9; 
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
            row["Price"] = LoggedInUser.GetCurrency() + book.GetPrice().ToString();
            row["Stock"] = book.GetStock();
            row["MinStock"] = book.GetMinStock();
            row["CopiesSold"] = book.GetCopiesSold();
            dt.Rows.Add(row);
        }
        private void BookForm_Load(object sender, EventArgs e)
        {
            SetTabIndices();
            dt.Columns.Add("Id",typeof(int));
            dt.Columns.Add("Title",typeof(string));
            dt.Columns.Add("Author",typeof(string));
            dt.Columns.Add("ISBN", typeof(string));
            dt.Columns.Add("PublicationYear", typeof(int));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("MinStock",typeof(int));
            dt.Columns.Add("CopiesSold",typeof(int));
            LoadData();
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
        }
        private bool IsInputValid()
        {
            if (Validations.ValidInput(TitleBox.Text) && Validations.ValidInput(AuthorBox.Text) && Validations.ValidInput(IsbnBox.Text) && Validations.ValidInput(YearBox.Text) && Validations.ValidInput(priceBox.Text) && Validations.ValidInput(stockBox.Text) && Validations.ValidInput(minStockBox.Text))
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
        private bool CheckNegative()
        {
            if (int.Parse(YearBox.Text) < 0 || int.Parse(priceBox.Text) < 0 || int.Parse(stockBox.Text) < 0 || int.Parse(minStockBox.Text) < 0)
            {
                return true;
            }
            return false;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!IsInputValid())
            {
                MessageBox.Show("Empty fields are not allowed. ',' or ';' are also not allowed");
                return;
            }
            if (!CheckInt())
            {
                MessageBox.Show("Please enter valid numbers");
                return;
            }
            if (CheckNegative())
            {
                MessageBox.Show("Please enter positive numbers");
                return;
            }
            if (BookDL.BookExists(IsbnBox.Text))
            {
                MessageBox.Show("Book already exists");
                return;
            }
            if (IsbnBox.Text.Length > 15)
            {
                MessageBox.Show("Invalid Isbn");
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
            if (IsInputValid())
            {
                MessageBox.Show("Empty fields are not allowed. ',' or ';' are also not allowed");
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
                dt.Rows[SelectedRow]["Price"] = LoggedInUser.GetCurrency() + priceBox.Text;
                dt.Rows[SelectedRow]["Stock"] = int.Parse(stockBox.Text);
                dt.Rows[SelectedRow]["MinStock"] = int.Parse(minStockBox.Text);
                ClearFields();
                dataGridView1.ClearSelection();
            }
            else
            {
                MessageBox.Show("Please select a row to update");
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
            priceBox.Text = dt.Rows[SelectedRow]["Price"].ToString().Substring(1);
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
            else
            {
                MessageBox.Show("Please select a row to delete");
                return;
            }
        }
    }
}
