//Configure the ASP.NET DI container (dependency injection container) so that instances of
//CheepRepository are injected into your application wherever needed. That is, none of your views,
//services, etc. has a direct dependency onto CheepRepository.
using System.Data;
using Chirp.EFCore;
using SQLitePCL;

namespace Chirp.UserFacade.Chirp.Infrastructure.Chirp.Repositories;

public interface ICheepRepository {
    /*public Cheep CreateCheep();*/
    public List<CheepObject> ReadCheeps();

}
public class CheepRepository: ICheepRepository {
    private readonly ChirpDBContext _context;
    public CheepRepository(ChirpDBContext context) {
        _context = context;
    }
  /*public Cheep CreateCheep(){

  }*/
  public List<CheepObject> ReadCheeps(){
           var cheepList = new List<CheepObject>();

            var query = _context.Cheeps./*Join(_context.Users,
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
                         ).*/Select(message => new Message()
                         {
							 Text = message.Text,
                             AuthorID = message.AuthorId,
                             //message.pub_date,
                             AuthorName = message.Author.Name,
                             TimeStamp = message.TimeStamp
                         }).OrderBy(message => message.TimeStamp);
            var result = query.ToList();
            

            foreach (var cheep in result)
            {
                //var dataRecord = (IDataRecord)result;
                var author = cheep.AuthorName;
                //string message = dataRecord.GetString(2);
                //double timestamp = dataRecord.GetDouble(3);
                var message = cheep.Text;
                var timestamp = cheep.TimeStamp;
                cheepList.Add(new CheepObject(author, message, timestamp.ToString()));
            }
        return cheepList;
    }
/*
    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }*/
}