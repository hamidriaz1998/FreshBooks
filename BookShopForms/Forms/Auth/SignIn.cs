using BookShopForms.Forms.Admin;
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
using BookShopForms.DL;

namespace BookShopForms.Forms
{
    public partial class SignIn : Form
    {
        IUserDL UserDL = ObjectHandler.GetUserDL();
        public SignIn()
        {
            InitializeComponent();
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
            if (u == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }
            if (u.GetType() == "admin")
            {
                this.Hide();
                new AdminMain((BL.Admin) u).ShowDialog();
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
