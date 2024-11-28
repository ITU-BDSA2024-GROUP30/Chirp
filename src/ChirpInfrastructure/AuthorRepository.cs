using System.Data;
using ChirpCore.Domain;
using ChirpInfrastructure;


namespace ChirpRepositories;

public interface IAuthorRepository
{
	public void AddAuthorToDatabase();
	public void LoginAuthor();
	public void DeleteAuthorFromDatabase();
	public Author GetAuthorFromUsername(string Username);
	public Boolean IsFollowing(string LoggedInAuthorUsername, string AuthorToFollowUsername);
  	public void AddAuthorToFollowList(string loggedInAuthorUsername, string authorToFollowUsername);
  	public void RemoveAuthorFromFollowList(string loggedInAuthorUsername, string authorToFollowUsername);
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


	//this method is used when an Author unfollows another Author

	public void DeleteAuthorFromDatabase() { }

	public Author GetAuthorFromUsername(string Username)
	{
		if (Username == null){
			throw new ArgumentNullException(Username);
		}
		Author LoggedInAuthor = _context.Authors.Where(Author => Author.UserName == Username).First();
		
		return LoggedInAuthor;
	}
	public Boolean IsFollowing(string LoggedInAuthorUsername, string AuthorToFollowUsername)
	{
		if (LoggedInAuthorUsername == null || AuthorToFollowUsername == null) {
			throw new ArgumentNullException("Usernames null");
		}
		Author LoggedInAuthor = GetAuthorFromUsername(LoggedInAuthorUsername);
		Author AuthorToFollow = GetAuthorFromUsername(AuthorToFollowUsername);
		if (LoggedInAuthor.Follows.Contains(AuthorToFollow))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

  public void AddAuthorToFollowList(string loggedInAuthorUsername, string authorToFollowUsername)
  {
	Author LoggedInAuthor = GetAuthorFromUsername(loggedInAuthorUsername);
	LoggedInAuthor.Follows.Add(GetAuthorFromUsername(authorToFollowUsername));
  }

  public void RemoveAuthorFromFollowList(string loggedInAuthorUsername, string authorToFollowUsername)
  {
	Author LoggedInAuthor = GetAuthorFromUsername(loggedInAuthorUsername);
	LoggedInAuthor.Follows.Remove(GetAuthorFromUsername(authorToFollowUsername));
  }
}