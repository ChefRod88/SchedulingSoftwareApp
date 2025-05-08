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

        private void PopulateFormFields()
        {
            // Populate fields if in update mode
            if (_isUpdateMode && _selectedAppointment != null)
            {
                cmbCustomerName.SelectedValue = _selectedAppointment.CustomerId;
                txtDescription.Text = _selectedAppointment.Description;
                txtEmail.Text = _selectedAppointment.Contact;
                cmbType.Text = _selectedAppointment.Type;
                dtpAppointmentDay.Value = _selectedAppointment.Start.Date;
                cmbAppointmentTime.SelectedItem = _selectedAppointment.Start.ToString("h:mm tt");
            }
        }

        private void btnSaveAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (!ValidateAppointmentInputs(out int customerId, out string type, out string description, out string email, out DateTime start, out DateTime end))
                    return;

                bool success;
                if (_isUpdateMode)
                {
                    // Update existing appointment
                    success = Appointment.UpdateAppointment(_selectedAppointment.AppointmentId, customerId, type, description, email, "Admin", start, end);
                }
                else
                {
                    // Add new appointment
                    success = Appointment.InsertAppointment(customerId, type, description, email, "Admin", start, end);
                }

                if (success)
                {
                    MessageBox.Show("Your appointment has been saved, and we will send you an email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Close the form after successful addition
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


        private bool ValidateAppointmentInputs(out int customerId, out string type, out string description, out string email, out DateTime start, out DateTime end)
        {
            // Initialize out parameters
            customerId = -1;
            type = cmbType.Text.Trim();
            description = txtDescription.Text.Trim();
            email = txtEmail.Text.Trim();
            start = DateTime.MinValue;
            end = DateTime.MinValue;

            // Validate customer
            if (cmbCustomerName.SelectedValue == null || !int.TryParse(cmbCustomerName.SelectedValue.ToString(), out customerId))
            {
                MessageBox.Show("Please select a valid customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate required fields
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(email))
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
    }
}

