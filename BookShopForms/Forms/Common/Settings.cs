using Library.BL;
using Library.DL;
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
using Guna.UI2.WinForms;
using Library.Utils;

namespace BookShopForms.Forms.Common
{
    public partial class Settings : Form
    {
        private User LoggedInUser = ObjectHandler.GetLoggedInUser();
        private static IUserDL UserDL = ObjectHandler.GetUserDL();
        private static Validations validations = ObjectHandler.GetValidations();
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
            LoggedInUser.SetCurrency(CurrencyBox.SelectedItem.ToString());
        }
        private void LoadCurrencyBox()
        {
            List<String> list = new List<String>() { "$", "€", "£" };
            CurrencyBox.DataSource = list;
            CurrencyBox.SelectedItem = LoggedInUser.GetCurrency();
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
            UserDL.UpdateUser(LoggedInUser);
            UpdateUser();
            MessageBox.Show("Currency updated successfully");
        }

        private void PassUpdateBtn_Click(object sender, EventArgs e)
        {
            LoggedInUser.SetPassword(NewPassBox.Text);
            UpdateUser();
        }

        private void OldPassBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox box = (Guna2TextBox)sender;
            if (box.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Please enter your old password");
            }
            else if (box.Text != LoggedInUser.GetPassword())
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Old password is incorrect");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }

        private void NewPassBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox box = (Guna2TextBox)sender;
            if (box.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Please enter a new password");
            }
            else if (box.Text == LoggedInUser.GetPassword())
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "New password cannot be the same as the old password");
            }
            else if (!validations.IsPasswordValid(box.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Password must be at least 6 characters long, can have special chars except ',' and ';', no spaces.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }

        private void ConfirmPassBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox box = (Guna2TextBox)sender;
            if (box.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Please confirm your new password");
            }
            else if (box.Text != NewPassBox.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Passwords do not match");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.Clear();
            }
        }
    }
}
