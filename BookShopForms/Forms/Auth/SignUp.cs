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
using Library.Utils;

namespace BookShopForms
{
    public partial class SignUp : Form
    {
        IUserDL UserDL = ObjectHandler.GetUserDL();
        Validations validations = ObjectHandler.GetValidations();
        public SignUp()
        {
            InitializeComponent();
            LoadComboBox();
            SetTabIndices();
        }
        private void SetTabIndices()
        {
            UsernameBox.TabIndex = 0;
            PasswordBox.TabIndex = 1;
            RoleBox.TabIndex = 2;
            SignUpButton.TabIndex = 3;
            BackButton.TabIndex = 4;
        }
        private void LoadComboBox()
        {
            List<string> roles = new List<string> { "Admin", "Salesman" };
            RoleBox.DataSource = roles;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();    
        }
        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string role = RoleBox.SelectedItem.ToString();
            string username = UsernameBox.Text;
            string password = PasswordBox.Text;
            if (role == "Admin")
            {
                if (UserDL.AdminExists())
                {
                    MessageBox.Show("System can only have one admin");
                    return;
                }
                Admin admin = new Admin(username,password);
                if (UserDL.AddUser(admin))
                {
                    MessageBox.Show("Admin created successfully");
                }
                else
                {
                    MessageBox.Show("An Error occured"); return;
                }
            }
            else if (role == "Salesman")
            {
                Salesman s  = new Salesman(username,password);
                if (UserDL.AddUser(s))
                {
                    MessageBox.Show("Salesman created successfully");
                }
                else
                {
                    MessageBox.Show("An Error occured"); return;
                }
            }
        }

        private void UsernameBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox box = (Guna2TextBox)sender;
            if (!validations.IsUsernameValid(box.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Username must be between 5 and 20 characters long and can only contain letters and numbers.");
            }
            else if (UserDL.UserExists(box.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Username already exists");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void PasswordBox_Validating(object sender, CancelEventArgs e)
        {
            Guna2TextBox box = (Guna2TextBox)sender;
            if (!validations.IsPasswordValid(box.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(box, "Your password needs to be at least 6 characters long and include both letters and numbers. Please note that ',' and ';' are not allowed.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
