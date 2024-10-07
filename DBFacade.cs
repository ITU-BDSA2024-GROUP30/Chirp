using System.Data;
using Microsoft.Data.Sqlite;

//namespace Chirp.Database;

public interface IDBFacade
{
    public List<CheepViewModel> DatabaseConnection();
}

public class DBFacade : IDBFacade
{
    public List<CheepViewModel> DatabaseConnection()
    {
        var cheepList = new List<CheepViewModel>();

        using (var connection = new SqliteConnection("Data Source=/tmp/chirp.db"))
        {
            connection.Open();

            var sqlQuery = "SELECT message.*, user.* FROM message, user WHERE message.author_id = user.user_id ORDER BY message.pub_date asc";

            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var dataRecord = (IDataRecord)reader;
                string author = FromAuthorIdToUserName(dataRecord.GetString(1));
                string message = dataRecord.GetString(2);
                double timestamp = dataRecord.GetDouble(3);

                cheepList.Add(new CheepViewModel(author, message, UnixTimeStampToDateTimeString(timestamp)));
            }
        }
        return cheepList;
    }

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

    private static string FromAuthorIdToUserName(string author) // 10
    {
        using (var connection = new SqliteConnection("Data Source=/tmp/chirp.db"))
        {
            connection.Open();

            var sqlQuery = "SELECT user.*, message.* FROM user, message WHERE user.user_id = " + author;
            //var username = "SELECT user_id FROM user JOIN message ON user.user_id = message.author_id WHERE user_id = " + author;
            //var username2 = "SELECT user_id FROM user WHERE user_id = " + author;

            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            using var reader1 = command.ExecuteReader();

            while (reader1.Read())
            {
                var name = reader1.GetString(1);
                return name;
            }
        }
        return "";
    }



    //command.CommandText = sqlQuery;

    /*
    using var reader = command.ExecuteReader();
    while (reader.Read())
    {

        var dataRecord = (IDataRecord)reader;
        for (int i = 0; i < dataRecord.FieldCount; i++)
        {
            Console.WriteLine($"{dataRecord.GetName(i)}: {dataRecord[i]}");
            Console.WriteLine(i);
        }

        // Retrieves all columns in an array together, instead of individually.
        Object[] values = new Object[reader.FieldCount];
        int fieldCount = reader.GetValues(values);
        for (int i = 0; i < fieldCount; i++)
        {
            Console.WriteLine($"{reader.GetName(i)}: {values[i]}");
            Console.WriteLine(i);
        }

        //var count = reader.GetString(0);
        //Console.WriteLine($"Hello, there is a total of {count} messages in the database!");
    } */
}