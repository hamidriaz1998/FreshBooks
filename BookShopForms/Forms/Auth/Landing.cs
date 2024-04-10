using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopForms.Forms
{
    public partial class Landing : Form
    {
        public Landing()
        {
            InitializeComponent();
        }
        private void SetTabIndices()
        {
            SignUpButton.TabIndex = 0;
            SignInButton.TabIndex = 1;
        }
        private void Landing_Load(object sender, EventArgs e)
        {
            SetTabIndices();
        }
        private void SignUpButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.ShowDialog();
            this.Show();
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignIn signIn = new SignIn();
            signIn.ShowDialog();
            this.Show();
        }
    }
}
