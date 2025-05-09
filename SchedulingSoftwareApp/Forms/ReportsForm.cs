using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Forms
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            LoadAppointmentTypesByMonth();
            LoadUserSchedules();
            LoadCustomerAppointmentSummary();
        }

        private void LoadAppointmentTypesByMonth()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = @"
                SELECT 
                    DATE_FORMAT(start, '%Y-%m') AS 'Month', 
                    type AS 'Appointment Type', 
                    COUNT(*) AS 'Total Appointments'
                FROM appointment
                GROUP BY DATE_FORMAT(start, '%Y-%m'), type
                ORDER BY DATE_FORMAT(start, '%Y-%m'), type";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    DataTable typesByMonthTable = new DataTable();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        typesByMonthTable.Load(reader);
                    }

                    dgvTypesByMonth.DataSource = typesByMonthTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment types by month: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserSchedules()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = @"
                        SELECT 
                            u.userName AS 'User',
                            a.title AS 'Title',
                            a.start AS 'Start Time',
                            a.end AS 'End Time',
                            c.customerName AS 'Customer'
                        FROM appointment a
                        JOIN user u ON a.userId = u.userId
                        JOIN customer c ON a.customerId = c.customerId
                        ORDER BY u.userName, a.start";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    DataTable userSchedulesTable = new DataTable();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        userSchedulesTable.Load(reader);
                    }

                    dgvUserSchedules.DataSource = userSchedulesTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user schedules: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerAppointmentSummary()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = @"
                        SELECT 
                            c.customerName AS 'Customer',
                            COUNT(a.appointmentId) AS 'Total Appointments',
                            MIN(a.start) AS 'First Appointment',
                            MAX(a.end) AS 'Last Appointment'
                        FROM appointment a
                        JOIN customer c ON a.customerId = c.customerId
                        GROUP BY c.customerName
                        ORDER BY c.customerName";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    DataTable customerSummaryTable = new DataTable();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        customerSummaryTable.Load(reader);
                    }

                    dgvCustomerSummary.DataSource = customerSummaryTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer appointment summary: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Close the ReportsForm and open the AppointmentForm
            AppointmentForm appointmentForm = new AppointmentForm();
            appointmentForm.Show();
            //this.Close();
        }
    }
}
