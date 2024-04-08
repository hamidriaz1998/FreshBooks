using BookShopForms.BL;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShopForms.DL;

namespace BookShopForms.Forms.Admin
{
    public partial class UsersForm : Form
    {
        IUserDL UserDL = ObjectHandler.GetUserDL();
        private DataTable dt = new DataTable();
        private int SelectedRow = 0;
        public UsersForm()
        {
            InitializeComponent();
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
        }
        private bool CheckEmpty()
        {
            if (string.IsNullOrEmpty(EarningsBox.Text) || string.IsNullOrEmpty(SalaryBox.Text) || string.IsNullOrEmpty(EarningsBox.Text) || string.IsNullOrEmpty(UsernameBox.Text) || string.IsNullOrEmpty(PasswordBox.Text) || CurrencyBox.SelectedIndex == 0)
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
    }
}
