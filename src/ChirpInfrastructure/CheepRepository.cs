/*Check later, does this hold?
Configure the ASP.NET DI container (dependency injection container) so that instances of
CheepRepository are injected into your application wherever needed. That is, none of your views,
services, etc. has a direct dependency onto CheepRepository.*/
using System.Data;
using ChirpCore.DTOs;
using ChirpInfrastructure;
using Microsoft.EntityFrameworkCore;
using ChirpCore.Domain;


namespace ChirpRepositories;

public interface ICheepRepository
{
	/*Below commented method will be relevant later
   public Cheep CreateCheepAsync();

	 Below 2 methods will not be implemented. If developers
	 wish to implement editing or deleting of Cheeps from an Author,
	 this is where to add this functionality.
   public Cheep EditCheep();
   public void DeleteCheep();
   */
	//public Task<int> CreateCheepAsync(int userId, string userName, string text);
	public List<CheepDTO> ReadCheeps(int pageNumber);
	public List<CheepDTO> ReadCheepsFromAuthor(string author, int pageNumber);
	public Task<Author?> GetAuthorByIdAsync(int userId);
	Task<int> GenerateNextCheepIdAsync();
	Task<int> AddCheepAsync(Cheep newCheep);

}
public class CheepRepository(ChirpDBContext context) : ICheepRepository
{
	private readonly ChirpDBContext _context = context;

	/*public Cheep CreateCheepAsync(){

  }
  Above will be relevant later*/
	/* Pseudo
  function MakeCheep(newMessage)
  cheepAuthor = GetAuthorById(newMessage.AuthorID) //Get the author synchronously

  //Create a new Cheep object
  message = New Cheep
  message.CheepId = GenerateNextCheepId() //Increment the next Cheep ID
  message.UserId = newMessage.AuthorID
  message.Text = newMessage.Text
  Message.Timestamp = CurrentDateTime() //Get the current date and the time
  message.Author = cheepAuthor

  //Add the cheep object to the database context (net yet persisted)
  queryResult = AddCheepToDatabaseContext(message)

  // Add the Cheep to the author's list of Cheeps
  cheepAuthor.Cheeps.Add(message)

  // Persist changes to the database
  Await SaveChangesToDatabase()

  // Log the Cheep details
  Print("Store Cheep message = " + message.Text + " and AuthorId = " + message.Author.UserId)

  // Return the ID of the newly created Cheep
  Return queryResult.CheepId
  End Function*/

	public async Task<Author?> GetAuthorByIdAsync(int userId)
	{
		return await _context.Authors.FirstOrDefaultAsync(a => a.Id == userId);
	}

	public async Task<int> GenerateNextCheepIdAsync()
	{
		return await _context.Cheeps.AnyAsync() ? await _context.Cheeps.MaxAsync(c => c.CheepId) + 1 : 1;
	}

	public async Task<int> AddCheepAsync(Cheep newCheep)
	{
		await _context.Cheeps.AddAsync(newCheep);
		await _context.SaveChangesAsync();
		return newCheep.CheepId;
	}

	public List<CheepDTO> ReadCheeps(int pageNumber)
	{
		int pageSize = 32;

		//query for getting every cheep
		var query = _context.Cheeps.OrderByDescending(Cheepmessage => Cheepmessage.TimeStamp)
			//orders by the domainmodel timestamp, which is datetime type
			.Select(cheep => new CheepDTO( // message = domain cheep. result = cheepDTO
				cheep.CheepId,
				cheep.Id,
				 cheep.Author.UserName,
				cheep.Text,
				cheep.TimeStamp.ToString("MM/dd/yy H:mm:ss")
			))
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize);

		var result = query.ToList();

		return result;
	}

	public List<CheepDTO> ReadCheepsFromAuthor(string author, int pageNumber)
	{
		int pageSize = 32;

		//query for getting every cheep
		var query = _context.Cheeps.OrderByDescending(Cheepmessage => Cheepmessage.TimeStamp)
					.Where(Cheep => Cheep.Author.UserName == author)
					//orders by the domainmodel timestamp, which is datetime type
					.Select(cheep => new CheepDTO( // message = domain cheep. result = cheepDTO
						cheep.CheepId,
						cheep.Id,
						cheep.Author.UserName,
						cheep.Text,
						cheep.TimeStamp.ToString("MM/dd/yy H:mm:ss")
					))
					.Skip((pageNumber - 1) * pageSize)
					.Take(pageSize);

		var result = query.ToList();

		return result;
	}


}
