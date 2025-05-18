using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchedulingSoftwareApp
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }

        public static bool InsertUser(string userName, string password, bool active, string createdBy)
        {
            try
            {
                userName = string.IsNullOrWhiteSpace(userName) ? "UnknownUser" : userName.Trim();
                password = string.IsNullOrWhiteSpace(password) ? "password123" : password.Trim();
                createdBy = string.IsNullOrWhiteSpace(createdBy) ? "System" : createdBy.Trim();

                using (var conn = Database.GetConnection())
                {
                    string query = @"
                INSERT INTO user (userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy)
                VALUES (@userName, @password, @active, NOW(), @createdBy, NOW(), @createdBy)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userName", userName);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@active", active);
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting user: {ex.Message}", "Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT * FROM user";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserId = reader["userId"] != DBNull.Value ? Convert.ToInt32(reader["userId"]) : 0,
                                UserName = reader["userName"]?.ToString()?.Trim() ?? "UnknownUser",
                                Password = reader["password"]?.ToString()?.Trim() ?? "password123",
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
            return users;
        }

        public static bool UpdateUser(int userId, string newUserName, string newPassword, bool active, string updatedBy)
        {
            try
            {
                if (userId <= 0)
                {
                    MessageBox.Show("Invalid User ID. Cannot update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                newUserName = string.IsNullOrWhiteSpace(newUserName) ? "UnknownUser" : newUserName.Trim();
                newPassword = string.IsNullOrWhiteSpace(newPassword) ? "password123" : newPassword.Trim();
                updatedBy = string.IsNullOrWhiteSpace(updatedBy) ? "System" : updatedBy.Trim();

                using (var conn = Database.GetConnection())
                {
                    string query = @"
                UPDATE user
                SET userName = @userName, password = @password, active = @active,
                    lastUpdate = NOW(), lastUpdateBy = @updatedBy
                WHERE userId = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userName", newUserName);
                        cmd.Parameters.AddWithValue("@password", newPassword);
                        cmd.Parameters.AddWithValue("@active", active);
                        cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                        cmd.Parameters.AddWithValue("@id", userId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool DeleteUser(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    MessageBox.Show("Invalid User ID. Cannot delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (var conn = Database.GetConnection())
                {
                    string query = "DELETE FROM user WHERE userId = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", userId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


    }

}
