using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

public class DBFacade
{
    // Not sure if this does exactly what I want it to,
    // or if it does it like how Helge wanted it to be done.
    public void connectingToSql()
    {
        //var sqlDBFilePath = "../tmp/chirp.db";
        var sqlQuery = @"SELECT COUNT(*) FROM message";

        using (var connection = new SqliteConnection("Data Source=/tmp/chirp.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var count = reader.GetString(0);
                Console.WriteLine($"Hello, {count}!");
            }
        }

    }
}