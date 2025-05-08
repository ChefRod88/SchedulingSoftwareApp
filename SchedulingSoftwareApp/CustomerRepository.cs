using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSoftwareApp
{
    public static class CustomerRepository
    {
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT * FROM customer";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = reader.GetInt32("customerId"),
                            CustomerName = reader.GetString("customerName"),
                            AddressId = reader.GetInt32("addressId"),
                            Active = reader.GetBoolean("active"),
                            CreateDate = reader.GetDateTime("createDate"),
                            CreatedBy = reader.GetString("createdBy"),
                            LastUpdate = reader.GetDateTime("lastUpdate"),
                            LastUpdateBy = reader.GetString("lastUpdateBy")
                        });
                    }
                }
            }
            return customers;
        }

        public static bool UpdateCustomer(int customerId, string newName, int addressId, bool active, string updatedBy)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "UPDATE customer SET customerName = @name, addressId = @addressId, active = @active, lastUpdate = NOW(), lastUpdateBy = @updatedBy WHERE customerId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", newName);
                cmd.Parameters.AddWithValue("@addressId", addressId);
                cmd.Parameters.AddWithValue("@active", active);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@id", customerId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool DeleteCustomer(int customerId)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "DELETE FROM customer WHERE customerId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customerId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool InsertCustomer(string customerName, int addressId, bool active, string createdBy)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@name, @addressId, @active, NOW(), @createdBy, NOW(), @createdBy)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", customerName);
                cmd.Parameters.AddWithValue("@addressId", addressId);
                cmd.Parameters.AddWithValue("@active", active);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }

}
