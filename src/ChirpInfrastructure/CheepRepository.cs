/*Check later, does this hold?
Configure the ASP.NET DI container (dependency injection container) so that instances of
CheepRepository are injected into your application wherever needed. That is, none of your views,
services, etc. has a direct dependency onto CheepRepository.*/
using ChirpCore.Domain;
using ChirpCore.DTOs;
using ChirpInfrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;


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
	public Task<List<CheepDTO>> ReadCheepsFromFollowListAsync(string author, int pageNumber);
	public Task<List<CheepDTO>> ReadCheepsFromAuthorAsync(string AuthorName, int PageNumber);
	public Task<Author> GetAuthorFromUsernameAsync(string? Username);
	//public Task<Author?> GetAuthorByIdAsync(int userId);
	Task<int> GenerateNextCheepIdAsync();
	Task<int> AddCheepAsync(Cheep newCheep);

	public Task ForgetCheepsFromAuthorAsync(string userName);
}
public class CheepRepository : ICheepRepository
{
	private readonly ChirpDBContext _context;
	private const int pageSize = 32;
	private readonly IAuthorRepository _AuthorRepository;

	public CheepRepository(ChirpDBContext context, IAuthorRepository AuthorRepository)

	{
		_context = context;
		_AuthorRepository = AuthorRepository;
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
		//query for getting every cheep
		var query = _context.Cheeps.OrderByDescending(Cheepmessage => Cheepmessage.TimeStamp)
			//orders by the domainmodel timestamp, which is datetime type
			.Select(cheep => new CheepDTO( // message = domain cheep. result = cheepDTO
				cheep.CheepId,
				cheep.Author.UserName,
				cheep.Text,
				cheep.TimeStamp.ToString("MM/dd/yy H:mm:ss")
			))
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize);

		return query.ToList();
	}

	public async Task<List<CheepDTO>> ReadCheepsFromAuthorAsync(string AuthorName, int PageNumber)
	{
		Author Author = await GetAuthorFromUsernameAsync(AuthorName);
		if (Author == null)
		{
			throw new Exception($"Author with username '{AuthorName}' not found.");
		}
		var query = _context.Cheeps
						.Include(cheep => cheep.Author) // Ensure Author is loaded.
						.OrderByDescending(Cheepmessage => Cheepmessage.TimeStamp)
						.Where(Cheep => Cheep.Author.Id == Author.Id)
						//orders by the domainmodel timestamp, which is datetime type
						.Select(cheep => new CheepDTO( // message = domain cheep. result = cheepDTO
							cheep.CheepId,
							cheep.Author.UserName,
							cheep.Text,
							cheep.TimeStamp.ToString("MM/dd/yy H:mm:ss")
						))
						.Skip((PageNumber - 1) * pageSize)
						.Take(pageSize);
		var ListOfCheeps = await query.ToListAsync();
		return ListOfCheeps;
	}

	public async Task<List<CheepDTO>> ReadCheepsFromFollowListAsync(string AuthorName, int pageNumber)
	{
		Author AuthorToGetFrom = await GetAuthorFromUsernameAsync(AuthorName);
		var ListOfListOfCheeps = new List<List<CheepDTO>>();
		var ListOfCheeps = new List<CheepDTO>();
		foreach (Author author in AuthorToGetFrom.Follows)
		{
			if (string.IsNullOrEmpty(author?.UserName))
			{
				continue; // Skip this follower if UserName is null.
			}
			if (author.UserName == null)
			{
				throw new ArgumentNullException(author.UserName);
			}
			//query for getting every cheep
			var query = _context.Cheeps.OrderByDescending(Cheepmessage => Cheepmessage.TimeStamp)
						.Where(Cheep => Cheep.Author.Id == author.Id)
						//orders by the domainmodel timestamp, which is datetime type
						.Select(cheep => new CheepDTO( // message = domain cheep. result = cheepDTO
							cheep.CheepId,
							cheep.Author.UserName,
							cheep.Text,
							cheep.TimeStamp.ToString("MM/dd/yy H:mm:ss")
						))
						.Skip((pageNumber - 1) * pageSize)
						.Take(pageSize);
			ListOfListOfCheeps.Add([.. query]);
		}

		foreach (List<CheepDTO> cheeplist in ListOfListOfCheeps)
		{
			foreach (CheepDTO cheep in cheeplist)
			{
				ListOfCheeps.Add(cheep);
			}
		}

		return ListOfCheeps;
	}

	public async Task<Author> GetAuthorFromUsernameAsync(string? Username)
	{
		if (Username == null)
		{
			throw new ArgumentNullException(Username);
		}
		return await _context.Authors
		.Include(a => a.Follows)
		.Where(author => author.UserName == Username)
		.FirstOrDefaultAsync();
	}

	public async Task ForgetCheepsFromAuthorAsync(string userName) {
		Author LoggedInAuthor = await GetAuthorFromUsernameAsync(userName);
		var CheepsToRemove = _context.Cheeps.Where(C => C.Author!.UserName == userName);
		foreach (Cheep cheep in CheepsToRemove){
			_context.Cheeps.Remove(cheep);
		}
		
		//_context.RemoveRange(CheepsToRemove);
		await _context.SaveChangesAsync(); //would save changes in database as well
		return;
	}


}
