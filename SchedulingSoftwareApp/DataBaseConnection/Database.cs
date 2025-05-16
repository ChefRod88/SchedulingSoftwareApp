using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace SchedulingSoftwareApp
{
    public static class Database
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

        // Using a lambda expression for creating and opening the connection
        //Func<Type> is a delegate type representing a method that returns a value of type T,
        //in this case the type is MySqlConnection
        public static Func<MySqlConnection> GetConnection = () =>
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
        };
    }
}
