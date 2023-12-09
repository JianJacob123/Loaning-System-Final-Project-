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
using Org.BouncyCastle.Bcpg;

namespace Loaning_System
{
    public partial class Form2 : Form
    {

        public static Form2? instance;
        string mysqlCon = "server=127.0.0.1; user=root; database=loaning system; password=;"; //Database Server
        public Form2()
        {
            InitializeComponent();
            MySqlConnection mysqlConnection = new MySqlConnection(mysqlCon);

            try
            {
                mysqlConnection.Open();
                MessageBox.Show("Connection Success");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mysqlConnection.Close();
            }
            instance = this;


        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            string login_username, login_password;  // Declare Variables

            login_username = loginuserinput.Text.ToString(); // Getting UserInput
            login_password = loginpassinput.Text.ToString();

            try
            {
                // Validating Credentials
                string validate = " SELECT * FROM user_account WHERE user_username = '" + loginuserinput.Text + "' AND user_password = '" + loginpassinput.Text + "'";
                MySqlConnection connection = new MySqlConnection(mysqlCon);
                MySqlDataAdapter sda = new MySqlDataAdapter(validate, connection);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    MessageBox.Show("Successfully Logged In");
                    Form3 f = new Form3();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password");
                }

                connection.Close();

                //Retrieving userid and username  and use for transferring data to other forms
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT user_id FROM user_account WHERE user_username = '" + loginuserinput.Text + "'", connection);
                MySqlDataReader reader1;
                reader1 = cmd.ExecuteReader();
                if (reader1.Read())
                {
                    string? userid = reader1["user_id"].ToString();
                    Form3.instance.displayusername.Text = loginuserinput.Text;
                    Form3.instance.displayuserid.Text = userid?.ToString();
                }
                else
                {
                    MessageBox.Show("Userid Not Found");
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Please input information being asked");
            }

            
                

           
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Register_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
