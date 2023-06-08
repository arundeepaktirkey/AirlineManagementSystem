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
    public partial class ViewAirplane : Form
    {
        public ViewAirplane()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");
        private void populate()
        {
            MySqlCommand cmd = new MySqlCommand("spDisplayAirplane", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (old_manu.Text == ""  || old_model.Text == "" || new_manuf.Text == "" || new_model.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    // Convert.ToInt32(asd.Text) for converting to int
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spUpdateAirplane", con);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("old_Manufacturing", old_manu.Text);
                    cmd.Parameters.AddWithValue("old_Model_number", old_model.Text);
                    cmd.Parameters.AddWithValue("new_Manufacturing", new_manuf.Text);
                    cmd.Parameters.AddWithValue("new_Model_number", new_model.Text);
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0)
                    {
                        MessageBox.Show("Airplane Updated Successfully");
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

        private void button3_Click(object sender, EventArgs e)
        {
            /* delete button */
            if (AirplaneIdcb.Text == "Select Airplane_Id")
            {
                MessageBox.Show("Enter the Airplane_Id to Delete");
            }
            else
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spDeleteAirplane", con);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Airplane_Id", Convert.ToInt32(AirplaneIdcb.SelectedValue.ToString()));
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0)
                    {
                        MessageBox.Show("Airplane Deleted Successfully");
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

        private void button1_Click(object sender, EventArgs e)
        {
            AirplaneIdcb.Text = "Select Airplane_Id";
            old_manu.Text = "";
            old_model.Text = "";
            new_manuf.Text = "";
            new_model.Text = "";
            searchtext.Text = "Type Airplane_Id";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecordAirplane recairplane = new RecordAirplane();
            recairplane.Show();
            this.Hide();
        }

        private void ViewAirplane_Load(object sender, EventArgs e)
        {
            populate(); 
            AirplaneIdcb.Text = "Select Airplane_Id";
            old_manu.Text = "";
            old_model.Text = "";
            new_manuf.Text = "";
            new_model.Text = "";
            searchtext.Text = "Type Airplane_Id";

            MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");
            string query = "select * from airplane";
            con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AirplaneIdcb.DataSource = ds.Tables[0];
            AirplaneIdcb.DisplayMember = "Type";
            AirplaneIdcb.ValueMember = "Airplane_Id";
            AirplaneIdcb.Text = "Select Airplane_Id";
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
/*            MessageBox.Show("Airline Id", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            MessageBox.Show("old manu", dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            MessageBox.Show("old model", dataGridView1.SelectedRows[0].Cells[2].Value.ToString());*/
            AirplaneIdcb.SelectedValue = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            old_manu.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            old_model.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void Searchbutton_Click(object sender, EventArgs e)
        {
            // search button
            if (Convert.ToInt32(searchtext.Text) != null)
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("spDisplayEligiblePilot", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Airplane_Id", Convert.ToInt32(searchtext.Text));
                DataSet ds = new DataSet();
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
            else
            {
                MessageBox.Show("Entered wrong input");
            }
        }
        

        private void AirplaneIdcb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
