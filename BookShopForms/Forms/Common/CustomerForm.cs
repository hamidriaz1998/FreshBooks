using Library.BL;
using Library.DL;
using Library.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BookShopForms.Forms.Common
{
    public partial class CustomerForm : Form
    {
        private static Validations Validations = ObjectHandler.GetValidations();
        private static ICustomerDL CustomerDL = ObjectHandler.GetCustomerDL();
        DataTable dt = new DataTable();
        int SelectedRow = 0;
        public CustomerForm()
        {
            InitializeComponent();
        }
        private void SetTabIndices()
        {
            NameBox.TabIndex = 0;
            EmailBox.TabIndex = 1;
            PhoneBox.TabIndex = 2;
            AddressBox.TabIndex = 3;
            AddButton.TabIndex = 4;
            updateBtn.TabIndex = 5;
            DeleteButton.TabIndex = 6;
        }
        private void LoadData()
        {
            List<Customer> customers = CustomerDL.GetCustomers();
            foreach (Customer customer in customers)
            {
                AddDataRow(customer);
            }
        }
        private void AddDataRow(Customer c)
        {
            DataRow dr = dt.NewRow();
            dr["Id"] = c.GetID();
            dr["Name"] = c.GetName();
            dr["Email"] = c.GetEmail();
            dr["Phone"] = c.GetPhone();
            dr["Address"] = c.GetAddress();
            dt.Rows.Add(dr);
        }
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            SetTabIndices();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            LoadData();
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = dataGridView1.CurrentCell.RowIndex;
            if (SelectedRow < 0 || SelectedRow >= dt.Rows.Count)
            {
                return;
            }
            NameBox.Text = dt.Rows[SelectedRow]["Name"].ToString();
            EmailBox.Text = dt.Rows[SelectedRow]["Email"].ToString();
            PhoneBox.Text = dt.Rows[SelectedRow]["Phone"].ToString();
            AddressBox.Text = dt.Rows[SelectedRow]["Address"].ToString();
        }
        private bool IsValidInput()
        {
            if (Validations.ValidInput(NameBox.Text) && Validations.ValidInput(EmailBox.Text) && Validations.ValidInput(PhoneBox.Text) && Validations.ValidInput(AddressBox.Text))
            {
                return true;
            }
            return false;
        }
        private void ClearFields()
        {
            NameBox.Text = "";
            EmailBox.Text = "";
            PhoneBox.Text = "";
            AddressBox.Text = "";
        }
        private bool CheckValidations()
        {
            if (!IsValidInput())
            {
                MessageBox.Show("Empty fields are not allowed. ',' or ';' are also not allowed");
                return false;
            }
            if (!Validations.IsEmailValid(EmailBox.Text))
            {
                MessageBox.Show("Please enter a valid email");
                return false;
            }
            if (!Validations.IsPhoneValid(PhoneBox.Text))
            {
                MessageBox.Show("Please enter a valid phone number");
                return false;
            }
            return true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!CheckValidations())
            {
                return;
            }
            Customer c = new Customer(NameBox.Text, EmailBox.Text, PhoneBox.Text, AddressBox.Text);
            if (CustomerDL.CustomerExists(c))
            {
                MessageBox.Show("Customer with this email already exists");
                return;
            }
            CustomerDL.AddCustomer(c);
            AddDataRow(c);
            ClearFields();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (!CheckValidations())
            {
                return;
            }
            SelectedRow = dataGridView1.CurrentCell.RowIndex;
            if (SelectedRow >= 0 && SelectedRow <= dt.Rows.Count)
            {
                int id = int.Parse(dt.Rows[SelectedRow]["Id"].ToString());
                Customer c = CustomerDL.FindCustomer(id);
                c.SetName(NameBox.Text);
                c.SetEmail(EmailBox.Text);
                c.SetPhone(PhoneBox.Text);
                c.SetAddress(AddressBox.Text);
                CustomerDL.UpdateCustomer(c);
                dt.Rows[SelectedRow]["Name"] = NameBox.Text;
                dt.Rows[SelectedRow]["Email"] = EmailBox.Text;
                dt.Rows[SelectedRow]["Phone"] = PhoneBox.Text;
                dt.Rows[SelectedRow]["Address"] = AddressBox.Text;
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a row to update");
                return;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SelectedRow = dataGridView1.CurrentCell.RowIndex;
            if (SelectedRow >= 0 && SelectedRow <= dt.Rows.Count)
            {
                int id = int.Parse(dt.Rows[SelectedRow]["Id"].ToString());
                CustomerDL.RemoveCustomer(id);
                dt.Rows.RemoveAt(SelectedRow);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a row to delete");
                return;
            }
        }
    }
}
