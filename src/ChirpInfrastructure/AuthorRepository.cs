using ChirpCore.Domain;
using ChirpInfrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ChirpRepositories;

/// <summary>
/// Defines methods for handling authors
/// </summary>
public interface IAuthorRepository
{
	public Task DeleteAuthorFromDatabaseAsync(string UserName);
	/// <summary>
	/// Gets an Username from a given IdentityUser which is a author
	/// </summary>
	/// <param name="Username"></param>
	/// <returns></returns>
	public Task<Author> GetAuthorFromUsername(string Username);
	/// <summary>
	/// checks for if author true or false follows another author
	/// </summary>
	/// <param name="LoggedInAuthorUsername"></param>
	/// <param name="AuthorToFollowUsername"></param>
	/// <returns></returns>
	public Task<Boolean> IsFollowing(string LoggedInAuthorUsername, string AuthorToFollowUsername);
	/// <summary>
	/// Adds author to given author followerlist
	/// </summary>
	/// <param name="loggedInAuthorUsername"></param>
	/// <param name="authorToFollowUsername"></param>
	/// <returns></returns>
	public Task AddAuthorToFollowList(string loggedInAuthorUsername, string authorToFollowUsername);
	/// <summary>
	/// Removes author to given author followerlist
	/// </summary>
	/// <param name="loggedInAuthorUsername"></param>
	/// <param name="authorToFollowUsername"></param>
	/// <returns></returns> <summary>
	///
	/// </summary>
	/// <param name="loggedInAuthorUsername"></param>
	/// <param name="authorToFollowUsername"></param>
	/// <returns></returns>
	public Task RemoveAuthorFromFollowList(string loggedInAuthorUsername, string authorToFollowUsername);
	/// <summary>
	/// Gets the followerlist for given Username, which is a specific author
	/// </summary>
	/// <param name="Username"></param>
	/// <returns></returns>
	public Task<List<string>> GetFollowlistAsync(string Username);

}

/// <summary>
/// Used handling logic of author
/// Includes methods for handling and accessing author data
/// </summary>
public class AuthorRepository : IAuthorRepository
{
	private readonly ChirpDBContext _context;
	public AuthorRepository(ChirpDBContext context)
	{
		_context = context;
	}

	//this method is used when an Authors followlist needs to be updated
	public async Task DeleteAuthorFromDatabaseAsync(string Username)
	{
		Author AuthorToDelete = await GetAuthorFromUsername(Username);
		_context.Authors.Remove(AuthorToDelete);

		var query = _context.Authors.Where(A => A.Follows.Contains(AuthorToDelete));

		foreach (Author author in query)
		{
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

	public async Task<List<string>> GetFollowlistAsync(string Username)
	{
		List<string> FollowlistUsernames = new List<string>();
		Author Me = await GetAuthorFromUsername(Username);
		foreach (Author author in Me.Follows)
		{
			FollowlistUsernames.Add(author.UserName);
		}
		return FollowlistUsernames;
	}
}