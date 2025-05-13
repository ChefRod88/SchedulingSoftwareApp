using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SchedulingSoftwareApp.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public static bool InsertAppointment(int customerId, string title, string description, string location, string contact, string type, DateTime startUtc, DateTime endUtc, string createdBy)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string insertQuery = @"
                INSERT INTO appointment (
                    customerId, userId, title, description, location, contact, type, url,
                    start, end, createDate, createdBy, lastUpdate, lastUpdateBy
                ) VALUES (
                    @customerId, @userId, @title, @description, @location, @contact, @type, @url,
                    @start, @end, NOW(), @createdBy, NOW(), @createdBy
                )";

                    using (var cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@userId", 1); // 👈 hardcoded for now (you can replace with dynamic user)
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@url", ""); // 👈 or make dynamic later
                        cmd.Parameters.AddWithValue("@start", startUtc);
                        cmd.Parameters.AddWithValue("@end", endUtc);
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




        public static List<Appointment> GetAllAppointments()
        {
            var appointments = new List<Appointment>();

            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close(); // 🔐 Ensure it's closed before opening again

                    conn.Open();

                    string query = "SELECT * FROM appointment";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return appointments;
        }


        public static bool UpdateAppointment(int appointmentId, int customerId, string title, string description, string location, string contact, string type, DateTime start, DateTime end, string updatedBy)
        {
            using (var conn = Database.GetConnection())
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = @"
                UPDATE appointment 
                SET customerId = @customerId, title = @title, description = @description, location = @location, 
                    contact = @contact, type = @type, start = @start, end = @end, 
                    lastUpdate = NOW(), lastUpdateBy = @updatedBy 
                WHERE appointmentId = @appointmentId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@start", start);
                        cmd.Parameters.AddWithValue("@end", end);
                        cmd.Parameters.AddWithValue("@updatedBy", updatedBy);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }


        public static bool DeleteAppointment(int appointmentId)
        {
            using (var conn = Database.GetConnection())
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "DELETE FROM appointment WHERE appointmentId = @appointmentId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        public static Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT * FROM appointment WHERE appointmentId = @appointmentId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Appointment
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
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        public static bool HasOverlappingAppointment(int customerId, DateTime newStartUtc, DateTime newEndUtc)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = @"
                SELECT COUNT(*) FROM appointment
                WHERE customerId = @customerId
                  AND ((@newStart < end) AND (start < @newEnd))";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@newStart", newStartUtc);
                        cmd.Parameters.AddWithValue("@newEnd", newEndUtc);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking overlap: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Assume conflict to avoid accidental insert
            }
        }
        public static bool HasUpcomingAppointmentWithin15Min()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = @"
                SELECT COUNT(*) FROM appointment
                WHERE start BETWEEN UTC_TIMESTAMP() AND DATE_ADD(UTC_TIMESTAMP(), INTERVAL 15 MINUTE)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking upcoming appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }

}
