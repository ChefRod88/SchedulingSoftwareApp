using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchedulingSoftwareApp
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public string AddressLine2 { get; set; }
        public int CityId { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }


        // Defense
        public static bool InsertAddress(
             string addressLine1,
             string addressLine2,
             int cityId,
             string postalCode,
             string phone,
             string createdBy)
        {
            try
            {
                // Defensive string handling
                addressLine1 = string.IsNullOrWhiteSpace(addressLine1) ? "Unknown Address" : addressLine1.Trim();
                addressLine2 = string.IsNullOrWhiteSpace(addressLine2) ? "" : addressLine2.Trim();
                postalCode = string.IsNullOrWhiteSpace(postalCode) ? "00000" : postalCode.Trim();
                phone = string.IsNullOrWhiteSpace(phone) ? "N/A" : phone.Trim();
                createdBy = string.IsNullOrWhiteSpace(createdBy) ? "System" : createdBy.Trim();

                // Defensive integer validation
                if (cityId <= 0)
                {
                    MessageBox.Show("Invalid City ID. Please provide a valid city.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Timestamps (createDate / lastUpdate) are handled in SQL with NOW(), but fallback logic is shown here just in case

                using (var conn = Database.GetConnection())
                {
                    string query = @"
                INSERT INTO address
                (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES
                (@address1, @address2, @cityId, @postalCode, @phone, NOW(), @createdBy, NOW(), @createdBy)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@address1", addressLine1);
                        cmd.Parameters.AddWithValue("@address2", addressLine2);
                        cmd.Parameters.AddWithValue("@cityId", cityId);
                        cmd.Parameters.AddWithValue("@postalCode", postalCode);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "MySQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static List<Address> GetAllAddresses()
        {
            List<Address> addresses = new List<Address>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT * FROM address";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        addresses.Add(new Address
                        {
                            AddressId = reader.GetInt32("addressId"),
                            AddressLine = reader.GetString("address"),
                            AddressLine2 = reader.GetString("address2"),
                            CityId = reader.GetInt32("cityId"),
                            PostalCode = reader.GetString("postalCode"),
                            Phone = reader.GetString("phone"),
                            CreateDate = reader.GetDateTime("createDate"),
                            CreatedBy = reader.GetString("createdBy"),
                            LastUpdate = reader.GetDateTime("lastUpdate"),
                            LastUpdateBy = reader.GetString("lastUpdateBy")
                        });
                    }
                }
            }
            return addresses;
        }
        public static bool UpdateAddress(int addressId, string newAddressLine1, string newAddressLine2, int cityId, string postalCode, string phone, string updatedBy)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "UPDATE address SET address = @address1, address2 = @address2, cityId = @cityId, postalCode = @postalCode, phone = @phone, lastUpdate = NOW(), lastUpdateBy = @updatedBy WHERE addressId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@address1", newAddressLine1);
                cmd.Parameters.AddWithValue("@address2", newAddressLine2);
                cmd.Parameters.AddWithValue("@cityId", cityId);
                cmd.Parameters.AddWithValue("@postalCode", postalCode);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@id", addressId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool DeleteAddress(int addressId)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "DELETE FROM address WHERE addressId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", addressId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }

}
