using MySql.Data.MySqlClient;
using System;

public class Database
{
    private static string connectionString = "server=localhost;database=SchedulingSoftware;user=sqlUser;password=NewPassw0rd!;";

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}
