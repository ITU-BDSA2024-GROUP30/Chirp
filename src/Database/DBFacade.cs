using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Chirp.EFCore;

public interface IDBFacade
{
    public List<CheepObject> DatabaseConnection();
}

public class DBFacade : IDBFacade
{

    private static string DBFilePath = Path.Combine(Path.GetTempPath(), "data"); //enten /data eller bare data
    private static string DBFilePathWithFile = Path.Combine(DBFilePath + "/chirp.db");
    // previously: DBFilePath = "data/chirp.db";
    private Boolean cheepdataExists = false;
    public DBFacade()
    {
        //if (!Directory.Exists("data"))
        {
            Directory.CreateDirectory("data");
            File.Create(DBFilePath);
        }
    }

    /*public void FillDatabase(string FileName)
    {
        try
        {
            var context = new ChirpDBContext();
            context.Execute(DBInitializer());

            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            using var readerSomething = embeddedProvider.GetFileInfo(FileName).CreateReadStream();
            using var sr = new StreamReader(readerSomething);
            var query = sr.ReadToEnd();

            //using (var context = new ChirpDBContext("Data Source=" + DBFilePathWithFile)) //Denne er den nye og rigtige
            using (var context = new ChirpDBContext())
            {
                context.Database.ExecuteSqlRaw(query);
            }
            Console.WriteLine("Table 'authors' created successfully. From file: " + FileName);
        }
        catch (SqliteException e)
        {
            Console.WriteLine("!!!!!!!!!!!!" + e.Message);
        }
    }*/
    // This method should either be renamed or refactored such that
    // it only does one thing.
    public List<CheepObject> DatabaseConnection()
    {
        var cheepList = new List<CheepObject>();

        //using (var context = new ChirpDBContext())
        //{
            // Add a statement to check what is in the data base already.
            // This is a ductape solution. It ensure we fill the chirp.db 
            // file noce, instead on every GET request.
            /*if (!cheepdataExists)
            {
                FillDatabase("data/schema.sql");
                FillDatabase("data/dump.sql");
                cheepdataExists = true;
            }*/

            var context = new ChirpDBContext();
            var query = context.Messages.Join(context.Users,
                         cheep => cheep.UserId,
                         author => author.UserId,
                         (cheep, author) => new
                         {
                             author_id = cheep.UserId,
                             user_id = author.UserId,
                             text = cheep.Text,
                             //pub_date = cheep.pub_date,
                             username = author.Name
                         }
                         ).Select(message => new
                         {
                             message.author_id,
                             message.text,
                             //message.pub_date,
                             message.username
                         }); //).OrderBy(message => message.pub_date);
            var result = query.ToList();

            foreach (var cheep in result)
            {
                var dataRecord = (IDataRecord)result;
                var author = cheep.username;
                string message = dataRecord.GetString(2);
                double timestamp = dataRecord.GetDouble(3);

                cheepList.Add(new CheepObject(author, message, UnixTimeStampToDateTimeString(timestamp)));
            }
        //}
        return cheepList;
    }

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

    private static async Task<string> FromAuthorIdToUserNameAsync (string author)
    {
        //using (var context = new ChirpDBContext())
        //{
            var context = new ChirpDBContext();
            var query = context.Messages.Join(context.Users,
                        cheep => cheep.UserId,
                        author => author.UserId,
                        (cheep, author) => new
                        {
                            author_id = cheep.UserId,
                            user_id = author.UserId,
                            username = author.Name,
                            text = cheep.Text
                        }
                        ).Select(message => new
                        {
                            message.author_id,
                            message.text,
                            message.username
                        });
            var result = await query.ToListAsync();


            foreach (var cheep in result)
            {
                return cheep.username;
            }

        return "";
    }
}
//}