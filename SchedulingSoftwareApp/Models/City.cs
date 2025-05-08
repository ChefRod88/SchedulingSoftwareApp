using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
            using (var conn = Database.GetConnection())
            {
                string query = "INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@name, @countryId, NOW(), @createdBy, NOW(), @createdBy)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", cityName);
                cmd.Parameters.AddWithValue("@countryId", countryId);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
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
            using (var conn = Database.GetConnection())
            {
                string query = "UPDATE city SET city = @name, lastUpdate = NOW(), lastUpdateBy = @updatedBy WHERE cityId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@id", cityId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool DeleteCity(int cityId)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "DELETE FROM city WHERE cityId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", cityId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


    }

}
