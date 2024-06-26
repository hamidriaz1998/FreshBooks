﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Library.BL
{
    public class Salesman : User
    {
        private float Earnings;
        private float Salary;
        private int Sales;
        public Salesman(string username, string password) : base(username, password)
        {
            Salary = 1000;
            Earnings = 0;
            Sales = 0;
        }
        public Salesman(string username, string password, float earnings, float salary, int sales) : base(username, password)
        {
            Earnings = earnings;
            Salary = salary;
            Sales = sales;
        }
        // Getters Setters
        public float GetEarnings()
        {
            return Earnings;
        }
        public void SetEarnings(float earnings)
        {
            Earnings = earnings;
        }
        public float GetSalary()
        {
            return Salary;
        }
        public void SetSalary(float salary)
        {
            Salary = salary;
        }
        public int GetSales()
        {
            return Sales;
        }
        public void SetSales(int sales)
        {
            Sales = sales;
        }
        // Methods
        public void AddSale(float price)
        {
            Earnings += price;
            Sales++;
        }
        public void AddSalary(float amount)
        {
            Salary += amount;
        }
        public override string GetType()
        {
            return "salesman";
        }
    }
}
