using System.Data;
using ChirpCore.Domain;
using ChirpInfrastructure;


namespace ChirpRepositories;

public interface IAuthorRepository
{
	public void AddAuthorToDatabase();
	public void LoginAuthor();
	public void AddAuthorToFollowlist(string LoggedInAuthorUsername, string AuthorToFollowUsername);
	public void RemoveAuthorFromFollowlist(string LoggedInAuthorUsername, string AuthorToFollowUsername);
	public void DeleteAuthorFromDatabase();
	public Author GetAuthorFromUsername(string Username);
}

public class AuthorRepository : IAuthorRepository
{
	private readonly ChirpDBContext _context;
	public AuthorRepository(ChirpDBContext context)
	{
		_context = context;
	}

	//Below adds a new author to the database and logs their info for later login
	public void AddAuthorToDatabase() { }

	//Below method takes username/email and password and matches it with an
	//author in the db
	public void LoginAuthor() { }

	//This method is used when an Author follows another Author,
	//and their followlist needs to be updated.
	public void AddAuthorToFollowlist(string LoggedInAuthorUsername, string AuthorToFollowUsername)
	{
		Author LoggedInAuthor = GetAuthorFromUsername(LoggedInAuthorUsername);
		LoggedInAuthor.Follows.Add(GetAuthorFromUsername(AuthorToFollowUsername));
	}


	//this method is used when an Author unfollows another Author
	public void RemoveAuthorFromFollowlist(string LoggedInAuthorUsername, string AuthorToFollowUsername)
	{
		Author LoggedInAuthor = GetAuthorFromUsername(LoggedInAuthorUsername);
		LoggedInAuthor.Follows.Remove(GetAuthorFromUsername(AuthorToFollowUsername));
	}

	public void DeleteAuthorFromDatabase() { }

	public Author GetAuthorFromUsername(string Username)
	{
		Author LoggedInAuthor = _context.Authors
		.Select(Author => Author.Name == Username);

		return LoggedInAuthor;
	}
}