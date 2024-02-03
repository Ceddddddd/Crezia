using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crezia
{
    public partial class Fees : Form
    {
        private static string connectionString = "Data Source=DESKTOP-2DKQGSL\\SQLEXPRESS;Initial Catalog=crezia;Integrated Security=True";
        public static void StyleDataGridView(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = Color.White;
            dataGridView.GridColor = Color.White;
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.White;

            // Set header style
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerStyle.BackColor = Color.White;
            headerStyle.ForeColor = Color.Black;
            dataGridView.ColumnHeadersDefaultCellStyle = headerStyle;

            // Set cell style
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle = cellStyle;
            dataGridView.RowTemplate.Height = 40;
            

        }
        public Fees()
        {
            InitializeComponent();
            displayoverall();
            StyleDataGridView(dataGridView1);
        }
        
        private void displayoverall()
        {
            string query = "Select * from Fees";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.Width =256;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            string fee_name = textBox2.Text;
            decimal amount = decimal.Parse(textBox3.Text);

            // Construct the SQL insert statement
            string insertStatement = $"INSERT INTO Fees (fee_id, fee_name, amount) VALUES ({id}, '{fee_name}', {amount});";

            // Replace the connection string with your actual database connection string

            // Create a SqlConnection using the connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a SqlCommand with the insert statement and the connection
                using (SqlCommand command = new SqlCommand(insertStatement, connection))
                {
                    // Execute the insert statement
                    int rowsAffected = command.ExecuteNonQuery();

                    // Optionally, check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        // Rows were affected (insert successful)
                        MessageBox.Show("Insert successful!");
                        displayoverall();
                        StyleDataGridView(dataGridView1);
                    }
                    else
                    {
                        // No rows were affected (insert failed)
                        MessageBox.Show("Insert failed!");
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row index is clicked
            {
                // Get the DataGridViewRow corresponding to the clicked cell
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Fill the textboxes with the values from the selected row
                textBox1.Text = row.Cells["fee_id"].Value.ToString(); // Replace "Column1_Name" with the actual name of the column for fee_id
                textBox2.Text = row.Cells["fee_name"].Value.ToString(); // Replace "Column2_Name" with the actual name of the column for fee_name
                textBox3.Text = row.Cells["amount"].Value.ToString(); // Replace "Column3_Name" with the actual name of the column for amount
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            string fee_name = textBox2.Text;
            decimal amount = decimal.Parse(textBox3.Text);

            string updateStatement = $"UPDATE Fees SET fee_name = '{fee_name}', amount = {amount} WHERE fee_id = {id};";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a SqlCommand with the insert statement and the connection
                using (SqlCommand command = new SqlCommand(updateStatement, connection))
                {
                    // Execute the insert statement
                    int rowsAffected = command.ExecuteNonQuery();

                    // Optionally, check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        // Rows were affected (insert successful)
                        MessageBox.Show("Update successful!");
                        displayoverall();
                        StyleDataGridView(dataGridView1);
                    }
                    else
                    {
                        // No rows were affected (insert failed)
                        MessageBox.Show("Insert failed!");
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            string deleteStatement = $"DELETE FROM Fees WHERE fee_id = {id};";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a SqlCommand with the insert statement and the connection
                using (SqlCommand command = new SqlCommand(deleteStatement, connection))
                {
                    // Execute the insert statement
                    int rowsAffected = command.ExecuteNonQuery();

                    // Optionally, check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        // Rows were affected (insert successful)
                        MessageBox.Show("Delete successful!");
                        displayoverall();
                        StyleDataGridView(dataGridView1);
                    }
                    else
                    {
                        // No rows were affected (insert failed)
                        MessageBox.Show("Delete failed!");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student student = new Student();
            student.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            payment payment = new payment();
            payment.ShowDialog();
        }
    }
}
