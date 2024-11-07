/*Check later, does this hold?
Configure the ASP.NET DI container (dependency injection container) so that instances of
CheepRepository are injected into your application wherever needed. That is, none of your views,
services, etc. has a direct dependency onto CheepRepository.*/
using System.Data;
using Chirp.EFCore;

namespace Chirp.UserFacade.Chirp.Infrastructure.Chirp.Repositories;

public interface ICheepRepository {
    /*public Cheep CreateCheep();
    Above will be relevant later*/
    public List<CheepObject> ReadCheeps();

}
public class CheepRepository(ChirpDBContext context) : ICheepRepository {
    private readonly ChirpDBContext _context = context;

  /*public Cheep CreateCheep(){

  }
  Above will be relevant later*/
  public List<CheepObject> ReadCheeps(){
            var cheepList = new List<CheepObject>();
            
            //query for getting every cheep
            var query = _context.Cheeps.Select(message => new Message()
                         {
							 Text = message.Text,
                             AuthorID = message.AuthorId,
                             AuthorName = message.Author.Name,
                             TimeStamp = message.TimeStamp
                         }).OrderBy(message => message.TimeStamp);
            var result = query.ToList();
            

            foreach (var cheep in result)
            {
                var author = cheep.AuthorName;
                var message = cheep.Text;
                var timestamp = cheep.TimeStamp;
                cheepList.Add(new CheepObject(author, message, timestamp.ToString()));
            }
        return cheepList;
    }
}