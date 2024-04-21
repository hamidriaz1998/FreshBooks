using BookShopForms.Forms;
using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopForms
{
    public partial class Source : Form
    {
        public Source()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            ObjectHandler.GetUserDL().LoadUsers();
            ObjectHandler.GetBookDL().LoadBooks();
            ObjectHandler.GetCustomerDL().LoadCustomers();
        }

        private void dbButton_Click(object sender, EventArgs e)
        {
            AppSettings.UseDB();
            LoadData();
            Landing landing = new Landing();
            this.Hide();
            landing.ShowDialog();
            this.Close();
        }

        private void FHButton_Click(object sender, EventArgs e)
        {
            AppSettings.UseFH();
            LoadData();
            Landing landing = new Landing();
            this.Hide();
            landing.ShowDialog();
            this.Close();
        }
    }
}
