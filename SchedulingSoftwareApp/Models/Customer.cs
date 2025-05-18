using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchedulingSoftwareApp
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }    // ✅ Add this line
        public string Phone { get; set; }      // ✅ Add this line
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public static bool InsertCustomer(string customerName, int addressId, bool active, string createdBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customerName))
                    customerName = "Unnamed Customer";

                if (addressId <= 0)
                {
                    MessageBox.Show("Invalid address ID. Please select a valid address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(createdBy))
                    createdBy = "System";

                using (var conn = Database.GetConnection())
                {
                    string query = @"
                INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES (@name, @addressId, @active, NOW(), @createdBy, NOW(), @createdBy)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", customerName);
                        cmd.Parameters.AddWithValue("@addressId", addressId);
                        cmd.Parameters.AddWithValue("@active", active);
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting customer: {ex.Message}", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (var conn = Database.GetConnection())
            {
                string query = "SELECT * FROM customer";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerId = reader["customerId"] != DBNull.Value ? Convert.ToInt32(reader["customerId"]) : 0,
                                CustomerName = reader["customerName"]?.ToString()?.Trim() ?? "Unnamed Customer",
                                AddressId = reader["addressId"] != DBNull.Value ? Convert.ToInt32(reader["addressId"]) : -1,
                                Active = reader["active"] != DBNull.Value && Convert.ToBoolean(reader["active"]),
                                CreateDate = reader["createDate"] != DBNull.Value ? Convert.ToDateTime(reader["createDate"]) : DateTime.UtcNow,
                                CreatedBy = reader["createdBy"]?.ToString()?.Trim() ?? "System",
                                LastUpdate = reader["lastUpdate"] != DBNull.Value ? Convert.ToDateTime(reader["lastUpdate"]) : DateTime.UtcNow,
                                LastUpdateBy = reader["lastUpdateBy"]?.ToString()?.Trim() ?? "System"
                            });
                        }
                    }
                }
            }

            return customers;
        }


        public static bool UpdateCustomer(int customerId, string newName, int addressId, bool active, string updatedBy)
        {
            try
            {
                if (customerId <= 0)
                {
                    MessageBox.Show("Invalid customer ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(newName))
                    newName = "Unnamed Customer";

                if (addressId <= 0)
                {
                    MessageBox.Show("Invalid address ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(updatedBy))
                    updatedBy = "System";

                using (var conn = Database.GetConnection())
                {
                    string query = @"
                UPDATE customer
                SET customerName = @name, addressId = @addressId, active = @active,
                    lastUpdate = NOW(), lastUpdateBy = @updatedBy
                WHERE customerId = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", newName);
                        cmd.Parameters.AddWithValue("@addressId", addressId);
                        cmd.Parameters.AddWithValue("@active", active);
                        cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                        cmd.Parameters.AddWithValue("@id", customerId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating customer: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DeleteCustomer(int customerId)
        {
            try
            {
                if (customerId <= 0)
                {
                    MessageBox.Show("Invalid customer ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (var conn = Database.GetConnection())
                {
                    string query = "DELETE FROM customer WHERE customerId = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", customerId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



    }

}
