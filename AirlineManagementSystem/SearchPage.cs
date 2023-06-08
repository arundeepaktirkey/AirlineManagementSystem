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
    public partial class SearchPage : Form
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewBooking viewBooking = new ViewBooking();
            viewBooking.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ViewAirplane viewplane = new ViewAirplane();
            viewplane.Show(); 
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
