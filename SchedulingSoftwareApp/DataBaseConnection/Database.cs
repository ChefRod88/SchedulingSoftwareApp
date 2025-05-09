using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace SchedulingSoftwareApp
{
    public static class Database
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

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
