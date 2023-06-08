using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
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
    public partial class AddStops : Form
    {
        public string Stops;
        int counter = 0;
        public AddStops(string no_of_stops)
        {

            InitializeComponent();
            Stops = no_of_stops;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Stops != "")
            {                
                if (counter == Int16.Parse(Stops)+1) { button2.Enabled = false; }
                counter++;
            }

            string constring = "server=localhost;user=root;database=airline_management_system;port=3306;password=root";

            using (MySqlConnection con = new MySqlConnection(constring))
            {
                if (arr_time.Text == "" || dep_time.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("spAddStop", con);
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Airport_Id", airportcb.SelectedValue.ToString()); 
                        cmd.Parameters.AddWithValue("Arr_Date", arr_datetext.Text);
                        cmd.Parameters.AddWithValue("Arr_Time", arr_time.Text);
                        cmd.Parameters.AddWithValue("Dep_Date", dep_datetext.Text);
                        cmd.Parameters.AddWithValue("Dep_Time", dep_time.Text); 
                        cmd.Parameters.AddWithValue("Flight_Id", Flight_Idcb.SelectedValue.ToString());
                        int j = cmd.ExecuteNonQuery();
                        if (j >= 0)
                        {
                            MessageBox.Show("Stop Recorded Successfully");
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

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arr_datetext.Text = "Enter Date in YYYY-MM-DD";
            arr_time.Text = "";
            dep_datetext.Text = "Enter Date in YYYY-MM-DD";
            dep_time.Text = "";
            airportcb.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecordFlight recordFlight= new RecordFlight();
            recordFlight.Show();
            this.Hide();
        }

        private void airportcb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddStops_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");
            string query = "select * from airport";
            con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            airportcb.DataSource = ds.Tables[0];
            airportcb.DisplayMember = "City";
            airportcb.ValueMember = "Airport_Id";
            con.Close();

            string query2 = "select * from flight";
            con.Open();
            MySqlDataAdapter sda2 = new MySqlDataAdapter(query2, con);
            MySqlCommandBuilder builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            Flight_Idcb.DataSource = ds2.Tables[0];
            Flight_Idcb.DisplayMember = "Flight_Code";
            Flight_Idcb.ValueMember = "Flight_Id";
            con.Close();

            arr_datetext.Text = "Enter Date in YYYY-MM-DD";
            dep_datetext.Text = "Enter Date in YYYY-MM-DD";
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
