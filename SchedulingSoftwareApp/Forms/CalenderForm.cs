using MySql.Data.MySqlClient;
using SchedulingSoftwareApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Forms
{
    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {
            InitializeComponent();
            LoadAppointmentsForSelectedDate(DateTime.Now);
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadAppointmentsForSelectedDate(e.Start);
        }

        private void LoadAppointmentsForSelectedDate(DateTime selectedDate)
        {
            try
            {
                // Create a new list to hold appointments
                List<Appointment> appointments = new List<Appointment>();

                // Use a fresh connection each time to avoid the "connection is already open" error
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    // Fetch all appointments
                    string query = "SELECT * FROM appointment";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentId = reader.GetInt32("appointmentId"),
                                CustomerId = reader.GetInt32("customerId"),
                                Title = reader.GetString("title"),
                                Description = reader.GetString("description"),
                                Location = reader.GetString("location"),
                                Contact = reader.GetString("contact"),
                                Type = reader.GetString("type"),
                                Url = reader.GetString("url"),
                                Start = reader.GetDateTime("start"),
                                End = reader.GetDateTime("end"),
                                CreateDate = reader.GetDateTime("createDate"),
                                CreatedBy = reader.GetString("createdBy"),
                                LastUpdate = reader.GetDateTime("lastUpdate"),
                                LastUpdateBy = reader.GetString("lastUpdateBy")
                            });
                        }
                    }

                    conn.Close(); // Close the connection explicitly
                }

                // Filter appointments for the selected date
                var filteredAppointments = appointments.Where(a => a.Start.Date == selectedDate.Date).ToList();

                // Bind to the DataGridView
                BindingList<Appointment> bindingList = new BindingList<Appointment>(filteredAppointments);
                BindingSource source = new BindingSource(bindingList, null);
                dgvAppointments.DataSource = source;

                // Set column headers for better readability
                dgvAppointments.Columns["AppointmentId"].HeaderText = "ID";
                dgvAppointments.Columns["CustomerId"].HeaderText = "Customer ID";
                dgvAppointments.Columns["Title"].HeaderText = "Title";
                dgvAppointments.Columns["Description"].HeaderText = "Description";
                dgvAppointments.Columns["Location"].HeaderText = "Location";
                dgvAppointments.Columns["Contact"].HeaderText = "Contact";
                dgvAppointments.Columns["Type"].HeaderText = "Type";
                dgvAppointments.Columns["Url"].HeaderText = "URL";
                dgvAppointments.Columns["Start"].HeaderText = "Start Time";
                dgvAppointments.Columns["End"].HeaderText = "End Time";

                // Hide columns if not necessary
                dgvAppointments.Columns["CreateDate"].Visible = false;
                dgvAppointments.Columns["CreatedBy"].Visible = false;
                dgvAppointments.Columns["LastUpdate"].Visible = false;
                dgvAppointments.Columns["LastUpdateBy"].Visible = false;

                dgvAppointments.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new list to hold all appointments
                List<Appointment> appointments = new List<Appointment>();

                // Use a fresh connection each time to avoid the "connection is already open" error
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    // Fetch all appointments
                    string query = "SELECT * FROM appointment";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentId = reader.GetInt32("appointmentId"),
                                CustomerId = reader.GetInt32("customerId"),
                                Title = reader.GetString("title"),
                                Description = reader.GetString("description"),
                                Location = reader.GetString("location"),
                                Contact = reader.GetString("contact"),
                                Type = reader.GetString("type"),
                                Url = reader.GetString("url"),
                                Start = reader.GetDateTime("start"),
                                End = reader.GetDateTime("end"),
                                CreateDate = reader.GetDateTime("createDate"),
                                CreatedBy = reader.GetString("createdBy"),
                                LastUpdate = reader.GetDateTime("lastUpdate"),
                                LastUpdateBy = reader.GetString("lastUpdateBy")
                            });
                        }
                    }

                    conn.Close(); // Close the connection explicitly
                }

                // Bind to the DataGridView
                BindingList<Appointment> bindingList = new BindingList<Appointment>(appointments);
                BindingSource source = new BindingSource(bindingList, null);
                dgvAppointments.DataSource = source;

                // Set column headers for better readability
                dgvAppointments.Columns["AppointmentId"].HeaderText = "ID";
                dgvAppointments.Columns["CustomerId"].HeaderText = "Customer ID";
                dgvAppointments.Columns["Title"].HeaderText = "Title";
                dgvAppointments.Columns["Description"].HeaderText = "Description";
                dgvAppointments.Columns["Location"].HeaderText = "Location";
                dgvAppointments.Columns["Contact"].HeaderText = "Contact";
                dgvAppointments.Columns["Type"].HeaderText = "Type";
                dgvAppointments.Columns["Url"].HeaderText = "URL";
                dgvAppointments.Columns["Start"].HeaderText = "Start Time";
                dgvAppointments.Columns["End"].HeaderText = "End Time";

                // Hide columns if not necessary
                dgvAppointments.Columns["CreateDate"].Visible = false;
                dgvAppointments.Columns["CreatedBy"].Visible = false;
                dgvAppointments.Columns["LastUpdate"].Visible = false;
                dgvAppointments.Columns["LastUpdateBy"].Visible = false;

                dgvAppointments.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionStart;
            LoadAppointmentsForSelectedDate(selectedDate);
        }
    }
}
