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
    public partial class ViewStaff : Form
    {
        public ViewStaff()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");

        private void populate()
        {

            MySqlCommand cmd = new MySqlCommand("spDisplayEmployee", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecordStaff recstaff = new RecordStaff();
            recstaff.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fname.Text = "";
            Lname.Text = "";
            Address.Text = "";
            Phone.Text = "";
            Salary.Text = "";
            Positioncb.Text = "Select Position";
            empid.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /* delete button */
            if (Fname.Text == "" || Phone.Text == "")
            {
                MessageBox.Show("Enter the Name and Phone coulmn to Delete record");
            }
            else
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spDeleteEmployee", con);
                    cmd.Connection = con;
                    cmd.CommandType= CommandType.StoredProcedure;
                    if (Fname.Text == "") { MessageBox.Show("Enter First Name for deletion"); }
                    cmd.Parameters.AddWithValue("Name", Fname.Text);
                    if (Phone.Text == "") { MessageBox.Show("Enter Phone number for deletion"); }
                    cmd.Parameters.AddWithValue("Phone", Phone.Text);
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0) { MessageBox.Show("Staff Deleted Successfully"); }
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
            if (Fname.Text == "" || Lname.Text == "" || Phone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("spUpdateEmployee", con);
                    cmd.Connection= con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Name", Fname.Text);
                    cmd.Parameters.AddWithValue("Surname", Lname.Text);
                    cmd.Parameters.AddWithValue("Address", Address.Text);
                    cmd.Parameters.AddWithValue("Phone", Phone.Text);
                    cmd.Parameters.AddWithValue("Positon", Positioncb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("Salary", Salary.Text);
                    cmd.Parameters.AddWithValue("Emp_Id", empid.Text);
                    int j = cmd.ExecuteNonQuery();
                    if (j >= 0) { MessageBox.Show("Flight Updated Successfully"); }
                    con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Fname.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Lname.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            Address.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            Phone.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            Positioncb.SelectedValue = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            Salary.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            empid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

        }

        private void ViewStaff_Load(object sender, EventArgs e)
        {
            populate();

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
        }
    }
}
