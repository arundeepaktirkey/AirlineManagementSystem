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
    public partial class RecordStaff : Form
    {
        public RecordStaff()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // back button
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // reset button
            Fname.Text = "";
            Lname.Text = "";
            Address.Text = "";
            Phone.Text = "";
            Salary.Text = "";
            Positioncb.Text = "Select Position";
            flightcb.Text = "Select Flight Code";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            // cross button
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // view staff button
            ViewStaff viewstaff = new ViewStaff();
            viewstaff.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Add record button
            string constring = "server=localhost;user=root;database=airline_management_system;port=3306;password=root";
            using (MySqlConnection con = new MySqlConnection(constring))
            {
                if (Fname.Text == "" || Lname.Text == "" || Address.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        // Convert.ToInt32(asd.Text) for converting to int
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("spAddEmployee", con);
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Name", Fname.Text);
                        cmd.Parameters.AddWithValue("Surname", Lname.Text);
                        cmd.Parameters.AddWithValue("Address", Address.Text);
                        cmd.Parameters.AddWithValue("Phone", Phone.Text);
                        cmd.Parameters.AddWithValue("Position", Positioncb.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("salary", Salary.Text);
                        cmd.Parameters.AddWithValue("Flight_Id", flightcb.SelectedValue.ToString());
                        int j = cmd.ExecuteNonQuery();
                        if (j >= 0)
                        {
                            MessageBox.Show("Staff Recorded Successfully");
                        }
                        string pilot = Positioncb.SelectedValue.ToString();
                        if (pilot == "Pilot") 
                        {
                            button5.Visible= true;
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

        private void Position_TextChanged(object sender, EventArgs e)
        {

        }

        private void RecordStaff_Load(object sender, EventArgs e)
        {
            // record staff screen
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");
            string query = "select distinct Position from employee_position";
            con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Positioncb.DataSource = ds.Tables[0];
            Positioncb.DisplayMember = "Position";
            Positioncb.ValueMember = "Position";
            Positioncb.Text = "Select Position";
            con.Close();

            string query2 = "select * from flight";
            con.Open();
            MySqlDataAdapter sda2 = new MySqlDataAdapter(query2, con);
            MySqlCommandBuilder builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            flightcb.DataSource = ds2.Tables[0];
            flightcb.DisplayMember = "Flight_Code";
            flightcb.ValueMember = "Flight_Id";
            con.Close();

            button5.Visible = false;
        }

        private void Positioncb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            RecordPilot recpilot = new RecordPilot();
            recpilot.Show();
            this.Hide();
        }
    }
}
