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
using System.Xml.Linq;

namespace AirlineManagementSystem
{
    public partial class RecordPilot : Form
    {
        public RecordPilot()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Add record button
            string constring = "server=localhost;user=root;database=airline_management_system;port=3306;password=root";
            using (MySqlConnection con = new MySqlConnection(constring))
            {
                if (rating.Text == "" || empcb.Text == "" || airplane_typecb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        // Convert.ToInt32(asd.Text) for converting to int
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("spAddPilot", con);
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Emp_Id", empcb.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("Rating", rating.Text);
                        cmd.Parameters.AddWithValue("Airplane_Type",airplane_typecb.SelectedValue.ToString());
                        int j = cmd.ExecuteNonQuery();
                        if (j >= 0)
                        {
                            MessageBox.Show("Pilot Recorded Successfully");
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

        private void button4_Click(object sender, EventArgs e)
        {
            RecordStaff staff = new RecordStaff();
            staff.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // reset button
            empcb.Text = "";
            rating.Text = "";
            airplane_typecb.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewPilot pilot = new ViewPilot();
            pilot.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
