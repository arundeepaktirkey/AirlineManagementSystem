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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RecordBooking recbooking = new RecordBooking();
            recbooking.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RecordFlight recflight = new RecordFlight();
            recflight.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RecordStaff recstaff =new RecordStaff();
            recstaff.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecordAirplane recairplane = new RecordAirplane();
            recairplane.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SearchPage searchpage = new SearchPage();
            searchpage.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Report rpt = new Report();
            rpt.Show();
            this.Hide();
        }
    }
}
