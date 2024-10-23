using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Chirp.EFCore;
using Microsoft.VisualBasic;

public interface IDBFacade
{
    public List<CheepObject> DatabaseConnection();
}

public class DBFacade : IDBFacade
{
    
    private string SqlDBFilePath = "data/chirp.db";
    private Boolean cheepdataExists = false;
    public DBFacade()
    {
        if (!Directory.Exists("data"))
        {
            Directory.CreateDirectory("data");
            File.Create(SqlDBFilePath);
        }
    }

    public void FillDatabase(string sqlFileName, SqliteConnection connection)
    {
        try
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            using var readerSomething = embeddedProvider.GetFileInfo(sqlFileName).CreateReadStream();
            using var sr = new StreamReader(readerSomething);
            var query = sr.ReadToEnd();

            using var command1 = new SqliteCommand(query, connection);
            command1.ExecuteNonQuery();
            Console.WriteLine("Table 'authors' created successfully. From file: " + sqlFileName);
        }
        catch (SqliteException e)
        {
            Console.WriteLine("!!!!!!!!!!!!" + e.Message);
        }
    }
    // This method should either be renamed or refactored such that
    // it only does one thing.
    public async List<CheepObject> DatabaseConnection()
    {
        var cheepList = new List<CheepObject>();

        using (var connection = new SqliteConnection("Data Source=data/chirp.db"))
        {
            connection.Open();
            // Add a statement to check what is in the data base already.
            // This is a ductape solution. It ensure we fill the chirp.db 
            // file noce, instead on every GET request.
            if (!cheepdataExists)
            {
                FillDatabase("data/schema.sql", connection);
                FillDatabase("data/dump.sql", connection);
                cheepdataExists = true;
            }

            var context = new AppDBContext(); 
            var authorQuery = context.Authors.Select(author => author.user_id);
            var query = context.Cheeps.Select(message => new{message.text, message.author_id}).Where(message => message.author_id == authorQuery).OrderBy(message => message.pub_date.asc);
            var result = await query.ToListAsync();

            //"SELECT message.*, user.* FROM message, user WHERE message.author_id = user.user_id ORDER BY message.pub_date asc";
            
            //var command = connection.CreateCommand();
            //command.CommandText = Query;

            using var reader = result.ExecuteReader();

            while (reader.Read())
            {
                var dataRecord = (IDataRecord)reader;
                string author = FromAuthorIdToUserName(dataRecord.GetString(1));
                string message = dataRecord.GetString(2);
                double timestamp = dataRecord.GetDouble(3);

                cheepList.Add(new CheepObject(author, message, UnixTimeStampToDateTimeString(timestamp)));
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

    private static string FromAuthorIdToUserName(string author)
    {
        using (var connection = new SqliteConnection("Data Source=data/chirp.db"))
        {
            connection.Open();

            var context = new AppDBContext(); 
            var query = context.Cheeps.Select(message, user => message.*, user.*).Where(user => user.user_id = author);
            var result = await query.ToListAsync();

            //var sqlQuery = "SELECT user.*, message.* FROM user, message WHERE user.user_id = " + author;
            //var username = "SELECT user_id FROM user JOIN message ON user.user_id = message.author_id WHERE user_id = " + author;
            // Below code might actually work. I changed SELECT user_id to SELECT user.*
            //var username2 = "SELECT user.* FROM user WHERE user_id = " + author;

            var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader1 = command.ExecuteReader();

            while (reader1.Read())
            {
                var name = reader1.GetString(1);
                return name;
            }
        }
        return "";
    }
}
