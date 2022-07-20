using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PropuskScaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connString = @"Data Source=DOCOIT;Initial Catalog=test;Integrated Security=True";
        public OleDbConnection oleDbConnection = null;
        public SqlConnection sqlConnection = null;

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        async void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                SqlCommand myCommand = new SqlCommand("SELECT * FROM UsersNow", conn);
                SqlDataReader dr = await myCommand.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(dr);
                guna2DataGridView1.DataSource = dt.DefaultView;
                conn.Close();

            }

        }

        async void guna2Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                SqlCommand myCommand = new SqlCommand($"SELECT count(*) FROM UsersNow Where Login = '{loginTb.Text.Trim()}' and Password = '{passTb.Text.Trim()}';", conn);
                SqlDataReader dr = await myCommand.ExecuteReaderAsync();
                await dr.ReadAsync();
                int a = Convert.ToInt32(dr[0]);
                if (a == 1)
                {
                    Message.Show("Успех");
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("Пользователя не существует, либо введен неверный логин или пароль", "Ошибка");
                }

            }
        }
    }
}
