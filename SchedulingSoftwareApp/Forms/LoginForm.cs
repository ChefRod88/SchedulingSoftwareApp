using System;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SchedulingSoftwareApp.Models;

namespace SchedulingSoftwareApp.Forms
{
    public partial class LoginForm : Form
    {
        private string CurrentLanguage;
        private string LanguageCode;
        private string LoginErrorMessage;
        private string LocationMessage;

        public LoginForm()
        {
            InitializeComponent();
            DetectUserLocation();
            SetLanguageMessages();
            InitializeLanguageComboBox();
        }

        private void InitializeLanguageComboBox()
        {
            cmbLanguage.Items.Add("EN");
            cmbLanguage.Items.Add("ES");
            cmbLanguage.SelectedIndex = 0; // Default to English
            cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LanguageCode = cmbLanguage.SelectedItem.ToString().ToLower();
            SetLanguageMessages();
        }

        private void DetectUserLocation()
        {
            try
            {
                RegionInfo region = RegionInfo.CurrentRegion;
                string city = GetCityFromIPAddress();
                string timeZone = TimeZoneInfo.Local.DisplayName;
                LocationMessage = $"{city}, {region.EnglishName}, {timeZone}";
                lblLocation.Text = LocationMessage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error detecting location: {ex.Message}", "Location Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCityFromIPAddress()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString("http://ip-api.com/json/");
                    dynamic locationData = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    return $"{locationData.city}, {locationData.regionName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching city and state: {ex.Message}", "Location Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Unknown City";
            }
        }

        private void SetLanguageMessages()
        {
            if (LanguageCode == "es")
            {
                lblUsername.Text = "Nombre de usuario:";
                lblPassword.Text = "Contraseña:";
                btnLogin.Text = "Iniciar sesión";
                LoginErrorMessage = "El nombre de usuario y la contraseña no coinciden.";
            }
            else
            {
                lblUsername.Text = "Username:";
                lblPassword.Text = "Password:";
                btnLogin.Text = "Login";
                LoginErrorMessage = "The username and password do not match.";
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Validate against the database
                using (var connection = Database.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM user WHERE userName = @username AND password = @password";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userCount > 0)
                        {
                            MessageBox.Show("You have successfully logged in!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // ✅ Alert for upcoming appointment
                            if (Appointment.HasUpcomingAppointmentWithin15Min())
                            {
                                MessageBox.Show("⚠️ You have an appointment within the next 15 minutes!", "Upcoming Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show(LoginErrorMessage, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

