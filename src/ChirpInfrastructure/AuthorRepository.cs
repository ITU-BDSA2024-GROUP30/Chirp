using ChirpCore.Domain;
using ChirpInfrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace ChirpRepositories;

public interface IAuthorRepository
{
	public void AddAuthorToDatabase();
	public void LoginAuthor();
	public Task DeleteAuthorFromDatabaseAsync(string UserName);
	public Task<Author> GetAuthorFromUsername(string Username);
	public Task<Boolean> IsFollowing(string LoggedInAuthorUsername, string AuthorToFollowUsername);
	public Task AddAuthorToFollowList(string loggedInAuthorUsername, string authorToFollowUsername);
	public Task RemoveAuthorFromFollowList(string loggedInAuthorUsername, string authorToFollowUsername);

	public Task<List<string>> GetFollowlistAsync(string Username);

	//public Task RemoveAuthor(string Username);
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

	public async Task DeleteAuthorFromDatabaseAsync(string Username)
	{
		Author AuthorToDelete = await GetAuthorFromUsername(Username);
		_context.Authors.Remove(AuthorToDelete);

		var query = _context.Authors.Where(A => A.Follows.Contains(AuthorToDelete));

		foreach (Author author in query) {
			author.Follows.Remove(AuthorToDelete);
		}

		await _context.SaveChangesAsync();
	}

	public async Task<Author> GetAuthorFromUsername(string? Username)
	{
		if (Username == null)
		{
			throw new ArgumentNullException(Username);
		}
		return await _context.Authors.Include(A => A.Follows).Where(Author => Author.UserName == Username).FirstAsync();
	}

	public async Task<Boolean> IsFollowing(string? LoggedInAuthorUsername, string? AuthorToFollowUsername)
	{

		Author? LoggedInAuthor = await GetAuthorFromUsername(LoggedInAuthorUsername);

		Author? AuthorToFollow = await GetAuthorFromUsername(AuthorToFollowUsername);
		if (LoggedInAuthorUsername == null || AuthorToFollowUsername == null)
		{
			throw new ArgumentNullException("Usernames null");
		}

		//Author LoggedInAuthor = await GetAuthorFromUsername(LoggedInAuthorUsername);
		//Author AuthorToFollow = await GetAuthorFromUsername(AuthorToFollowUsername);

		if (LoggedInAuthor.Follows.Contains(AuthorToFollow))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public async Task AddAuthorToFollowList(string LoggedInAuthorUsername, string AuthorToFollowUsername)
	{
		Author LoggedInAuthor = await GetAuthorFromUsername(LoggedInAuthorUsername);
		Author AuthorToFollow = await GetAuthorFromUsername(AuthorToFollowUsername);

		LoggedInAuthor.Follows.Add(AuthorToFollow);
		await _context.SaveChangesAsync();
	}

	public async Task RemoveAuthorFromFollowList(string LoggedInAuthorUsername, string AuthorToUnfollowUsername)
	{
		Author LoggedInAuthor = await GetAuthorFromUsername(LoggedInAuthorUsername);
		Author AuthorToUnfollow = await GetAuthorFromUsername(AuthorToUnfollowUsername);
		LoggedInAuthor.Follows.Remove(AuthorToUnfollow);
		await _context.SaveChangesAsync();
	}

	public async Task<List<string>> GetFollowlistAsync(string Username){
		List<string> FollowlistUsernames = new List<string>();
		Author Me = await GetAuthorFromUsername(Username);
		foreach (Author author in Me.Follows){
			FollowlistUsernames.Add(author.UserName);
		}
		return FollowlistUsernames;
	}
}