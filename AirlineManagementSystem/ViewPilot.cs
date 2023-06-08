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
    public partial class ViewPilot : Form
    {
        public ViewPilot()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");

        private void populate()
        {
            MySqlCommand cmd = new MySqlCommand("spDisplayPilot", con);
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
            if (empid.Text == "" || rating.Text == "" || airplane_typecb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    // Convert.ToInt32(asd.Text) for converting to int
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spUpdatePilot", con);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Emp_Id", empid.Text);
                    cmd.Parameters.AddWithValue("Rating", Convert.ToInt32(rating.Text));
                    cmd.Parameters.AddWithValue("Airplane_Type", airplane_typecb.SelectedValue.ToString());
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0)
                    {
                        MessageBox.Show("Pilot Updated Successfully");
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
            if (empid.Text == "")
            {
                MessageBox.Show("Enter the Emp_Id to Delete");
            }
            else
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spDeletePilot", con);
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Emp_Id", empid.Text);
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0)
                    {
                        MessageBox.Show("Pilot Deleted Successfully");
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
            empid.Text = "";
            rating.Text = "";
            airplane_typecb.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecordPilot pilot= new RecordPilot();
            pilot.Show();
            this.Hide();    
        }

        private void ViewPilot_Load(object sender, EventArgs e)
        {
            populate();

            MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");
            string query = "select * from airplane";
            con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            airplane_typecb.DataSource = ds.Tables[0];
            airplane_typecb.DisplayMember = "Type";
            airplane_typecb.ValueMember = "Airplane_Id";
            con.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            empid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            rating.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            airplane_typecb.SelectedValue = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}
