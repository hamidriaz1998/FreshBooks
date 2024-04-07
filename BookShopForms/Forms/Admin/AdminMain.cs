﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShopForms.Forms.Admin
{
    public partial class AdminMain : Form
    {
        public AdminMain()
        {
            InitializeComponent();
            LoadForm(new DashBoard());
        }
        private void LoadForm(object form)
        {
            if (this.MainPanel.Controls.Count > 0)
            {
                this.MainPanel.Controls.RemoveAt(0);
            }
            Form f = form as Form;
            f.FormBorderStyle = FormBorderStyle.None;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.MainPanel.Controls.Add(f);
            this.MainPanel.Tag = f;
            f.Show();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DashBoardButton_Click(object sender, EventArgs e)
        {
LoadForm(new DashBoard());
        }

        private void BooksButton_Click(object sender, EventArgs e)
        {
            LoadForm(new BookForm());
        }

        private void UsersButton_Click(object sender, EventArgs e)
        {
            LoadForm(new UsersForm());
        }
    }
}
