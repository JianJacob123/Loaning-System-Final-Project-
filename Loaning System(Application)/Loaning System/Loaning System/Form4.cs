using MySql.Data.MySqlClient;
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

namespace Loaning_System
{
    public partial class Form4 : Form
    {
        public static Form4? instance;
        public TextBox displayusername;
        public TextBox displayuserid;
        string mysqlCon = "server=127.0.0.1; user=root; database=loaning system; password=;"; //database server
        public Form4()
        {
            InitializeComponent();
            instance = this;
            displayusername = dispusername;
            displayuserid = dispuserid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Declare Variables

            int userid;
            double annual;
            string creditor;
            decimal amount; 
            int years;
            decimal total;
            decimal interest;
            decimal monthstopay;
            decimal total2;

            // Getting User Input
            userid = int.Parse(useridinput.Text);
            creditor = creditorinput.Text;
            annual = double.Parse(incomeinput.Text);
            amount = decimal.Parse(amountinput.Text);
            years = int.Parse(yearstopayinput.Text);

            
                if (annual >= 100000 && amount <= 2000000) //Requirements
                {
                    // Getting info from Database
                    MySqlConnection con = new MySqlConnection(mysqlCon);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT creditor_id, loan_type, interest FROM creditors WHERE company_name = '" + creditor + "'", con); //Select statement
                    MySqlDataReader reader1;
                    reader1 = cmd.ExecuteReader();
                    if (reader1.Read())
                    {
                        if (yearstopayinput.SelectedIndex == 0) // Preffered years to pay condition
                        {
                            useridoutput.Text = userid.ToString();
                            creditoroutput.Text = reader1["creditor_id"].ToString(); // Confirmation, computation and Displaying Output
                            purposeoutput.Text = reader1["loan_type"].ToString();
                            interestoutput.Text = reader1["interest"].ToString();
                            interest = amount * decimal.Parse(interestoutput.Text) / 12;
                            monthstopay = amount / 36;
                            total = Math.Round(interest, 2, MidpointRounding.ToEven) + Math.Round(monthstopay, 2, MidpointRounding.ToEven);
                            total2 = total * 36;
                            amountoutput.Text = Math.Round(total2, 2).ToString();
                            monthlypaymentoutput.Text = Math.Round(total, 2).ToString();
                            DateTime dateapplied = DateTime.Now;
                            DateTime duedate = dateapplied.AddYears(3);
                            dateappliedoutput.Text = dateapplied.ToString("yyyy-MM-dd");
                            duedateoutput.Text = duedate.ToString("yyyy-MM-dd");

                        }
                        else if (yearstopayinput.SelectedIndex == 1)
                        {
                            creditoroutput.Text = reader1["creditor_id"].ToString(); //Confirmation, computation and Displaying Output
                            purposeoutput.Text = reader1["loan_type"].ToString();
                            interestoutput.Text = reader1["interest"].ToString();
                            interest = amount * decimal.Parse(interestoutput.Text) / 12;
                            monthstopay = amount / 60;
                            total = Math.Round(interest, 2, MidpointRounding.ToEven) + Math.Round(monthstopay, 2, MidpointRounding.ToEven);
                            total2 = total * 60;
                            amountoutput.Text = Math.Round(total2, 2).ToString();
                            monthlypaymentoutput.Text = Math.Round(total, 2).ToString();
                            DateTime dateapplied = DateTime.Now;
                            DateTime duedate = dateapplied.AddYears(5);
                            dateappliedoutput.Text = dateapplied.ToString("yyyy-MM-dd");
                            duedateoutput.Text = duedate.ToString("yyyy-MM-dd");
                        }


                    }
                    else
                    {
                        MessageBox.Show("No Data Found");
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Does not meet the requirements for a loan");
                }
            

            






        }

        private void button2_Click(object sender, EventArgs e)
        {
            useridinput.Text = ""; //For clearing
            creditorinput.Text = "";
            incomeinput.Text = "";
            amountinput.Text = "";
            yearstopayinput.Text = "";
            useridoutput.Text = "---";
            creditoroutput.Text = "---";
            purposeoutput.Text = "---";
            interestoutput.Text = "---";
            amountoutput.Text = "---";
            dateappliedoutput.Text = "---";
            duedateoutput.Text = "---";
            monthlypaymentoutput.Text = "0.00";
            cardnuminput.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3(); // exit and returning to userdashboard
            f.Show();
            this.Hide();
            Form3.instance.displayusername.Text = dispusername.Text;
            Form3.instance.displayuserid.Text = dispuserid.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Inserting data to database
                string insertdata = " insert into debt (user_id, creditor_id, amount, monthly_payment, debt_date, due_date, is_active) VALUES ('" + useridoutput.Text + "','"
                    + creditoroutput.Text + "','" + amountoutput.Text + "','" + monthlypaymentoutput.Text + "','" + dateappliedoutput.Text + "','" + duedateoutput.Text + "', true)";

                MySqlConnection mySqlconnection = new MySqlConnection(mysqlCon);
                mySqlconnection.Open();
                MySqlCommand command = new MySqlCommand(insertdata, mySqlconnection);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Loan application is Successful. Funds have been added to your card");
                    Form3 f = new Form3();
                    f.Show();
                    this.Hide();
                    Form3.instance.displayusername.Text = dispusername.Text;
                    Form3.instance.displayuserid.Text = dispuserid.Text;
                }
                else
                {
                    MessageBox.Show("Loan Application Unsuccessful. Please Try Again");
                }

                mySqlconnection.Close();
            }
            catch
            {
                MessageBox.Show("Error. Please input information being asked");
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }
    }
}
