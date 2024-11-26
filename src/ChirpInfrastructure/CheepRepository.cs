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
  public Cheep CreateCheep();

	Below 2 methods will not be implemented. If developers
	wish to implement editing or deleting of Cheeps from an Author,
	this is where to add this functionality.
  public Cheep EditCheep();
  public void DeleteCheep();
  */
  public List<CheepDTO> ReadCheeps(int pageNumber);
	public List<CheepDTO> ReadCheepsFromAuthor(string author, int pageNumber);

}
public class CheepRepository(ChirpDBContext context) : ICheepRepository
{
	private readonly ChirpDBContext _context = context;

	/*public Cheep CreateCheep(){

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
	public async Task<int> CreateCheep(int userId, string text)
	{
		// Retrieve the author by their ID
		var author = await _context.Authors
			.FirstOrDefaultAsync(a => a.Id == userId);

		if (author == null)
		{
			throw new Exception("Author not found");
		}

		// Create a new Cheep object
		var newCheep = new Cheep
		{
			CheepId = await GenerateNextCheepIdAsync(),
			Id = userId,
			Author = author,
			Text = text,
			TimeStamp = DateTime.UtcNow
		};

		// Add the Cheep to the database context
		await _context.Cheeps.AddAsync(newCheep);

		// Add the Cheep to the author's list of Cheeps
		author.Cheeps.Add(newCheep);

		// Persist changes to the database
		await _context.SaveChangesAsync();

		// Return the ID of the newly created Cheep
		return newCheep.CheepId;
	}

	private async Task<int> GenerateNextCheepIdAsync()
	{
		// Generate the next Cheep ID
		return await _context.Cheeps.AnyAsync()
			? await _context.Cheeps.MaxAsync(c => c.CheepId) + 1
			: 1;
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
