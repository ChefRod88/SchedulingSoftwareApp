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
    public partial class AppointmentForm : Form
    {
        private Appointment _selectedAppointment;
        private bool _isUpdateMode;

        public AppointmentForm()
        {
            InitializeComponent();
            LoadCustomerDropdown();
            LoadTypeDropdown();
            LoadTimeBlocks();
            LoadAppointments();
            this.Text = "Add New Appointment";
        }

        public AppointmentForm(Appointment appointment)
        {
            InitializeComponent();
            _selectedAppointment = appointment;
            _isUpdateMode = true;
            this.Text = "Update Appointment";
            LoadCustomerDropdown();
            LoadTypeDropdown();
            LoadTimeBlocks();
            LoadAppointments();
            PopulateFormFields();
        }

        private void LoadCustomerDropdown()
        {
            try
            {
                List<Customer> customers = CustomerRepository.GetAllCustomers();
                cmbCustomerName.DataSource = customers;
                cmbCustomerName.DisplayMember = "CustomerName";
                cmbCustomerName.ValueMember = "CustomerId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTypeDropdown()
        {
            // Populate predefined appointment types
            cmbType.Items.AddRange(new string[]
            {
                "Consultation",
                "Follow-up",
                "Sales Call",
                "Support Call",
                "New Client Meeting",
                "Project Update"
            });
        }

        private void LoadTimeBlocks()
        {
            // Populate the time blocks from 9:00 AM to 5:00 PM in 30-minute increments
            DateTime startTime = DateTime.Today.AddHours(9);
            DateTime endTime = DateTime.Today.AddHours(17);

            while (startTime < endTime)
            {
                cmbAppointmentTime.Items.Add(startTime.ToString("h:mm tt"));
                startTime = startTime.AddMinutes(30);
            }

            // Set the default selected time to 9:00 AM
            if (cmbAppointmentTime.Items.Count > 0)
                cmbAppointmentTime.SelectedIndex = 0;
        }

        private void LoadAppointments()
        {
            try
            {
                // Create a new list to hold appointments
                List<Appointment> appointments = new List<Appointment>();

                // Use a single connection with proper exception handling
                using (MySqlConnection conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    // Fetch all appointments
                    string query = "SELECT * FROM appointment";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
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
                    }
                }

                // Bind to the DataGridView
                BindingList<Appointment> bindingList = new BindingList<Appointment>(appointments);
                dgvAppointments.DataSource = bindingList;

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

                // Hide unnecessary columns
                dgvAppointments.Columns["CreateDate"].Visible = false;
                dgvAppointments.Columns["CreatedBy"].Visible = false;
                dgvAppointments.Columns["LastUpdate"].Visible = false;
                dgvAppointments.Columns["LastUpdateBy"].Visible = false;

                dgvAppointments.AutoResizeColumns();
            }
            catch (InvalidOperationException invOpEx)
            {
                MessageBox.Show($"Database connection issue: {invOpEx.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void PopulateFormFields()
        {
            // Populate fields if in update mode
            if (_isUpdateMode && _selectedAppointment != null)
            {
                cmbCustomerName.SelectedValue = _selectedAppointment.CustomerId;
                txtTitle.Text = _selectedAppointment.Title;
                txtDescription.Text = _selectedAppointment.Description;
                txtLocation.Text = _selectedAppointment.Location;
                txtContact.Text = _selectedAppointment.Contact;
                cmbType.Text = _selectedAppointment.Type;
                dtpAppointmentDay.Value = _selectedAppointment.Start.Date;
                cmbAppointmentTime.SelectedItem = _selectedAppointment.Start.ToString("h:mm tt");
            }
        }

        


        private bool ValidateAppointmentInputs(out int customerId, out string title, out string description, out string location, out string contact, out string type, out DateTime start, out DateTime end)
        {
            // Initialize out parameters
            customerId = -1;
            title = txtTitle.Text.Trim();
            description = txtDescription.Text.Trim();
            location = txtLocation.Text.Trim();
            contact = txtContact.Text.Trim();
            type = cmbType.Text.Trim();
            start = DateTime.MinValue;
            end = DateTime.MinValue;

            // Validate customer
            if (cmbCustomerName.SelectedValue == null || !int.TryParse(cmbCustomerName.SelectedValue.ToString(), out customerId))
            {
                MessageBox.Show("Please select a valid customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate required fields
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(type))
            {
                MessageBox.Show("All fields are required and cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate time block selection
            if (cmbAppointmentTime.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid appointment time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Set start and end times
            DateTime selectedDate = dtpAppointmentDay.Value.Date;
            DateTime selectedTime = DateTime.Parse(cmbAppointmentTime.SelectedItem.ToString());
            start = selectedDate.Add(selectedTime.TimeOfDay);
            end = start.AddMinutes(30);  // Default to a 30-minute appointment

            // Validate business hours (9:00 AM to 5:00 PM, Monday to Friday)
            if (start.Hour < 9 || start.Hour >= 17 || start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Appointments must be scheduled during business hours (9:00 AM to 5:00 PM, Monday to Friday).", "Business Hours Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private void btnCancelAppointment_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateAppointmentInputs(out int customerId, out string title, out string description, out string location, out string contact, out string type, out DateTime start, out DateTime end))
                    return;

                bool success;
                if (_isUpdateMode)
                {
                    // Update existing appointment
                    success = Appointment.UpdateAppointment(_selectedAppointment.AppointmentId, customerId, title, description, location, contact, type, start, end, "Admin");
                }
                else
                {
                    // Add new appointment
                    success = Appointment.InsertAppointment(customerId, title, description, location, contact, type, start, end, "Admin");
                }

                if (success)
                {
                    MessageBox.Show("Your appointment has been saved, and we will send you an email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the appointments grid to show the latest addition or update
                    LoadAppointments();

                    // Clear the form for the next entry if adding a new appointment
                    if (!_isUpdateMode)
                    {
                        cmbCustomerName.SelectedIndex = -1;
                        txtTitle.Clear();
                        txtDescription.Clear();
                        txtLocation.Clear();
                        txtContact.Clear();
                        cmbType.SelectedIndex = -1;
                        cmbAppointmentTime.SelectedIndex = 0;
                        dtpAppointmentDay.Value = DateTime.Today;
                    }

                }
                else
                {
                    MessageBox.Show("Failed to add appointment. Please check the input values.", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                // Make sure a row is selected
                if (dgvAppointments.SelectedRows.Count > 0)
                {
                    // Get the selected appointment ID
                    int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["appointmentId"].Value);
                    string appointmentTitle = dgvAppointments.SelectedRows[0].Cells["title"].Value.ToString();

                    // Confirm deletion
                    var confirmResult = MessageBox.Show($"Are you sure you want to delete the appointment '{appointmentTitle}'?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        // Attempt to delete the appointment
                        bool success = Appointment.DeleteAppointment(appointmentId);
                        if (success)
                        {
                            MessageBox.Show("Appointment deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAppointments(); // Refresh the grid after deletion
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the appointment. Please try again.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an appointment to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCalendarView_Click(object sender, EventArgs e)
        {
            CalendarForm calendarForm = new CalendarForm();
            calendarForm.ShowDialog(); // Use ShowDialog to keep it modal
        }

    }
}
