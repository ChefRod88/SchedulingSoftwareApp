using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SchedulingSoftwareApp
{
    public static class CustomerRepository
    {
        public static DataTable GetAllCustomers()
        {
            DataTable customerTable = new DataTable();

            try
            {
                using (var conn = Database.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    // Join customer and address tables to include address and phone
                    string query = @"
                SELECT 
                    c.customerId,
                    c.customerName,
                    c.active,
                    c.createDate,
                    c.createdBy,
                    c.lastUpdate,
                    c.lastUpdateBy,
                    a.address,
                    a.phone
                FROM customer c
                JOIN address a ON c.addressId = a.addressId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        customerTable.Load(reader);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading customers: {ex.Message}");
            }

            return customerTable;
        }




        public static bool InsertCustomer(string customerName, int addressId, bool active, string createdBy)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    // Ensure the connection is not already open
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                                   "VALUES (@name, @addressId, @active, NOW(), @createdBy, NOW(), @createdBy)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", customerName);
                    cmd.Parameters.AddWithValue("@addressId", addressId);
                    cmd.Parameters.AddWithValue("@active", active);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting customer: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool UpdateCustomer(int customerId, string customerName, int addressId, bool active, string updatedBy)
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    // Ensure the connection is not already open
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "UPDATE customer SET customerName = @name, addressId = @addressId, active = @active, " +
                                   "lastUpdate = NOW(), lastUpdateBy = @updatedBy WHERE customerId = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", customerName);
                    cmd.Parameters.AddWithValue("@addressId", addressId);
                    cmd.Parameters.AddWithValue("@active", active);
                    cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                    cmd.Parameters.AddWithValue("@id", customerId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    return rowsAffected > 0;
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
                    // Ensure the connection is not already open
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "DELETE FROM customer WHERE customerId = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", customerId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    return rowsAffected > 0;
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
