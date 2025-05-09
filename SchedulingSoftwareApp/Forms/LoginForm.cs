using MySql.Data.MySqlClient;
using SchedulingSoftwareApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Forms
{
    public partial class LoginForm : Form
    {
        private const string RequiredUsername = "test";
        private const string RequiredPassword = "test";

        // Localization
        private string CurrentLanguage;
        private string LanguageCode;
        private string LoginErrorMessage;
        private string LocationMessage;

        public LoginForm()
        {
            InitializeComponent();
            DetectUserLocation();
            SetLanguageMessages();
        }

        private void DetectUserLocation()
        {
            try
            {
                RegionInfo region = RegionInfo.CurrentRegion;
                LanguageCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

                if (LanguageCode == "es")
                {
                    CurrentLanguage = "Español";
                    LocationMessage = $"Ubicación detectada: {region.DisplayName}";
                }
                else
                {
                    CurrentLanguage = "English";
                    LocationMessage = $"Location detected: {region.DisplayName}";
                }

                lblLocation.Text = LocationMessage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error detecting location: {ex.Message}", "Location Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM user WHERE userName = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    // Open the connection if it's not already open
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    // Close the connection after the query
                    conn.Close();

                    return userCount > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void LogLoginAttempt(string username, bool success)
        {
            try
            {
                string status = success ? "Success" : "Failure";
                string logEntry = $"{DateTime.Now:G} - Username: {username}, Status: {status}";
                File.AppendAllText("Login_History.txt", logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to login history file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Validate against the database
                if (AuthenticateUser(username, password))
                {
                    // Log successful login
                    LogLoginAttempt(username, success: true);

                    // Check for upcoming appointments within 15 minutes
                    CheckUpcomingAppointments();

                    MessageBox.Show("You have successfully logged in!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Open the Main Form
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    // Log failed login
                    LogLoginAttempt(username, success: false);

                    MessageBox.Show("The username and password do not match.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckUpcomingAppointments()
        {
            try
            {
                // Get the current UTC time and add 15 minutes
                DateTime utcNow = DateTime.UtcNow;
                DateTime utc15MinutesFromNow = utcNow.AddMinutes(15);

                // Fetch all upcoming appointments within the next 15 minutes
                List<Appointment> upcomingAppointments = new List<Appointment>();

                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = @"
                SELECT appointmentId, customerId, title, start, end, type
                FROM appointment
                WHERE start BETWEEN @start AND @end";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@start", utcNow);
                        cmd.Parameters.AddWithValue("@end", utc15MinutesFromNow);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                upcomingAppointments.Add(new Appointment
                                {
                                    AppointmentId = reader.GetInt32("appointmentId"),
                                    CustomerId = reader.GetInt32("customerId"),
                                    Title = reader.GetString("title"),
                                    Start = reader.GetDateTime("start"),
                                    End = reader.GetDateTime("end"),
                                    Type = reader.GetString("type")
                                });
                            }
                        }
                    }
                }

                // Show alert if there are any upcoming appointments
                if (upcomingAppointments.Count > 0)
                {
                    StringBuilder message = new StringBuilder("You have the following appointments within the next 15 minutes:\n\n");

                    foreach (var appointment in upcomingAppointments)
                    {
                        DateTime localStartTime = appointment.Start.ToLocalTime();
                        message.AppendLine($"- {appointment.Title} at {localStartTime.ToString("hh:mm tt")} ({appointment.Type})");
                    }

                    MessageBox.Show(message.ToString(), "Upcoming Appointments", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking for upcoming appointments: {ex.Message}", "Alert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
