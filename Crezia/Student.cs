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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace Crezia
{
    public partial class Student : Form
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
        public Student()
        {
            InitializeComponent();
            display1();
            display2();
            StyleDataGridView(dataGridView1);
            StyleDataGridView(dataGridView2);
        }
        private void display2() {
            string query = "Select adviser_id,adviser_name from Adviser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    column.Width = 100;
                }

            }
        }
        private void display1() {
           
                string query = "Select * from Student";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.Width = 100;
                    }
            
                }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from textboxes
                int student_id = int.Parse(textBox1.Text);
                string name = textBox2.Text;
                string gender = textBox3.Text;
                int year_level = int.Parse(textBox6.Text);
                int adviser_id = int.Parse(textBox5.Text);
                string email = textBox4.Text;

                // Construct the SQL insert statement
                string insertStatement = $"INSERT INTO Student (student_id, name, gender, year_level, adviser_id, contact_details) VALUES ({student_id}, '{name}', '{gender}', {year_level}, {adviser_id}, '{email}');";

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

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            // Insert successful
                            MessageBox.Show("Student record inserted successfully!");
                            display1();
                            display2();
                            StyleDataGridView(dataGridView1);
                            StyleDataGridView(dataGridView2);
                        }
                        else
                        {
                            // Insert failed
                            MessageBox.Show("Failed to insert student record!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0) // Ensure a valid row index is clicked
            {
                // Get the DataGridViewRow corresponding to the clicked cell
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Fill the textboxes with the values from the selected row
                textBox1.Text = row.Cells["student_id"].Value.ToString(); // Replace "Column1_Name" with the actual name of the column for fee_id
                textBox2.Text = row.Cells["name"].Value.ToString(); // Replace "Column2_Name" with the actual name of the column for fee_name
                textBox3.Text = row.Cells["gender"].Value.ToString(); // Replace "Column3_Name" with the actual name of the column for amount
                textBox4.Text = row.Cells["contact_details"].Value.ToString();
                textBox5.Text = row.Cells["adviser_id"].Value.ToString();
                textBox6.Text = row.Cells["year_level"].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int student_id = int.Parse(textBox1.Text);
            string name = textBox2.Text;
            string gender = textBox3.Text;
            int year_level = int.Parse(textBox6.Text);
            int adviser_id = int.Parse(textBox5.Text);
            string email = textBox4.Text;
            try
            {
                // Construct the SQL update statement
                string updateStatement = $"UPDATE Student SET name = '{name}', gender = '{gender}', year_level = {year_level}, adviser_id = {adviser_id}, contact_details = '{email}' WHERE student_id = {student_id};";

                // Create a SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand with the update statement and the connection
                    using (SqlCommand command = new SqlCommand(updateStatement, connection))
                    {
                        // Execute the update statement
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            // Update successful
                            MessageBox.Show("Student record updated successfully!");
                            display1();
                            display2();
                            StyleDataGridView(dataGridView1);
                            StyleDataGridView(dataGridView2);
                        }
                        else
                        {
                            // Update failed
                            MessageBox.Show("Failed to update student record! Student ID not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int student_id = int.Parse(textBox1.Text);
            try
            {
                // Construct the SQL delete statement
                string deleteStatement = $"DELETE FROM Student WHERE student_id = {student_id};";

                // Replace the connection string with your actual database connection string


                // Create a SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand with the delete statement and the connection
                    using (SqlCommand command = new SqlCommand(deleteStatement, connection))
                    {
                        // Execute the delete statement
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            // Delete successful
                            MessageBox.Show("Student record deleted successfully!");
                            display1();
                            display2();
                            StyleDataGridView(dataGridView1);
                            StyleDataGridView(dataGridView2);
                        }
                        else
                        {
                            // Delete failed
                            MessageBox.Show("Failed to delete student record! Student ID not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}");
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
            this.Hide();
            Fees fees = new Fees();
            fees.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            payment payment = new payment();
            payment.ShowDialog();
        }
    }
    }
    

