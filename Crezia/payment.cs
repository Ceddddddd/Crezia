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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Crezia
{
    public partial class payment : Form
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
        public payment()
        {
            InitializeComponent();
            display1();
            display2();
            display3();
            StyleDataGridView(dataGridView1);
            StyleDataGridView(dataGridView2);
            StyleDataGridView(dataGridView3);
        }
        private void display1() {
            string query = "Select * from Transactions";

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
        private void display2()
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
                    column.Width = 171;
                }

            }
        }
        private void display3()
        {
            string query = "Select * from Student";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                dataGridView3.DataSource = dataTable;
                foreach (DataGridViewColumn column in dataGridView3.Columns)
                {
                    column.Width = 99;
                }

            }
        }
        private void dashboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string amount_paid = checkBox1.Checked ? "Paid" : "Not paid"; // Handle checkbox state directly// Retrieve values from textboxes
            int transaction_id = int.Parse(textBox1.Text);
            int student_id = int.Parse(textBox2.Text);
            int fee_id = int.Parse(textBox3.Text);
            DateTime dateTime = DateTime.Now;
            string formattedDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            // Construct the SQL insert statement
            string insertStatement = $"INSERT INTO Transactions (transaction_id, student_id, fee_id, transaction_date, amount_paid) " +
                                      $"VALUES ({transaction_id}, {student_id}, {fee_id}, '{ formattedDateTime}', '{amount_paid}');";

            try
            {
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
                            MessageBox.Show("Transaction record inserted successfully!");
                            display1();
                            display2();
                            display3();
                            StyleDataGridView(dataGridView1);
                            StyleDataGridView(dataGridView2);
                            StyleDataGridView(dataGridView3);
                        }
                        else
                        {
                            // Insert failed
                            MessageBox.Show("Failed to insert transaction record!");
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
        public static string amount_paid;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row index is clicked
            {
                // Get the DataGridViewRow corresponding to the clicked cell
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                // Fill the textboxes with the values from the selected row
                textBox1.Text = row.Cells["transaction_id"].Value.ToString(); // Replace "Column1_Name" with the actual name of the column for fee_id
                textBox2.Text = row.Cells["student_id"].Value.ToString(); // Replace "Column2_Name" with the actual name of the column for fee_name
                textBox3.Text = row.Cells["fee_id"].Value.ToString(); // Replace "Column3_Name" with the actual name of the column for amount
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Payment is marked as Paid
                amount_paid = "Paid";
            }
            else
            {
                // Payment is marked as Not paid
                amount_paid = "Not paid";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int transaction_id = int.Parse(textBox1.Text);
            int student_id = int.Parse(textBox2.Text);
            int fee_id = int.Parse(textBox3.Text);
            try
            {
                // Construct the SQL update statement
                string updateStatement = $"UPDATE Transactions SET student_id = {student_id}, fee_id = {fee_id}, amount_paid = '{amount_paid}' WHERE transaction_id = {transaction_id};";
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
                            MessageBox.Show("Transaction record updated successfully!");
                            display1();
                            display2();
                            display3();
                            StyleDataGridView(dataGridView1);
                            StyleDataGridView(dataGridView2);
                            StyleDataGridView(dataGridView3);
                        }
                        else
                        {
                            // Update failed
                            MessageBox.Show("Failed to update transaction record! Transaction ID not found.");
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
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int transaction_id = int.Parse(textBox1.Text);
            try
            {
                // Construct the SQL delete statement
                string deleteStatement = $"DELETE FROM Transactions WHERE transaction_id = {transaction_id};";


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
                            MessageBox.Show("Transaction record deleted successfully!");
                            display1();
                            display2();
                            display3();
                            StyleDataGridView(dataGridView1);
                            StyleDataGridView(dataGridView2);
                            StyleDataGridView(dataGridView3);
                        }
                        else
                        {
                            // Delete failed
                            MessageBox.Show("Failed to delete transaction record! Transaction ID not found.");
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
            this.Hide();
            Student student = new Student();
            student.ShowDialog();
        }
    }
}
