using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SchedulingSoftwareApp
{
    public static class CustomerRepository
    {
        public static List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close(); // 🛑 Close it before opening

                    conn.Open();

                    string query = @"
                SELECT 
                    c.customerId,
                    c.customerName,
                    c.addressId,
                    a.address,
                    a.phone,
                    c.active,
                    c.createDate,
                    c.createdBy,
                    c.lastUpdate,
                    c.lastUpdateBy
                FROM customer c
                JOIN address a ON c.addressId = a.addressId";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerId = reader.GetInt32("customerId"),
                                CustomerName = reader.GetString("customerName"),
                                AddressId = reader.GetInt32("addressId"),
                                Address = reader.GetString("address"),
                                Phone = reader.GetString("phone"),
                                Active = reader.GetBoolean("active"),
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
                MessageBox.Show($"Error loading customers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return customers;
        }



        public static bool InsertCustomer(string customerName, string address, string phone, bool active, string createdBy)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    // ✅ Insert into address table with required fields
                    string insertAddressQuery = @"
                INSERT INTO address (address, phone, cityId, postalCode, createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES (@address, @phone, @cityId, @postalCode, NOW(), @createdBy, NOW(), @createdBy)";

                    using (var addressCmd = new MySqlCommand(insertAddressQuery, conn))
                    {
                        addressCmd.Parameters.AddWithValue("@address", address);
                        addressCmd.Parameters.AddWithValue("@phone", phone);
                        addressCmd.Parameters.AddWithValue("@cityId", 1); // default cityId
                        addressCmd.Parameters.AddWithValue("@postalCode", "00000"); // placeholder postal code
                        addressCmd.Parameters.AddWithValue("@createdBy", createdBy);

                        addressCmd.ExecuteNonQuery();
                    }

                    // ✅ Get newly inserted addressId
                    int addressId;
                    using (var getIdCmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn))
                    {
                        addressId = Convert.ToInt32(getIdCmd.ExecuteScalar());
                    }

                    // ✅ Insert into customer table
                    string insertCustomerQuery = @"
                INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES (@customerName, @addressId, @active, NOW(), @createdBy, NOW(), @createdBy)";

                    using (var customerCmd = new MySqlCommand(insertCustomerQuery, conn))
                    {
                        customerCmd.Parameters.AddWithValue("@customerName", customerName);
                        customerCmd.Parameters.AddWithValue("@addressId", addressId);
                        customerCmd.Parameters.AddWithValue("@active", active ? 1 : 0);
                        customerCmd.Parameters.AddWithValue("@createdBy", createdBy);

                        customerCmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




        public static bool UpdateCustomer(int customerId, string customerName, string address, string phone, bool active, string updatedBy)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    // Get addressId from customer
                    int addressId;
                    using (var getIdCmd = new MySqlCommand("SELECT addressId FROM customer WHERE customerId = @customerId", conn))
                    {
                        getIdCmd.Parameters.AddWithValue("@customerId", customerId);
                        addressId = Convert.ToInt32(getIdCmd.ExecuteScalar());
                    }

                    // Update address
                    using (var addressCmd = new MySqlCommand(@"
                UPDATE address 
                SET address = @address, phone = @phone, lastUpdate = NOW(), lastUpdateBy = @updatedBy 
                WHERE addressId = @addressId", conn))
                    {
                        addressCmd.Parameters.AddWithValue("@address", address);
                        addressCmd.Parameters.AddWithValue("@phone", phone);
                        addressCmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                        addressCmd.Parameters.AddWithValue("@addressId", addressId);
                        addressCmd.ExecuteNonQuery();
                    }

                    // Update customer
                    using (var customerCmd = new MySqlCommand(@"
                UPDATE customer 
                SET customerName = @customerName, active = @active, lastUpdate = NOW(), lastUpdateBy = @updatedBy 
                WHERE customerId = @customerId", conn))
                    {
                        customerCmd.Parameters.AddWithValue("@customerName", customerName);
                        customerCmd.Parameters.AddWithValue("@active", active);
                        customerCmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                        customerCmd.Parameters.AddWithValue("@customerId", customerId);
                        customerCmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool DeleteCustomer(int customerId)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    // ✅ Only open if not already open
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string deleteQuery = "DELETE FROM customer WHERE customerId = @customerId";

                    using (var cmd = new MySqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
