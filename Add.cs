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
    public partial class Add : Form
    {
        public string connString="";
        public Add()
        {
            InitializeComponent();

            comboBox1.Items.Add("Tuyến xe bus");
            comboBox1.Items.Add("Tuyến tàu điện");

            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add(" ", 25);
            listView1.Columns.Add("Mã ga/trạm",120);
            listView1.Columns.Add("STT trạm dừng",140);
            listView1.Columns.Add("Giờ ghé",80);
            listView1.Columns.Add("Giờ đi",80);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text== "Tuyến xe bus")
            {
                label2.Hide();
                label3.Hide();
                label4.Hide();
                textBox2.Hide();
                textBox3.Hide();
                textBox4.Hide();
                textBox1.Text = "B";    
            }
            else
            {
                label2.Show();
                label3.Show();
                label4.Show();
                textBox2.Show();
                textBox3.Show();
                textBox4.Show();
                textBox1.Text = "T";

            }
            label15.Hide();
            label17.Hide();
            label18.Hide();
            label19.Hide();
            label20.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label15.Text = textBox1.Text;
            label17.Text = textBox2.Text;
            label18.Text = textBox3.Text;
            label19.Text = textBox4.Text;
            label20.Text = textBox5.Text;
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            label15.Show();
            label20.Show();
            if (comboBox1.Text == "Tuyến tàu điện")
            {
                label17.Show();
                label18.Show();
                label19.Show();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label15.Hide();
            label17.Hide();
            label18.Hide();
            label19.Hide();
            label20.Hide();
            textBox1.Show();
            textBox5.Show();
            if (comboBox1.Text=="Tuyến tàu điện")
            {
                textBox2.Show();
                textBox3.Show();
                textBox4.Show();
                
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string[] row = { " ", textBox6.Text, textBox11.Text,
                textBox7.Text + ":" + textBox9.Text, textBox8.Text + ":" + textBox10.Text };
            ListViewItem item = new ListViewItem(row);
            listView1.Items.Add(item);
            textBox6.Text = "";
            textBox11.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.SelectedItems[0].Remove();
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            using var cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                cmd.CommandText = "INSERT INTO Tuyen_tau_xe VALUES ('" + textBox1.Text + "');\n";
                //cmd.ExecuteNonQuery();
                if(comboBox1.Text=="Tuyến xe bus")
                {
                    cmd.CommandText += "INSERT INTO Tuyen_xe_buyt VALUES('" + textBox1.Text + "');\n";
                    //cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText += "ALTER TABLE Tuyen_xe_dien NOCHECK CONSTRAINT fk_tuyentau_matuyentau;\n";
                    //cmd.ExecuteNonQuery();
                    cmd.CommandText += "INSERT INTO Tuyen_xe_dien VALUES('" + textBox3.Text + "','" +
                        textBox2.Text + "'," + textBox4.Text+",'" + textBox1.Text+"'"+ ");\n";
                    //cmd.ExecuteNonQuery();
                    cmd.CommandText += "ALTER TABLE Tuyen_xe_dien CHECK CONSTRAINT fk_tuyentau_matuyentau;\n";
                    //cmd.ExecuteNonQuery();
                }
                cmd.CommandText += "ALTER TABLE Chuyen_tau_xe NOCHECK CONSTRAINT fk_chuyentauxe_matuyentauxe;\n";
                //cmd.ExecuteNonQuery();
                cmd.CommandText += "INSERT INTO Chuyen_tau_xe VALUES('" + textBox1.Text + "'," + textBox5.Text + ");\n";
                //cmd.ExecuteNonQuery();
                cmd.CommandText += "ALTER TABLE Chuyen_tau_xe CHECK CONSTRAINT fk_chuyentauxe_matuyentauxe;\n";
                //cmd.ExecuteNonQuery();
                int so_tram_dung = listView1.Items.Count;
                cmd.CommandText += "ALTER TABLE Chuyen_tau_xe_ghe_ga_tram NOCHECK CONSTRAINT fk_chuyenghegatram_chuyentau,fk_chuyenghegatram_gatram;\n";
                //cmd.ExecuteNonQuery();
                for (int i = 0; i < so_tram_dung; i++)
                {
                    string ma_ga_tram=listView1.Items[i].SubItems[1].Text;
                    string tram_dung=listView1.Items[i].SubItems[2].Text;
                    string gio_ghe= listView1.Items[i].SubItems[3].Text;
                    string gio_di= listView1.Items[i].SubItems[4].Text;
                    cmd.CommandText += "INSERT INTO Chuyen_tau_xe_ghe_ga_tram VALUES('" + textBox1.Text
                        + "'," + textBox5.Text + ",'" + ma_ga_tram + "'," + tram_dung + ",'" + gio_ghe 
                        + "','" + gio_di + "');\n";
                }
                cmd.CommandText += "ALTER TABLE Chuyen_tau_xe_ghe_ga_tram CHECK CONSTRAINT fk_chuyenghegatram_chuyentau,fk_chuyenghegatram_gatram;\n";
                cmd.ExecuteNonQuery();
                conn.Close();

                this.Close();
                

            }
            catch
            {
                MessageBox.Show("Error!");
            }
        }
    }
}
