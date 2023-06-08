using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace AirlineManagementSystem
{
    public partial class RecordAirplane : Form
    {
        public RecordAirplane()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ADD Airplane Details
            string constring = "server=localhost;user=root;database=airline_management_system;port=3306;password=root";
            using (MySqlConnection con = new MySqlConnection(constring))
            {
                if (Manufacturing.Text == "" || Model_no.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("spAddAirplane", con);
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Manufacturing", Manufacturing.Text);
                        cmd.Parameters.AddWithValue("Model_number", Model_no.Text);
                        int j = cmd.ExecuteNonQuery();
                        if (j >= 0)
                        {
                            MessageBox.Show("Airplane Recorded Successfully");
                        }
                        con.Close();

                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reset Button
            Manufacturing.Text = "";
            Model_no.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Back button
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // View Flight Button
            ViewAirplane viewairplane = new ViewAirplane();
            viewairplane.Show();
            this.Hide();
        }
    }
}
