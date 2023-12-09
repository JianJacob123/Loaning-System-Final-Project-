using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loaning_System
{
    public partial class Form3 : Form
    {
        public static Form3? instance; //transferred data from other forms
        public TextBox displayusername;
        public TextBox displayuserid;
        public Form3()
        {
            InitializeComponent();
            instance = this;
            displayusername = dispusername; //displaying username and userid
            displayuserid= dispuserid;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logged out"); //logging out and return to login dashboard
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(); //transfer to loan application dashboard
            f.Show();
            this.Hide();
            Form4.instance.displayusername.Text = dispusername.Text;
            Form4.instance.displayuserid.Text = dispuserid.Text;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5(); //transfer to loan payment dashboard
            f.Show();
            this.Hide();
            Form5.instance.displayusername.Text = dispusername.Text;
            Form5.instance.displayuserid.Text = dispuserid.Text;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6(); //transfer to loan payment dashboard
            f.Show();
            this.Hide();
            Form6.instance.displayusername.Text = dispusername.Text;
            Form6.instance.displayuserid.Text = dispuserid.Text;
        }
    }
}
