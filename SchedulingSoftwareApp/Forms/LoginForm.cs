using System;
using System.Globalization;
using System.Net;
using System.Windows.Forms;

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
            string selectedLanguage = cmbLanguage.SelectedItem.ToString();
            LanguageCode = selectedLanguage.ToLower();
            SetLanguageMessages();
        }

        private void DetectUserLocation()
        {
            try
            {
                // Get the current region and time zone
                RegionInfo region = RegionInfo.CurrentRegion;
                string city = GetCityFromIPAddress();
                string timeZone = TimeZoneInfo.Local.DisplayName;

                // Build the location message
                LocationMessage = $"{city}, {region.EnglishName}, {timeZone}";

                // Set the location label
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

                // Validate credentials 
                if (username == "test" && password == "test")
                {
                    MessageBox.Show("You have successfully logged in!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(LoginErrorMessage, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
