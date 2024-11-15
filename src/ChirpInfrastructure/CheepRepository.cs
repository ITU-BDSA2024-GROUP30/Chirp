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
  /*public Cheep CreateCheep();
  Above will be relevant later*/
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
	//var cheepList = new List<CheepDTO>();
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

	/*
	foreach (var cheep in result)
	{
	  var author = cheep.AuthorName;
	  var message = cheep.Text;
	  var timestamp = cheep.TimeStamp;
	  cheepList.Add(new CheepDTO());
	}
	return cheepList;*/
  }
}