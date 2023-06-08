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

namespace AirlineManagementSystem
{
    public partial class RecordFlight : Form
    {
        public RecordFlight()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // ADD Flight Details
                string constring = "server=localhost;user=root;database=airline_management_system;port=3306;password=root";
                using (MySqlConnection con = new MySqlConnection(constring))
                {
                    if (Fcodetb.Text == "" || Fdate.Text == "" || Fstoptb.Text == "")
                    {
                        MessageBox.Show("Missing Information");
                    }
                    else
                    {
                        try
                        {
                        // Convert.ToInt32(asd.Text) for converting to int
                            if (MessageBox.Show("Have you added stops ?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                con.Open();
                                MySqlCommand cmd = new MySqlCommand("spAddFlight", con);
                                cmd.Connection = con;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("Flight_Code", Fcodetb.Text);
                                cmd.Parameters.AddWithValue("Origin_Id", FOrigcb.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("Destination_Id", Fdescb.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("Airplane_Id", FAirplaneTypecb.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("Flight_Date", Fdate.Value.Date.ToString());
                                cmd.Parameters.AddWithValue("No_Stops", Fstoptb.Text);
                                int j = cmd.ExecuteNonQuery();
                                if (j >= 0)
                                {
                                    MessageBox.Show("Flight Recorded Successfully");
                                }
                                con.Close();
                            }
                 
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message);
                        }
                    }
                }
        }
            
        private void label8_Click(object sender, EventArgs e)
        {
            // Cross button
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reset Button
            Fcodetb.Text = "";
            FOrigcb.Text = "";
            Fdescb.Text = "";
            FAirplaneTypecb.Text = "";
            Fdate.Text = "";
            Fstoptb.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // View Flight Button
            ViewFlight viewflight = new ViewFlight();
            viewflight.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Back button
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void FOrigcb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Fdescb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private void Fstoptb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Add Stops button
            
            if (Fstoptb.Text != "")
            {
                AddStops addstop = new AddStops(Fstoptb.Text.ToString());
                addstop.Show();
                this.Hide();
                button5.Enabled = false;
            }
            else
            {
                button5.Enabled= false;
                MessageBox.Show("Enter value in No Of Stops!!!!");
                Thread.Sleep(500);
                Application.DoEvents();
                button5.Enabled = true;
            }
        }

        private void RecordFlight_Load(object sender, EventArgs e)
        {

            // Display screen

            MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");
            string query = "select * from airport";
            con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FOrigcb.DataSource = ds.Tables[0];
            FOrigcb.DisplayMember = "City";
            FOrigcb.ValueMember= "Airport_Id";
            con.Close();

            con.Open();
            MySqlDataAdapter sda2 = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            Fdescb.DataSource = ds2.Tables[0];
            Fdescb.DisplayMember = "City";
            Fdescb.ValueMember = "Airport_Id";
            con.Close();

            string query2 = "select * from airplane";
            con.Open();
            MySqlDataAdapter sda3 = new MySqlDataAdapter(query2, con);
            MySqlCommandBuilder builder3 = new MySqlCommandBuilder(sda3);
            var ds3 = new DataSet();
            sda3.Fill(ds3);
            FAirplaneTypecb.DataSource = ds3.Tables[0];
            FAirplaneTypecb.DisplayMember = "Type";
            FAirplaneTypecb.ValueMember = "Airplane_Id";
            con.Close();
        }

    }
}
