using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (var conn = Database.GetConnection())
            {
                string query = "INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@name, NOW(), @createdBy, NOW(), @createdBy)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", countryName);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT country FROM country";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countries.Add(reader.GetString("country"));
                    }
                }
            }
            return countries;
        }

        public static bool UpdateCountry(int countryId, string newName, string updatedBy)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "UPDATE country SET country = @name, lastUpdate = NOW(), lastUpdateBy = @updatedBy WHERE countryId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@id", countryId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteCountry(int countryId)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "DELETE FROM country WHERE countryId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", countryId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


    }



}
