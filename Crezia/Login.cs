using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crezia
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                this.Hide();
                Form1 form = new Form1();
                form.ShowDialog();
            }
           else if (textBox1.Text != "admin" && textBox2.Text == "admin")
            {
                MessageBox.Show("Username is incorrect");
            }
            else if (textBox1.Text == "admin" && textBox2.Text != "admin")
            {
                MessageBox.Show("Password is incorrect");
            }
            else 
            {
                MessageBox.Show("Username or Password is incorrect");
            }
        }
    }
}
