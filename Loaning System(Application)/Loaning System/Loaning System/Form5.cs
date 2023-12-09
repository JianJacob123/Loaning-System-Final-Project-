using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace Loaning_System
{
    public partial class Form5 : Form
    {
        public static Form5 instance;
        public TextBox displayusername;
        public TextBox displayuserid;
        string mysqlCon = "server=127.0.0.1; user=root; database=loaning system; password=;"; //database server
        public Form5()
        {
            InitializeComponent();
            instance = this;
            displayusername = dispusername; //displaying username and userid
            displayuserid = dispuserid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = dispusername.Text;
            //Displaying active and settled loans for user
            if (optioninput.SelectedIndex == 0)
            {
                string searchQuery = "SELECT debt_id, amount, first_name, last_name, company_name, loan_type, monthly_payment, debt_date, due_date FROM debt inner join user_account on user_account.user_id = debt.user_id inner join creditors on creditors.creditor_id = debt.debt_id WHERE user_username = '" + username + "'" + "AND is_active = true";
                MySqlConnection connection = new MySqlConnection(mysqlCon);
                MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = table;
                
            }
            else if (optioninput.SelectedIndex == 1)
            {
                string searchQuery = "SELECT debt_id, first_name, last_name, company_name, loan_type, monthly_payment, debt_date, due_date FROM debt inner join user_account on user_account.user_id = debt.user_id inner join creditors on creditors.creditor_id = debt.debt_id WHERE user_username = '" + username + "'" + "AND is_active = false";
                MySqlConnection connection = new MySqlConnection(mysqlCon);
                MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string username = dispusername.Text;
            if (optioninput.SelectedIndex == 0) // For displaying information
            {
                string Query = "SELECT debt_id, first_name, last_name, company_name, loan_type, monthly_payment, debt_date, due_date FROM debt inner join user_account on user_account.user_id = debt.user_id inner join creditors on creditors.creditor_id = debt.debt_id WHERE user_username = '" + username + "'" + "AND is_active = true";
                MySqlConnection connection = new MySqlConnection(mysqlCon);
                MySqlDataAdapter adapter = new MySqlDataAdapter(Query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                foreach (DataRow dr in table.Rows)
                {
                    debtidinput.Text = dr["debt_id"].ToString();
                    valueinput.Text = dr["monthly_payment"].ToString();
                }
                DateTime dateapplied = DateTime.Now;
                datepaidinput.Text = dateapplied.ToString("yyyy-MM-dd");

            }
            else if (optioninput.SelectedIndex == 1)
            {
                MessageBox.Show("Already Settled");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Declaring Variables
            string userid = dispuserid.Text;
            string cardname;
            string cardnumber;
            string pin;

            //Getting User Input
            cardname = cardnameinput.Text;
            cardnumber = cardnuminput.Text; 
            pin = PINinput.Text;

                MySqlConnection con = new MySqlConnection(mysqlCon);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT amount, monthly_payment FROM debt WHERE user_id = '" + userid + "'", con); //Getting the amount
                MySqlDataReader reader1;
                reader1 = cmd.ExecuteReader();
                if (reader1.Read())
                {
                    // hidden labels to be used in computation
                    totalamountlabel.Text = reader1["amount"].ToString();
                    monthlylabel.Text = reader1["monthly_payment"].ToString();
                }
                else
                {
                    MessageBox.Show("No Data Found");
                }
                con.Close();

                if (double.Parse(totalamountlabel.Text) != double.Parse(monthlylabel.Text) && cardname != "" && cardnumber != "" && pin != "")
                {
                    string insertdata = " insert into payment (debt_id, value, method, payment_date, user_id) VALUES ('" + debtidinput.Text + "','" // inserting data
                    + valueinput.Text + "','" + "Debit Card" + "','" + datepaidinput.Text + "','" + userid + "')";

                    MySqlConnection mySqlconnection = new MySqlConnection(mysqlCon);
                    mySqlconnection.Open();
                    MySqlCommand command = new MySqlCommand(insertdata, mySqlconnection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Payment Successful!");

                        decimal value = decimal.Parse(valueinput.Text);
                        decimal newtotal = decimal.Parse(totalamountlabel.Text) - value;
                        string updatedata = "UPDATE debt SET amount = '" + newtotal + "' WHERE user_id = '" + userid + "'"; // computation to update the new amount
                        MySqlCommand command2 = new MySqlCommand(updatedata, mySqlconnection);
                        command2.ExecuteNonQuery();

                        Form3 f = new Form3();
                        f.Show();
                        this.Hide();
                        Form3.instance.displayusername.Text = dispusername.Text;
                        Form3.instance.displayuserid.Text = dispuserid.Text;

                    }
                    else
                    {
                        MessageBox.Show("Payment Unsuccessful. Please Try Again.");
                    }
                    mySqlconnection.Close();
                }
                else if (double.Parse(totalamountlabel.Text) == double.Parse(monthlylabel.Text))
                {
                    string insertdata = " insert into payment (debt_id, value, method, payment_date) VALUES ('" + debtidinput.Text + "','" // inserting data
                   + valueinput.Text + "','" + "Debit Card" + "','" + datepaidinput.Text + "')";

                    MySqlConnection mySqlconnection = new MySqlConnection(mysqlCon);
                    mySqlconnection.Open();
                    MySqlCommand command = new MySqlCommand(insertdata, mySqlconnection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Payment Successful!");

                        decimal value = decimal.Parse(valueinput.Text);
                        decimal newtotal = decimal.Parse(totalamountlabel.Text) - value;
                        string updatedata = "UPDATE debt SET amount = '" + newtotal + "' WHERE user_id = '" + userid + "'"; // computation to update the new amount
                        MySqlCommand command2 = new MySqlCommand(updatedata, mySqlconnection);
                        command2.ExecuteNonQuery();
                        string updatedata2 = "UPDATE debt SET is_active = false WHERE user_id = '" + userid + "'";
                        MySqlCommand command3 = new MySqlCommand(updatedata2, mySqlconnection);
                        command3.ExecuteNonQuery();
                        Form3 f = new Form3();
                        f.Show();
                        this.Hide();
                        Form3.instance.displayusername.Text = dispusername.Text;
                        Form3.instance.displayuserid.Text = dispuserid.Text;

                    }
                    else
                    {
                        MessageBox.Show("Payment Unsuccessful. Please Try Again.");
                    }
                    mySqlconnection.Close();
                }

            else
            {
                MessageBox.Show("Payment Unsuccessful. Please check if credentials is correct");
            }

        }

             
    }
}
    



