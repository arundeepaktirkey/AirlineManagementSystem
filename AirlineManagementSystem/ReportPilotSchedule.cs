using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineManagementSystem
{
    public partial class ReportPilotSchedule : Form
    {
        public ReportPilotSchedule()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");

        private void populate()
        {
            MySqlCommand cmd = new MySqlCommand("spReportPilotScheduleNoInput", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
            this.Hide();
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
                        string radiobtn1 = "employee." + radioButton1.Text;
                        string sctfld = "'" + searchtext.Text + "'";

                        string query = "select monthname(flight.Date) as Month , date(flight.Date) as Date, employee.Emp_Id, flight.Flight_Code, employee.Name, employee.Surname, rating.Rating, employee.Address, employee.Phone From rating Inner join pilot on pilot.Rating_Id = rating.Rating_Id Inner join employee on employee.Emp_Id = pilot.Emp_Id Inner Join crew on crew.Emp_Id = employee.Emp_Id Inner Join flight on flight.Flight_Id = crew.Flight_Id where " + radiobtn1 + " = " + sctfld + "group by flight.Date,employee.Emp_Id,flight.Flight_Code, employee.Name, employee.Surname, employee.Address, employee.Phone, rating.Rating order by flight.Date";
                        con.Open();
                        MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];
                        con.Close();
                        break;

                    case "radioButton2":
                        string radiobtn2 = "flight." + radioButton2.Text;
                        string sctfld2 = "'" + searchtext.Text + "'";

                        string query2 = "select monthname(flight.Date) as Month , date(flight.Date) as Date, employee.Emp_Id, flight.Flight_Code, employee.Name, employee.Surname, rating.Rating, employee.Address, employee.Phone From rating Inner join pilot on pilot.Rating_Id = rating.Rating_Id Inner join employee on employee.Emp_Id = pilot.Emp_Id Inner Join crew on crew.Emp_Id = employee.Emp_Id Inner Join flight on flight.Flight_Id = crew.Flight_Id where " + radiobtn2 + " = " + sctfld2 + "group by flight.Date,employee.Emp_Id,flight.Flight_Code, employee.Name, employee.Surname, employee.Address, employee.Phone, rating.Rating order by flight.Date";
                        con.Open();
                        MySqlDataAdapter sda2 = new MySqlDataAdapter(query2, con);
                        MySqlCommandBuilder builder2 = new MySqlCommandBuilder(sda2);
                        var ds2 = new DataSet();
                        sda2.Fill(ds2);
                        dataGridView1.DataSource = ds2.Tables[0];
                        con.Close();
                        break;

                    case "radioButton3":
                        string radiobtn3 = "rating." + radioButton3.Text;
                        string sctfld3 = "'" + searchtext.Text + "'";

                        string query3 = "select monthname(flight.Date) as Month , date(flight.Date) as Date, employee.Emp_Id, flight.Flight_Code, employee.Name, employee.Surname, rating.Rating, employee.Address, employee.Phone From rating Inner join pilot on pilot.Rating_Id = rating.Rating_Id Inner join employee on employee.Emp_Id = pilot.Emp_Id Inner Join crew on crew.Emp_Id = employee.Emp_Id Inner Join flight on flight.Flight_Id = crew.Flight_Id where " + radiobtn3 + " = " + sctfld3 + "group by flight.Date,employee.Emp_Id,flight.Flight_Code, employee.Name, employee.Surname, employee.Address, employee.Phone, rating.Rating order by flight.Date";
                        con.Open();
                        MySqlDataAdapter sda3 = new MySqlDataAdapter(query3, con);
                        MySqlCommandBuilder builder3 = new MySqlCommandBuilder(sda3);
                        var ds3 = new DataSet();
                        sda3.Fill(ds3);
                        dataGridView1.DataSource = ds3.Tables[0];
                        con.Close();
                        break;

                }
            }
        }

        private void ReportPilotSchedule_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
