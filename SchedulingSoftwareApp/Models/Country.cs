using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchedulingSoftwareApp
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public static bool InsertCountry(string countryName, string createdBy)
        {
            try
            {
                // Defensive input validation
                countryName = string.IsNullOrWhiteSpace(countryName) ? "Unknown Country" : countryName.Trim();
                createdBy = string.IsNullOrWhiteSpace(createdBy) ? "System" : createdBy.Trim();

                using (var conn = Database.GetConnection())
                {
                    string query = @"
                INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES (@name, NOW(), @createdBy, NOW(), @createdBy)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", countryName);
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting country: {ex.Message}", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT country FROM country";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(reader["country"]?.ToString()?.Trim() ?? "Unknown Country");
                        }
                    }
                }
            }
            return countries;
        }


        public static bool UpdateCountry(int countryId, string newName, string updatedBy)
        {
            try
            {
                if (countryId <= 0)
                {
                    MessageBox.Show("Invalid Country ID. Cannot update record.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                newName = string.IsNullOrWhiteSpace(newName) ? "Unknown Country" : newName.Trim();
                updatedBy = string.IsNullOrWhiteSpace(updatedBy) ? "System" : updatedBy.Trim();

                using (var conn = Database.GetConnection())
                {
                    string query = @"
                UPDATE country
                SET country = @name, lastUpdate = NOW(), lastUpdateBy = @updatedBy
                WHERE countryId = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", newName);
                        cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                        cmd.Parameters.AddWithValue("@id", countryId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating country: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool DeleteCountry(int countryId)
        {
            try
            {
                if (countryId <= 0)
                {
                    MessageBox.Show("Invalid Country ID. Cannot delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (var conn = Database.GetConnection())
                {
                    string query = "DELETE FROM country WHERE countryId = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", countryId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting country: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



    }



}
