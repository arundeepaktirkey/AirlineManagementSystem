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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportPilotSchedule pilotSchedule = new ReportPilotSchedule();
            pilotSchedule.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportAirjetServices reportAirjetServices = new ReportAirjetServices();
            reportAirjetServices.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportPilotHours reportPilotHours = new ReportPilotHours();
            reportPilotHours.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReportPassengers reportPassengers = new ReportPassengers();
            reportPassengers.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
