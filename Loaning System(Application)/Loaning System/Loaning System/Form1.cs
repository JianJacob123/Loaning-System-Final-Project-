using MySql.Data.MySqlClient;
namespace Loaning_System
{
    public partial class Form1 : Form
    {
        string mysqlCon = "server=127.0.0.1; user=root; database=loaning system; password=;"; //Database Server

        public Form1()
        {
            InitializeComponent();
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Declaration of Variables
            String createusername, createpassword, firstname, lastname, address, phone;


            //Getting User Input
            createusername = CreateUserInput.Text.ToString();  
            createpassword = CreatePassInput.Text.ToString();
            firstname = FirstNameInput.Text.ToString();
            lastname = LastNameInput.Text.ToString();
            address = AddressInput.Text.ToString();
            phone = PhoneInput.Text.ToString();

            // Inserting Data to Database
            string insertdata = " insert into user_account (user_username, user_password, first_name, last_name, address, phone_number) VALUES ('" + createusername + "','" + createpassword + "','" + firstname + "','" + lastname + "','" + address + "','" + phone + "')";
            
            MySqlConnection mySqlconnection = new MySqlConnection(mysqlCon);
            mySqlconnection.Open();
            MySqlCommand command = new MySqlCommand(insertdata, mySqlconnection);

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Successfully Registered");
                Form2 f = new Form2();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Registration Unsuccessful");
            }

            mySqlconnection.Close();
            



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}