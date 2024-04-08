﻿using Library.DL;
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
    public partial class DashBoard : Form
    {
        private IBookDL BookDL = ObjectHandler.GetBookDL();
        private IUserDL UserDL = ObjectHandler.GetUserDL();
        public DashBoard()
        {
            InitializeComponent();
        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            UserNameLabel.Text = "Welcome, " + AdminMain.LoggedInUser.GetUsername();
            BooksCountLabel.Text = BookDL.GetBooks().Count.ToString();
            EarningsLabel.Text = AdminMain.LoggedInUser.GetCurrency() + UserDL.GetTotalEarnings().ToString();
            UsersLabel.Text = UserDL.GetSalesmen().Count.ToString();
        }
    }
}
