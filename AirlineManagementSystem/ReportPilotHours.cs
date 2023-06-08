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
    public partial class ReportPilotHours : Form
    {
        public ReportPilotHours()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=airline_management_system;port=3306;password=root");

        private void populate()
        {
            MySqlCommand cmd = new MySqlCommand("spReportPilotHours", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void ReportPilotHours_Load(object sender, EventArgs e)
        {
            populate();
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
    }
}
