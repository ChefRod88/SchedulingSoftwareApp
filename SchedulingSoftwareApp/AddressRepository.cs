using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SchedulingSoftwareApp
{
    public static class AddressRepository
    {
        public static List<Address> GetAllAddresses()
        {
            List<Address> addresses = new List<Address>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT addressId, address FROM address";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // DEFENSIVE BLOCK
                        addresses.Add(new Address
                        {
                            AddressId = reader["addressId"] != DBNull.Value ? Convert.ToInt32(reader["addressId"]) : 0,
                            AddressLine = reader["address"]?.ToString()?.Trim() ?? "Unknown Address",
                            AddressLine2 = reader["address2"]?.ToString()?.Trim() ?? "",
                            CityId = reader["cityId"] != DBNull.Value ? Convert.ToInt32(reader["cityId"]) : -1,
                            PostalCode = reader["postalCode"]?.ToString()?.Trim() ?? "00000",
                            Phone = reader["phone"]?.ToString()?.Trim() ?? "N/A",
                            CreateDate = reader["createDate"] != DBNull.Value ? Convert.ToDateTime(reader["createDate"]) : DateTime.UtcNow,
                            CreatedBy = reader["createdBy"]?.ToString()?.Trim() ?? "System",
                            LastUpdate = reader["lastUpdate"] != DBNull.Value ? Convert.ToDateTime(reader["lastUpdate"]) : DateTime.UtcNow,
                            LastUpdateBy = reader["lastUpdateBy"]?.ToString()?.Trim() ?? "System"
                        });
                    }
                }
            }
            return addresses;
        }
    }
}

