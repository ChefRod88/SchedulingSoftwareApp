using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static bool InsertAppointment(int customerId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, string createdBy)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "INSERT INTO appointment (customerId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@customerId, @title, @description, @location, @contact, @type, @url, @start, @end, NOW(), @createdBy, NOW(), @createdBy)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT * FROM appointment";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
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
            return appointments;
        }

        public static bool UpdateAppointment(int appointmentId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, string updatedBy)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "UPDATE appointment SET title = @title, description = @description, location = @location, contact = @contact, type = @type, url = @url, start = @start, end = @end, lastUpdate = NOW(), lastUpdateBy = @updatedBy WHERE appointmentId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@id", appointmentId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool DeleteAppointment(int appointmentId)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "DELETE FROM appointment WHERE appointmentId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", appointmentId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }

}
