using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchedulingSoftwareApp
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public static bool InsertCity(string cityName, int countryId, string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cityName))
                    cityName = "Unknown City";

                if (countryId <= 0)
                {
                    MessageBox.Show("Invalid Country ID. Cannot create city record.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(createdBy))
                    createdBy = "System";

                using (var conn = Database.GetConnection())
                {
                    string query = @"
                INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES (@name, @countryId, NOW(), @createdBy, NOW(), @createdBy)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", cityName);
                        cmd.Parameters.AddWithValue("@countryId", countryId);
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"MySQL Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static List<string> GetAllCities()
        {
            List<string> cities = new List<string>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT city FROM city";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cities.Add(reader.GetString("city"));
                    }
                }
            }
            return cities;
        }

        public static bool UpdateCity(int cityId, string newName, string updatedBy)
        {
            try
            {
                if (cityId <= 0)
                {
                    MessageBox.Show("Invalid City ID. Cannot update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(newName))
                    newName = "Unknown City";

                if (string.IsNullOrWhiteSpace(updatedBy))
                    updatedBy = "System";

                using (var conn = Database.GetConnection())
                {
                    string query = "UPDATE city SET city = @name, lastUpdate = NOW(), lastUpdateBy = @updatedBy WHERE cityId = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", newName);
                        cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                        cmd.Parameters.AddWithValue("@id", cityId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating city: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DeleteCity(int cityId)
        {
            try
            {
                if (cityId <= 0)
                {
                    MessageBox.Show("Invalid City ID. Cannot delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (var conn = Database.GetConnection())
                {
                    string query = "DELETE FROM city WHERE cityId = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", cityId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting city: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



    }

}
