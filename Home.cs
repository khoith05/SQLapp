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
    public partial class Home : Form
    {
        public string connString = @"Data Source=DESKTOP-V9JSR1D\SQLEXPRESS;Initial Catalog=PUBLIC_TRAFFIC;User ID=sManager;Password=123456";
        SqlConnection conn;
        struct BtName
        {
            public string button_name;
            public string table_name;
        };
        BtName[] btName= new BtName[20];
        string[] button_name ={"Hành khách", "Chuyến tàu xe", "Chuyến tàu xe ghé ga trạm", "Con đường", "Đoạn đường",
            "Ga trạm", "Ga trạm làm việc", "Giao lộ", "Vé ngày", "Hoạt động vé 1 ngày", "Vé tháng", "Hoạt động vé tháng",
            "Nhân viên", "Thẻ từ", "Tuyến tàu điện", "Tuyến tàu xe", "Tuyến xe buýt", "Vé", "Vé lẻ","Bảng giá" };
        string[] table_name = {"Hanh_khach", "Chuyen_tau_xe", "Chuyen_tau_xe_ghe_ga_tram", "Con_duong", "Doan_duong",
            "Ga_tram", "Ga_tram_lam_viec", "Giao_lo", "Ve_1_ngay", "Hoat_dong_ve_1_ngay", "Ve_thang", "Hoat_dong_ve_thang",
            "Nhan_vien", "The_tu", "Tuyen_tau_dien", "Tuyen_tau_xe", "Tuyen_xe_buyt", "Ve", "Ve_le","Bang_gia" } ;

        public Home()
        {
            InitializeComponent();
            conn = new SqlConnection(connString);
            conn.Open();
            for (int i = 0; i < 20; i++)
            {
                btName[i].button_name =button_name[i];
                btName[i].table_name = table_name[i];
                Button b = new Button();
                b.Text = button_name[i];
                b.AutoSize= true;
                b.Click += new EventHandler(this.ShowData);
                flowLayoutPanel1.Controls.Add(b);
            }
            

        }

        void ShowData(Object sender,EventArgs e) 
        {
            Button clickedButton = (Button)sender;
            String tName= "SELECT * FROM ";
            for(int i = 0; i < 20; i++)
            {
                if (clickedButton.Text == btName[i].button_name)
                {
                    tName += btName[i].table_name;
                    break;
                }
            }

            SqlDataAdapter sqlDA = new SqlDataAdapter(tName, conn);
            DataTable dtTable = new DataTable();
            sqlDA.Fill(dtTable);
            dataGridView1.DataSource = dtTable;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            conn.Close();
            this.Hide();
            Login h = new Login();
            h.FormClosed += (s, args) => this.Close();
            h.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string cmds = "EXEC ThongKeLuotNguoi '" + textBox1.Text + "', '" + textBox3.Text + "-"
                + textBox2.Text + "-" + textBox4.Text + "', '" + textBox6.Text + "-" + textBox5.Text + "-" + textBox7.Text + "' ;";
                SqlDataAdapter sqlDA = new SqlDataAdapter(cmds, conn);
                DataTable dtTable = new DataTable();
                sqlDA.Fill(dtTable);
                dataGridView1.DataSource = dtTable;
            }
            catch
            {
                MessageBox.Show("Error!");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add h = new Add();
            h.Show();
            h.connString = connString;
        }

    }
}
