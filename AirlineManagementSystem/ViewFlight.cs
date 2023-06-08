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
    public partial class ViewFlight : Form
    {
        public ViewFlight()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");

        private void populate()
        {

       /*     con.Open();
            string query = "select * from flight";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();*/

            MySqlCommand cmd = new MySqlCommand("spDisplayFlight", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();

            /*            con.Open();
                        MySqlCommand cmd = new MySqlCommand("spDispayFlight", con);
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        int j = cmd.ExecuteNonQuery();
                        if (j >= 0)
                        {
                            MessageBox.Show("Flight Updated Successfully");
                        }
                        con.Close();*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecordFlight recflight = new RecordFlight();
            recflight.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            viewcode.Text = "";
            vieworigincb.Text = "";
            viewdestcb.Text = "";
            viewFdate.Text = "";
            viewstop.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /* delete button */
            if (viewcode.Text == "")
            {
                MessageBox.Show("Enter the Flight to Delete");
            }
            else
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spDeleteFlight", con);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Flight_Code", viewcode.Text);
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0)
                    {
                        MessageBox.Show("Flight Deleted Successfully");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (viewcode.Text == ""  ||  viewFdate.Text == "" || viewstop.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    // Convert.ToInt32(asd.Text) for converting to int
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spUpdateFlight", con);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Flight_Code", viewcode.Text);
                    cmd.Parameters.AddWithValue("Origin_Id", Convert.ToInt32(vieworigincb.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("Destination_Id", Convert.ToInt32(viewdestcb.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("Flight_Date", viewFdate.Value.Date.ToString());
                    cmd.Parameters.AddWithValue("Airplane_Id", Convert.ToInt32(airplaneTypecb.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("No_Stops", Convert.ToInt32(viewstop.Text));
                    int j = cmd.ExecuteNonQuery();          
                    if (j >= 0)
                    {
                        MessageBox.Show("Flight Updated Successfully");
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

        private void ViewFlight_Load(object sender, EventArgs e)
        {
            populate();

            MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");
            string query = "select * from airport";
            con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            vieworigincb.DataSource = ds.Tables[0];
            vieworigincb.DisplayMember = "City";
            vieworigincb.ValueMember = "Airport_Id";
            con.Close();

            con.Open();
            MySqlDataAdapter sda2 = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder2 = new MySqlCommandBuilder(sda2);
            var ds2 = new DataSet();
            sda2.Fill(ds2);
            viewdestcb.DataSource = ds2.Tables[0];
            viewdestcb.DisplayMember = "City";
            viewdestcb.ValueMember = "Airport_Id";
            con.Close();

            string query3 = "select * from airplane";
            con.Open();
            MySqlDataAdapter sda3 = new MySqlDataAdapter(query3, con);
            MySqlCommandBuilder builder3 = new MySqlCommandBuilder(sda3);
            var ds3 = new DataSet();
            sda3.Fill(ds3);
            airplaneTypecb.DataSource = ds3.Tables[0];
            airplaneTypecb.DisplayMember = "Type";
            airplaneTypecb.ValueMember = "Airplane_Id";
            con.Close();
        }

        private void vieworigincb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            viewcode.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            vieworigincb.SelectedValue = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            viewdestcb.SelectedValue = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            viewFdate.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            viewstop.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            airplaneTypecb.SelectedValue = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void viewdestcb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void viewFdate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
