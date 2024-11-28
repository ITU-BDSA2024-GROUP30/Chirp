/*Check later, does this hold?
Configure the ASP.NET DI container (dependency injection container) so that instances of
CheepRepository are injected into your application wherever needed. That is, none of your views,
services, etc. has a direct dependency onto CheepRepository.*/
using System.Data;
using ChirpCore.DTOs;
using ChirpInfrastructure;
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
	public List<CheepDTO> ReadCheepsFromFollowList(string author, int pageNumber);

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
	/*public Cheep CreateCheep(){

		}
		Above will be relevant later*/
	public List<CheepDTO> ReadCheeps(int pageNumber)
	{

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


		return query.ToList();
	}

	public List<CheepDTO> ReadCheepsFromFollowList(string AuthorName, int pageNumber)
	{
		Author AuthorToGetFrom = GetAuthorFromUsername(AuthorName);
		var ListOfListOfCheeps = new List<List<CheepDTO>>();
		var ListOfCheeps = new List<CheepDTO>();
		foreach (Author author in AuthorToGetFrom.Follows)
		{
			if (author.UserName == null){
				throw new ArgumentNullException(author.UserName);
			}
			//query for getting every cheep
			var query = _context.Cheeps.OrderByDescending(Cheepmessage => Cheepmessage.TimeStamp)
						.Where(Cheep => Cheep.Id == author.Id)
						//orders by the domainmodel timestamp, which is datetime type
						.Select(cheep => new CheepDTO( // message = domain cheep. result = cheepDTO
							cheep.CheepId,
							cheep.Id,
							author.UserName,
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

	public Author GetAuthorFromUsername(string Username)
	{
		if (Username == null){
			throw new ArgumentNullException(Username);
		}
		Author LoggedInAuthor = _context.Authors.Where(Author => Author.UserName == Username).First();
		
		return LoggedInAuthor;
	}
}
