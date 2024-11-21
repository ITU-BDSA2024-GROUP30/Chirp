/*Check later, does this hold?
Configure the ASP.NET DI container (dependency injection container) so that instances of
CheepRepository are injected into your application wherever needed. That is, none of your views,
services, etc. has a direct dependency onto CheepRepository.*/
using System.Data;
using ChirpCore.DTOs;
using ChirpInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChirpRepositories;

public interface ICheepRepository
{
  /*Below commented method will be relevant later
  public Cheep CreateCheep();

	Below 2 methods will not be implemented. If developers
	wish to implement editing or deleting of Cheeps from an Author,
	this is where to add this functionality.
  public Cheep EditCheep();
  public void DeleteCheep();
  */
  public List<CheepDTO> ReadCheeps(int pageNumber);

}
public class CheepRepository(ChirpDBContext context) : ICheepRepository
{
  private readonly ChirpDBContext _context = context;

  /*public Cheep CreateCheep(){

  }
  Above will be relevant later*/
  public List<CheepDTO> ReadCheeps(int pageNumber)
  {
	//defining number of cheeps per page
	int pageSize = 32;

	//query for getting every cheep
	var query = _context.Cheeps.OrderByDescending(Cheepmessage => Cheepmessage.TimeStamp)
	//orders by the domainmodel timestamp, which is datetime type
	.Select(message => new CheepDTO() // message = domain cheep. result = cheepDTO
	{
	  Text = message.Text,
	  AuthorID = message.AuthorId,
	  AuthorName = message.Author.Name,
	  TimeStamp = message.TimeStamp.ToString("MM/dd/yy H:mm:ss") 
	})
	.Skip((pageNumber - 1) * pageSize)
	.Take(pageSize);

	var result = query.ToList();

	return result;
  }

	/*Below 2 methods will not be implemented. If developers
	wish to implement editing or deleting of Cheeps from an Author,
	this is where to add this functionality.*/
	//public Cheep EditCheep();
  	//public void DeleteCheep();

}