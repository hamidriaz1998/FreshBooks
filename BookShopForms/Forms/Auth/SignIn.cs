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
using Library.BL;
using Library.DL;

namespace BookShopForms.Forms
{
    public partial class SignIn : Form
    {
        IUserDL UserDL = ObjectHandler.GetUserDL();
        public SignIn()
        {
            InitializeComponent();
            SetTabIndices();
        }
        private void SetTabIndices()
        {
            UsernameBox.TabIndex = 0;
            PasswordBox.TabIndex = 1;
            SignInButton.TabIndex = 2;
            BackButton.TabIndex = 3;
        }
        private void ClearFields()
        {
            UsernameBox.Text = "";
            PasswordBox.Text = "";
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Text;
            User u = UserDL.Login(username, password);
            ClearFields();
            if (u == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }
            if (u.GetType() == "admin")
            {
                this.Hide();
                new AdminMain((Admin) u).ShowDialog();
                this.Show();
            }
            //else
            //{
            //    this.Hide();
            //    new SalesmanDashboard().Show();
            //}
        }
    }
}
