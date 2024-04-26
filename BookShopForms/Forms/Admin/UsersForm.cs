using System.Collections.Generic;
using System;
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
    public partial class UsersForm : Form
    {
        IUserDL UserDL = ObjectHandler.GetUserDL();
        Validations validations = ObjectHandler.GetValidations();
        private DataTable dt = new DataTable();
        private int SelectedRow = 0;
        public UsersForm()
        {
            InitializeComponent();
        }
        private void SetTabIndices()
        {
            UsernameBox.TabIndex = 0;
            PasswordBox.TabIndex = 1;
            CurrencyBox.TabIndex = 2;
            EarningsBox.TabIndex = 3;
            SalaryBox.TabIndex = 4;
            AddButton.TabIndex = 5;
            updateBtn.TabIndex = 6;
            DeleteButton.TabIndex = 7;
        }
        private void FillCurrencyBox()
        {
            List<string> list = new List<string>() { "$", "€", "£" };
            CurrencyBox.DataSource = list;
        }
        private void LoadData()
        {
            List<Salesman> s = UserDL.GetSalesmen();
            foreach (Salesman salesman in s)
            {
                AddDataRow(salesman);
            }
        }
        private void AddDataRow(Salesman s)
        {
            DataRow dr = dt.NewRow();
            dr["Id"] = s.GetID();
            dr["Username"] = s.GetUsername();
            dr["Password"] = s.GetPassword();
            dr["Currency"] = s.GetCurrency();
            dr["Earnings"] = s.GetEarnings();
            dr["Salary"] = s.GetSalary();
            dr["Sales"] = s.GetSales();
            dt.Rows.Add(dr);
        }
        private void UserForm_Load(object sender, EventArgs e)
        {
            SetTabIndices();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("Currency", typeof(string));
            dt.Columns.Add("Earnings", typeof(int));
            dt.Columns.Add("Salary", typeof(int));
            dt.Columns.Add("Sales",typeof(int));
            LoadData();
            FillCurrencyBox();
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
        }
        private bool CheckEmpty()
        {
            if (string.IsNullOrEmpty(EarningsBox.Text) || string.IsNullOrEmpty(SalaryBox.Text) || string.IsNullOrEmpty(EarningsBox.Text) || string.IsNullOrEmpty(UsernameBox.Text) || string.IsNullOrEmpty(PasswordBox.Text) || CurrencyBox.SelectedIndex < 0)
            {
                return false;
            }
            return true;
        }
        private bool CheckInt()
        {
            if (int.TryParse(EarningsBox.Text, out int earnings) && int.TryParse(SalaryBox.Text, out int salary))
            {
                return true;
            }
            return false;
        }
        private bool CheckNegative()
        {
            if (int.Parse(EarningsBox.Text) < 0 || int.Parse(SalaryBox.Text) < 0)
            {
                return true;
            }
            return false;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedRow = dataGridView1.CurrentCell.RowIndex;
            if (SelectedRow < 0 || SelectedRow >= dt.Rows.Count)
            {
                return;
            }
            UsernameBox.Text = dt.Rows[SelectedRow]["Username"].ToString();
            PasswordBox.Text = dt.Rows[SelectedRow]["Password"].ToString();
            CurrencyBox.SelectedItem = dt.Rows[SelectedRow]["Currency"].ToString();
            EarningsBox.Text = dt.Rows[SelectedRow]["Earnings"].ToString();
            SalaryBox.Text = dt.Rows[SelectedRow]["Salary"].ToString();
        }
        private bool CheckValidations()
        {
            if (!CheckEmpty())
            {
                MessageBox.Show("Please fill all fields");
                return false;
            }
            if (!CheckInt())
            {
                MessageBox.Show("Earnings and Salary must be integers");
                return false;
            }
            if (CheckNegative())
            {
                MessageBox.Show("Earnings and Salary must be positive");
                return false;
            }
            if (!validations.IsUsernameValid(UsernameBox.Text))
            {
                MessageBox.Show("Username must be between 4 and 20 characters long and can only contain letters and numbers.");
                return false;
            }
            if (!validations.IsPasswordValid(PasswordBox.Text))
            {
                MessageBox.Show("Your password needs to be at least 6 characters long and include both letters and numbers. Please note that ',' and ';' are not allowed.");
                return false;
            }
            return true;
        }
        private void ClearFields()
        {
            UsernameBox.Text = "";
            PasswordBox.Text = "";
            EarningsBox.Text = "";
            SalaryBox.Text = "";
            CurrencyBox.SelectedIndex = 0;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!CheckValidations())
            {
                return;
            }

            if (UserDL.UserExists(UsernameBox.Text))
            {
                MessageBox.Show("Username already exists");
                return;
            }
            Salesman s = new Salesman(UsernameBox.Text, PasswordBox.Text);
            s.SetCurrency(CurrencyBox.SelectedItem.ToString());
            s.SetEarnings(int.Parse(EarningsBox.Text));
            s.SetSalary(int.Parse(SalaryBox.Text));
            UserDL.AddUser(s);
            AddDataRow(s);
            ClearFields();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (!CheckValidations())
            {
                return;
            } 
            if (SelectedRow < 0 || SelectedRow >= dt.Rows.Count)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            int id = int.Parse(dt.Rows[SelectedRow]["Id"].ToString());
            Salesman s = (Salesman)UserDL.GetUser(id);
            if (s == null)
            {
                MessageBox.Show("User not found");
                return;
            }
            s.SetUsername(UsernameBox.Text);
            s.SetPassword(PasswordBox.Text);
            s.SetCurrency(CurrencyBox.SelectedItem.ToString());
            s.SetEarnings(int.Parse(EarningsBox.Text));
            s.SetSalary(int.Parse(SalaryBox.Text));
            UserDL.UpdateUser(s);
            dt.Rows[SelectedRow]["Username"] = s.GetUsername();
            dt.Rows[SelectedRow]["Password"] = s.GetPassword();
            dt.Rows[SelectedRow]["Currency"] = s.GetCurrency();
            dt.Rows[SelectedRow]["Earnings"] = s.GetEarnings();
            dt.Rows[SelectedRow]["Salary"] = s.GetSalary();
            ClearFields();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            SelectedRow = dataGridView1.CurrentCell.RowIndex;
            if (SelectedRow < 0 || SelectedRow >= dt.Rows.Count)
            {
                MessageBox.Show("Please select a user");
                return;
            }
            int id = int.Parse(dt.Rows[SelectedRow]["Id"].ToString());
            Salesman s = (Salesman)UserDL.GetUser(id);
            UserDL.RemoveUser(s);
            dt.Rows.RemoveAt(SelectedRow);
        }

     }
}
