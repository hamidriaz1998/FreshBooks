using BookShopForms.Forms.AdminForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopForms.Forms.AdminForms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        private void SetTabIndices()
        {
            CurrencyBox.TabIndex = 0;
            CurrencyUpdateBtn.TabIndex = 4;
            OldPassBox.TabIndex = 1;
            NewPassBox.TabIndex = 2;
            ConfirmPassBox.TabIndex = 3;
            PassUpdateBtn.TabIndex = 5;
        }
        private void UpdateUser()
        {
            AdminMain.LoggedInUser.SetCurrency(CurrencyBox.SelectedItem.ToString());
        }
        private void LoadCurrencyBox()
        {
            List<String> list = new List<String>() { "$", "€", "£" };
            CurrencyBox.DataSource = list;
            CurrencyBox.SelectedItem = AdminMain.LoggedInUser.GetCurrency();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            SetTabIndices();
            LoadCurrencyBox();
        }
        private bool CheckEmpty()
        {
            if (string.IsNullOrEmpty(OldPassBox.Text) || string.IsNullOrEmpty(NewPassBox.Text) || string.IsNullOrEmpty(ConfirmPassBox.Text))
            {
                return true;
            }
            return false;
        }
        private void CurrencyUpdateBtn_Click(object sender, EventArgs e)
        {
            if (CurrencyBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a currency");
                return;
            }
            ObjectHandler.GetUserDL().UpdateUser(AdminMain.LoggedInUser);
            UpdateUser();
            MessageBox.Show("Currency updated successfully");
        }

        private void PassUpdateBtn_Click(object sender, EventArgs e)
        {
            if (CheckEmpty())
            {
                MessageBox.Show("Please fill all fields");
                return;
            }
            if (OldPassBox.Text != AdminMain.LoggedInUser.GetPassword())
            {
                MessageBox.Show("Old password is incorrect");
                return;
            }
            if (NewPassBox.Text != ConfirmPassBox.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }
            AdminMain.LoggedInUser.SetPassword(NewPassBox.Text);
            UpdateUser();
        }
    }
}
