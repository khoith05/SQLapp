using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQLapp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string connString = @"Data Source="+textBox3.Text+
                ";Initial Catalog=PUBLIC_TRAFFIC;Persist Security Info=True;User ID=" 
                + textBox1.Text + ";Password=" + textBox2.Text;*/
            string connString = @"Data Source=DESKTOP-V9JSR1D\SQLEXPRESS;Initial Catalog=PUBLIC_TRAFFIC;User ID="
                + textBox1.Text + ";Password=" + textBox2.Text+";";
            SqlConnection conn;
            try
            {
                
                conn= new SqlConnection(connString);
                conn.Open();
                this.Hide();
                Home h = new Home();
                h.FormClosed += (s, args) => this.Close();                
                h.Show();
                h.connString = connString;          
                MessageBox.Show("Welcome to Homepage");
                
            }
            catch 
            {
                MessageBox.Show("Invalid Username or Password");
                
            }
            

        }
    }
}
