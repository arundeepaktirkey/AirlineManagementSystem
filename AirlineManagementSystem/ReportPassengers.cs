using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
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
    public partial class ReportPassengers : Form
    {
        public ReportPassengers()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");

        private void populate()
        {
            MySqlCommand cmd = new MySqlCommand("spReportPassengers", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
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

                        string sctfld1 = "'" +searchtext.Text +"'";
                        string radiobtn1 = "flight." + radioButton1.Text;

                        string query = @"select flight.Flight_Code, count(flight.Flight_Code) as No_Of_Passengers, (select City from airport where Airport_Id = flight.Origin) as Origin, (select City from airport where Airport_Id = flight.Destination) as Destination, flight.NoOfStops as Stops, flight.Date
                                            From passenger
                                            Inner join
                                                ticket on passenger.Passenger_Id = ticket.Ticket_Id
                                                    inner join
                                                        flight on flight.Flight_Id = ticket.Flight_Id
                                            where " +radiobtn1 + @" = (select Airport_Id From airport where airport.City = " +sctfld1 +@")
                                            group by flight.Flight_Code, flight.Origin, flight.Destination, flight.NoOfStops, flight.Date
                                            order by count(flight.Flight_Code) desc;";

                        con.Open();
                        MySqlDataAdapter sda = new MySqlDataAdapter(query, con);
                        MySqlCommandBuilder builder = new MySqlCommandBuilder(sda);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0];
                        con.Close();
                        break;

                    case "radioButton2":

                        string sctfld2 = "'" + searchtext.Text + "'";
                        string radiobtn2 = "flight." + radioButton2.Text;

                        string query2 = @"select flight.Flight_Code, count(flight.Flight_Code) as No_Of_Passengers, (select City from airport where Airport_Id = flight.Origin) as Origin, (select City from airport where Airport_Id = flight.Destination) as Destination, flight.NoOfStops as Stops, flight.Date
                                            From passenger
                                            Inner join
                                                ticket on passenger.Passenger_Id = ticket.Ticket_Id
                                                    inner join
                                                        flight on flight.Flight_Id = ticket.Flight_Id
                                            where " + radiobtn2 + @" = (select Airport_Id From airport where airport.City = " + sctfld2 + @")
                                            group by flight.Flight_Code, flight.Origin, flight.Destination, flight.NoOfStops, flight.Date
                                            order by count(flight.Flight_Code) desc;";

                        con.Open();
                        MySqlDataAdapter sda2 = new MySqlDataAdapter(query2, con);
                        MySqlCommandBuilder builder2 = new MySqlCommandBuilder(sda2);
                        var ds2 = new DataSet();
                        sda2.Fill(ds2);
                        dataGridView1.DataSource = ds2.Tables[0];
                        con.Close();
                        break;

                    case "radioButton3":
                        string sctfld3 = "'" + searchtext.Text + "'";
                        string radiobtn3 = "flight." + radioButton3.Text;

                        string query3 = @"select flight.Flight_Code, count(flight.Flight_Code) as No_Of_Passengers, (select City from airport where Airport_Id = flight.Origin) as Origin, (select City from airport where Airport_Id = flight.Destination) as Destination, flight.NoOfStops as Stops, flight.Date
                                    From passenger
                                    Inner join
                                    ticket on passenger.Passenger_Id = ticket.Ticket_Id
                                    inner join
                                    flight on flight.Flight_Id = ticket.Flight_Id
                                    where " + radiobtn3 + @" = " + sctfld3 + @"
                                    group by flight.Flight_Code, flight.Origin, flight.Destination, flight.NoOfStops, flight.Date
                                    order by count(flight.Flight_Code) desc"; 
                        
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

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Report report   = new Report();
            report.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ReportPassengers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
