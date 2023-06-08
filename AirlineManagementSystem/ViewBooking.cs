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
    public partial class ViewBooking : Form
    {
        public ViewBooking()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");

        private void populate()
        {
            MySqlCommand cmd = new MySqlCommand("spDisplayBooking", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (bookingName.Text == "" || Surname.Text == "" || Address.Text == "" || Phone.Text == "" || Charge.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    // Convert.ToInt32(asd.Text) for converting to int
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spUpdateBooking", con);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("b_id", bookingId.Text);
                    cmd.Parameters.AddWithValue("Name", bookingName.Text);
                    cmd.Parameters.AddWithValue("Surname", Surname.Text);
                    cmd.Parameters.AddWithValue("Address", Address.Text);
                    cmd.Parameters.AddWithValue("Phone", Phone.Text);
                    cmd.Parameters.AddWithValue("T_type", T_type.Text);
                    cmd.Parameters.AddWithValue("Charge", Charge.Text);
                    cmd.Parameters.AddWithValue("booking_Date", bookingdate.Value.Date.ToString());
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0)
                    {
                        MessageBox.Show("Booking Updated Successfully");
                    }
                    con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            /* delete button */
            if (bookingId.Text == "")
            {
                MessageBox.Show("Enter the Booking to Delete");
            }
            else
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spDeleteBooking", con);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pas_id", bookingId.Text);
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0)
                    {
                        MessageBox.Show("Booking Deleted Successfully");
                    }
                    con.Close();
                    populate();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            bookingName.Text = "";
            Surname.Text = "";
            Address.Text = "";
            Phone.Text = "";
            T_type.Text = "";
            Charge.Text = "";
            bookingdate.Text = "";
            search.Text = "Type to search";
        }

        private void back_Click(object sender, EventArgs e)
        {
            RecordBooking recbooking= new RecordBooking();
            recbooking.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bookingId.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Phone.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            bookingName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            T_type.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            Surname.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            Charge.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            Address.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            bookingdate.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
        }

        private void ViewBooking_Load(object sender, EventArgs e)
        {
            populate();
            search.Text = "Type to search";
            con.Close();

        }



        private void button11_Click(object sender, EventArgs e)
        {
            RadioButton radioBtn = this.Controls.OfType<RadioButton>()
                                       .Where(x => x.Checked).FirstOrDefault();
            if (radioBtn != null)
            {
                switch (radioBtn.Name)
                {
                    case "radioButton1":

                        string query = "select p.Passenger_Id, p.Name, p.Surname, p.Address, p.Phone, t.T_type, t.Charge, t.Ticket_Date, p.b_p_name From airline_management_system.passenger as p Inner join airline_management_system.ticket as t on p.Passenger_Id = t.Ticket_Id where p.Name = '" + search.Text + "';";
                        MessageBox.Show(query);
                        con.Open();
                        MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];
                        con.Close();
                        break;

                    case "radioButton2":
                        string query2 = "select p.Passenger_Id, p.Name, p.Surname, p.Address, p.Phone, t.T_type, t.Charge, t.Ticket_Date, p.b_p_name From airline_management_system.passenger as p Inner join airline_management_system.ticket as t on p.Passenger_Id = t.Ticket_Id where p.b_p_name = '" + search.Text + "';";
                        MessageBox.Show(query2);
                        con.Open();
                        MySqlDataAdapter sda2 = new MySqlDataAdapter(query2, con);
                        MySqlCommandBuilder builder2 = new MySqlCommandBuilder(sda2);
                        var ds2 = new DataSet();
                        sda2.Fill(ds2);
                        dataGridView1.DataSource = ds2.Tables[0];
                        con.Close();
                        break;

                }
            }
        }
    }
}
