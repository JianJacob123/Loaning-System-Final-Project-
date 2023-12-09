using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Loaning_System
{
    public partial class Form6 : Form
    {
        public static Form6 instance;
        public TextBox displayusername;
        public TextBox displayuserid;
        string mysqlCon = "server=127.0.0.1; user=root; database=loaning system; password=;"; //database server
        public Form6()
        {
            InitializeComponent();
            instance = this;
            displayusername = dispusername; //displaying username and userid
            displayuserid = dispuserid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userid = dispuserid.Text;
            if (optioninput.SelectedIndex == 0)
            {
                string searchQuery = "SELECT * FROM payment WHERE user_id = " + userid; //Getting data from database
                MySqlConnection connection = new MySqlConnection(mysqlCon);
                MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = table;

            }
            else
            {
                MessageBox.Show("No Such Data");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3(); //exit and return to user dashboard
            f.Show();
            this.Hide();
            Form3.instance.displayusername.Text = dispusername.Text;
            Form3.instance.displayuserid.Text = dispuserid.Text;
        }
    }
}
