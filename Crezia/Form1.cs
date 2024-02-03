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
    public partial class Form1 : Form
    {
        private static string connectionString = "Data Source=DESKTOP-2DKQGSL\\SQLEXPRESS;Initial Catalog=crezia;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            displayoverall();
            StyleDataGridView(dataGridView1);
            StyleDataGridView(dataGridView2);
            displaytransaction();
            UpdateLabelWithTotalRecords();
            UpdateLabelWithAcademicFees();
            NumberOfTransations();
        }
        private void NumberOfTransations()
        {
            try
            {

                string query = "Select Count(*) From Transactions"; 

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand with the query and the connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and retrieve the total number of records
                        int totalRecords = (int)command.ExecuteScalar();

                        // Set the total number of records as the text of your label
                        label7.Text = totalRecords.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void UpdateLabelWithAcademicFees()
        {
            try
            {

                string query = "SELECT SUM(F.amount) AS total_fees_paid\r\nFROM Transactions AS T\r\nJOIN Fees AS F ON T.fee_id = F.fee_id;"; // Replace "YourTableName" with your actual table name

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand with the query and the connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and retrieve the total number of records
                        decimal totalRecords = (decimal)command.ExecuteScalar();

                        // Set the total number of records as the text of your label
                        label6.Text = totalRecords.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void UpdateLabelWithTotalRecords()
        {
            try
            {

                // Construct your SQL query to count the total number of records
                string query = "SELECT COUNT(*) FROM Student;"; // Replace "YourTableName" with your actual table name

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand with the query and the connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and retrieve the total number of records
                        int totalRecords = (int)command.ExecuteScalar();

                        // Set the total number of records as the text of your label
                        label5.Text=totalRecords.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void displaytransaction() {
           string query = @"SELECT 
    S.name AS student_name,
    SUM(F.amount) AS total_fee
FROM 
    Student S
JOIN 
    Transactions T ON S.student_id = T.student_id
JOIN 
    Fees F ON T.fee_id = F.fee_id
GROUP BY 
    S.name;
";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
            }
        }
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
        private void displayoverall() {
            string query = "SELECT \r\n    S.student_id,\r\n    S.name AS student_name,\r\n    S.year_level as 'Grade Level',\r\n    A.adviser_name,\r\n    SUM(F.amount) AS total_fee,\r\n    CASE\r\n        WHEN SUM(CASE WHEN T.amount_paid = 'Paid' THEN 1 ELSE 0 END) > 0 THEN 'Paid'\r\n        ELSE 'Not paid'\r\n    END AS payment_status\r\nFROM \r\n    Student S\r\nJOIN \r\n    Adviser A ON S.adviser_id = A.adviser_id\r\nJOIN \r\n    Transactions T ON S.student_id = T.student_id\r\nJOIN \r\n    Fees F ON T.fee_id = F.fee_id\r\nGROUP BY \r\n    S.student_id, S.name, S.year_level, A.adviser_name;";
                
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
          


        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            payment payment = new payment();
            payment.ShowDialog();
        }
    }
}
