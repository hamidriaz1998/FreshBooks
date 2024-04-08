using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookShopForms.BL;
using BookShopForms.Forms;
using BookShopForms.DL;

namespace BookShopForms
{
    public partial class SignUp : Form
    {
        IUserDL UserDL = ObjectHandler.GetUserDL();
        public SignUp()
        {
            InitializeComponent();
            LoadComboBox();
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
            if (UserDL.UserExists(username))
            {
                MessageBox.Show("User already exists");
                return;
            }   
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
    }
}
