using MySql.Data.MySqlClient;
using System;

namespace SchedulingSoftwareApp
{
    public static class Database
    {
        private static string connectionString = "server=localhost;database=SchedulingSoftware;user=sqlUser;password=NewPassw0rd!;SslMode=none;";

        public static MySqlConnection GetConnection()
        {
            try
            {
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database connection error: {ex.Message}");
            }
        }
    }
}
