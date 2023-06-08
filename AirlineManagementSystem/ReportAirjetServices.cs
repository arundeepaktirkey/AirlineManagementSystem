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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using RadioButton = System.Windows.Forms.RadioButton;

namespace AirlineManagementSystem
{
    public partial class ReportAirjetServices : Form
    {
        public ReportAirjetServices()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");

        private void populate()
        {
            MySqlCommand cmd = new MySqlCommand("spReportServices", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void ReportAirjetServices_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
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
                        string radiobtn1 = "airport." + radioButton1.Text;
                        string sctfld1 = "'" + searchtext.Text + "'";

                        string query = "select  airport.City,  airport.Country, flight.Flight_Code, CAST(flight.Date AS DATE) AS Flight_Date From airport Inner join flight on flight.Origin = airport.Airport_Id where " + radiobtn1 + " = " + sctfld1 + " group by airport.City, flight.Flight_Code, airport.Country, flight.Date\r\norder by airport.City;";
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

                        string query2 = "select  airport.City,  airport.Country, flight.Flight_Code, CAST(flight.Date AS DATE) AS Flight_Date\r\nFrom airport\r\nInner join\r\nflight on flight.Origin = airport.Airport_Id\r\nwhere " + radiobtn2 + " = " + sctfld2 + " group by airport.City, flight.Flight_Code, airport.Country, flight.Date\r\norder by airport.City;";
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
