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
    public partial class RecordBooking : Form
    {
        public RecordBooking()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {

            // Back button
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            // Reset Button
            flightcb.Text = "Select the Flight Code";
            bookingName.Text = "";
            Surname.Text = "";
            Address.Text = "";
            Phone.Text = "";
            bookingType.Text = "";
            Charge.Text = "";
            bookingdate.Text = "";
        }

        private void Record_Click(object sender, EventArgs e)
        {
            // ADD Booking Details
            string constring = "server=localhost;user=root;database=airline_management_system;port=3306;password=root";
            using (MySqlConnection con = new MySqlConnection(constring))
            {
                if (bookingName.Text == "" || Surname.Text == "" || Address.Text == "" || Phone.Text == "" || Charge.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("spAddBooking", con);
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Flight_Id", flightcb.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("Name", bookingName.Text);
                        cmd.Parameters.AddWithValue("Surname", Surname.Text);
                        cmd.Parameters.AddWithValue("Address", Address.Text);
                        cmd.Parameters.AddWithValue("Phone", Phone.Text);
                        cmd.Parameters.AddWithValue("T_type", bookingType.Text);
                        cmd.Parameters.AddWithValue("Charge", Charge.Text);
                        cmd.Parameters.AddWithValue("Ticket_Date", bookingdate.Value.Date.ToString());
                        cmd.Parameters.AddWithValue("b_p_name", b_p_name.Text);
                        int j = cmd.ExecuteNonQuery();
                        if (j >= 0)
                        {
                            MessageBox.Show("Booking Recorded Successfully");
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

        private void view_booking_Click(object sender, EventArgs e)
        {

            // View Booking Button
            ViewBooking viewBooking = new ViewBooking();
            viewBooking.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void RecordBooking_Load(object sender, EventArgs e)
        {
            flightcb.Text = "Select Flight Code";

            MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");
            string query = "select * from flight";
            con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            flightcb.DataSource = ds.Tables[0];
            flightcb.DisplayMember = "Flight_Code";
            flightcb.ValueMember = "Flight_Id";
            con.Close();
        }
    }
}
