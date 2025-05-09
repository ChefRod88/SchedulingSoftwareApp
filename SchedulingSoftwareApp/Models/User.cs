using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
            using (var conn = Database.GetConnection())
            {
                string query = "INSERT INTO user (userName, password, active, createDate, createdBy, lastUpdate, lastUpdateBy) VALUES (@userName, @password, @active, NOW(), @createdBy, NOW(), @createdBy)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@active", active);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (var conn = Database.GetConnection())
            {
                string query = "SELECT * FROM user";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = reader.GetInt32("userId"),
                            UserName = reader.GetString("userName"),
                            Password = reader.GetString("password"),
                            Active = reader.GetBoolean("active"),
                            CreateDate = reader.GetDateTime("createDate"),
                            CreatedBy = reader.GetString("createdBy"),
                            LastUpdate = reader.GetDateTime("lastUpdate"),
                            LastUpdateBy = reader.GetString("lastUpdateBy")
                        });
                    }
                }
            }
            return users;
        }
        public static bool UpdateUser(int userId, string newUserName, string newPassword, bool active, string updatedBy)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "UPDATE user SET userName = @userName, password = @password, active = @active, lastUpdate = NOW(), lastUpdateBy = @updatedBy WHERE userId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", newUserName);
                cmd.Parameters.AddWithValue("@password", newPassword);
                cmd.Parameters.AddWithValue("@active", active);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@id", userId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static bool DeleteUser(int userId)
        {
            using (var conn = Database.GetConnection())
            {
                string query = "DELETE FROM user WHERE userId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }

}
