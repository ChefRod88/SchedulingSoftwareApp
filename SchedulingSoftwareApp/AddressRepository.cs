using MySql.Data.MySqlClient;
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
                        addresses.Add(new Address
                        {
                            AddressId = reader.GetInt32("addressId"),
                            AddressLine = reader.GetString("address")
                        });
                    }
                }
            }
            return addresses;
        }
    }
}

