using MySql.Data.MySqlClient;
using SchedulingSoftwareApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
//using SchedulingSoftwareApp.Helpers; // 🔁 for TimeHelper


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
            DateTime startTime = DateTime.Today.AddHours(9);
            DateTime endTime = DateTime.Today.AddHours(17);

            while (startTime < endTime)
            {
                cmbAppointmentTime.Items.Add(startTime.ToString("h:mm tt"));
                startTime = startTime.AddMinutes(15);
            }

            if (cmbAppointmentTime.Items.Count > 0)
                cmbAppointmentTime.SelectedIndex = 0;
        }

        private void LoadAppointments()
        {
            try
            {
                List<Appointment> appointments = new List<Appointment>();

                using (MySqlConnection conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM appointment";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime startUtc = reader.GetDateTime("start");
                                DateTime endUtc = reader.GetDateTime("end");

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

                                    // Time Helper
                                    Start = TimeHelper.ToEST(startUtc),
                                    End = TimeHelper.ToEST(endUtc),

                                    CreateDate = reader.GetDateTime("createDate"),
                                    CreatedBy = reader.GetString("createdBy"),
                                    LastUpdate = reader.GetDateTime("lastUpdate"),
                                    LastUpdateBy = reader.GetString("lastUpdateBy")
                                });
                            }
                        }
                    }
                }

                BindingList<Appointment> bindingList = new BindingList<Appointment>(appointments);
                dgvAppointments.DataSource = bindingList;

                dgvAppointments.Columns["AppointmentId"].HeaderText = "ID";
                dgvAppointments.Columns["CustomerId"].HeaderText = "Customer ID";
                dgvAppointments.Columns["Title"].HeaderText = "Title";
                dgvAppointments.Columns["Description"].HeaderText = "Description";
                dgvAppointments.Columns["Location"].HeaderText = "Location";
                dgvAppointments.Columns["Contact"].HeaderText = "Contact";
                dgvAppointments.Columns["Type"].HeaderText = "Type";
                dgvAppointments.Columns["Url"].HeaderText = "URL";
                // Clarity for (EST) Display
                dgvAppointments.Columns["Start"].HeaderText = "Start Time (EST)";
                dgvAppointments.Columns["End"].HeaderText = "End Time (EST)";


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
            if (_isUpdateMode && _selectedAppointment != null)
            {
                cmbCustomerName.SelectedValue = _selectedAppointment.CustomerId;
                txtTitle.Text = _selectedAppointment.Title;
                txtDescription.Text = _selectedAppointment.Description;
                txtLocation.Text = _selectedAppointment.Location;
                txtContact.Text = _selectedAppointment.Contact;
                cmbType.Text = _selectedAppointment.Type;
                //Time Helper
                DateTime estStart = TimeHelper.ToEST(_selectedAppointment.Start);
                dtpAppointmentDay.Value = estStart.Date;
                cmbAppointmentTime.SelectedItem = estStart.ToString("h:mm tt");

            }
        }

        private bool ValidateAppointmentInputs(
            out int customerId,
            out string title,
            out string description,
            out string location,
            out string contact,
            out string type,
            out DateTime start,
            out DateTime end)
        {
            customerId = -1;
            title = txtTitle.Text.Trim();
            description = txtDescription.Text.Trim();
            location = txtLocation.Text.Trim();
            contact = txtContact.Text.Trim();
            type = cmbType.Text.Trim();
            start = DateTime.MinValue;
            end = DateTime.MinValue;

            if (cmbCustomerName.SelectedValue == null ||
                !int.TryParse(cmbCustomerName.SelectedValue.ToString(), out customerId))
            {
                MessageBox.Show("Please select a valid customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) ||
                string.IsNullOrEmpty(location) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(type))
            {
                MessageBox.Show("All fields are required and cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbAppointmentTime.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid appointment time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DateTime selectedDate = dtpAppointmentDay.Value.Date;
            DateTime selectedTime = DateTime.Parse(cmbAppointmentTime.SelectedItem.ToString());
            start = selectedDate.Add(selectedTime.TimeOfDay);
            end = start.AddMinutes(30);

            // Convert start time to EST before checking
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime startEST = start;




            // Validate against EST business hours and weekdays
            if (startEST.Hour < 9 || startEST.Hour >= 17 ||
                startEST.DayOfWeek == DayOfWeek.Saturday ||
                startEST.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Appointments must be scheduled during EST business hours" +
                    " (9:00 AM to 5:00 PM, Monday to Friday).", "Business Hours Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // ✅ All validations passed
        }


        private void btnCancelAppointment_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateAppointmentInputs(out int customerId, out string title, out string description, out string location, out string contact, out string type, out DateTime startLocal, out DateTime endLocal))
                    return;

                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
                //Time Helper
                DateTime startUtc = TimeHelper.ToUTCFromEST(startLocal);
                DateTime endUtc = TimeHelper.ToUTCFromEST(endLocal);


                if (Appointment.HasOverlappingAppointment(customerId, startUtc, endUtc))
                {
                    MessageBox.Show("This appointment overlaps with an existing appointment for this customer.", "Overlap Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success;
                if (_isUpdateMode && _selectedAppointment != null)
                {
                    success = Appointment.UpdateAppointment(
                        _selectedAppointment.AppointmentId,
                        customerId,
                        title,
                        description,
                        location,
                        contact,
                        type,
                        startUtc,
                        endUtc,
                        "Admin"
                    );

                    _isUpdateMode = false;
                    _selectedAppointment = null;
                }
                else
                {
                    success = Appointment.InsertAppointment(
                        customerId,
                        title,
                        description,
                        location,
                        contact,
                        type,
                        startUtc,
                        endUtc,
                        "Admin"
                    );
                }

                if (success)
                {
                    MessageBox.Show("Your appointment has been saved, and we will send you an email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                }
                else
                {
                    MessageBox.Show("Failed to add appointment. Please check the input values.", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAppointments.SelectedRows.Count > 0)
                {
                    int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["appointmentId"].Value);
                    string appointmentTitle = dgvAppointments.SelectedRows[0].Cells["title"].Value.ToString();

                    var confirmResult = MessageBox.Show($"Are you sure you want to delete the appointment '{appointmentTitle}'?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        bool success = Appointment.DeleteAppointment(appointmentId);
                        if (success)
                        {
                            MessageBox.Show("Appointment deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAppointments();
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
            calendarForm.ShowDialog();
        }

        private void btnAppointmentReports_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm();
            reportsForm.Show();
        }

        private void btnUpdateAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAppointments.SelectedRows.Count > 0)
                {
                    int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentId"].Value);
                    Appointment selectedAppointment = Appointment.GetAppointmentById(appointmentId);

                    if (selectedAppointment != null)
                    {
                        _selectedAppointment = selectedAppointment;
                        _isUpdateMode = true;

                        cmbCustomerName.SelectedValue = selectedAppointment.CustomerId;
                        txtTitle.Text = selectedAppointment.Title;
                        txtDescription.Text = selectedAppointment.Description;
                        txtLocation.Text = selectedAppointment.Location;
                        txtContact.Text = selectedAppointment.Contact;
                        cmbType.SelectedItem = selectedAppointment.Type;
                        //Time Helper
                        DateTime estStart = TimeHelper.ToEST(_selectedAppointment.Start);
                        dtpAppointmentDay.Value = estStart.Date;
                        cmbAppointmentTime.SelectedItem = estStart.ToString("h:mm tt");

                    }
                }
                else
                {
                    MessageBox.Show("Please select an appointment to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        
    }
}
