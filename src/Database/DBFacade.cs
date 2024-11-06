using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Chirp.EFCore;
using Chirp.UserFacade.Chirp.Infrastructure.Chirp.Services;


public interface IDBFacade
{
    public List<CheepObject> DatabaseConnection();
}

public class DBFacade : IDBFacade
{

    private static string DBFilePath = Path.GetTempPath(); //enten /data eller bare data
    private static string DBFilePathWithFile = Path.Combine(DBFilePath + "/chirp.db");
    private Boolean cheepdataExists = false;
    public DBFacade()
    {
        if (!Directory.Exists("/tmp/data"))
        {
            Directory.CreateDirectory(DBFilePath);
            File.Create(DBFilePathWithFile);
        }
        
    }

    // This method should either be renamed or refactored such that
    // it only does one thing.
    public List<CheepObject> DatabaseConnection()
    {
        var cheepList = new List<CheepObject>();

            var context = new ChirpDBContext();
            var query = context.Cheeps.Join(context.Users,
                         cheep => cheep.AuthorId,
                         author => author.AuthorId,
                         (cheep, author) => new
                         {
                             author_id = cheep.AuthorId,
                             user_id = author.AuthorId,
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
            var query = context.Cheeps.Join(context.Users,
                        cheep => cheep.AuthorId,
                        author => author.AuthorId,
                        (cheep, author) => new
                        {
                            author_id = cheep.AuthorId,
                            user_id = author.AuthorId,
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