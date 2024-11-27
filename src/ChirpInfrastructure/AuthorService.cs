using System.Data;
using ChirpRepositories;
using ChirpCore.DTOs;

public interface IAuthorService
{
	public void RegisterAuthor();
	public void LoginAuthor();
	public void ForgetAuthor();
	public void FollowAuthor(string LoggedInAuthor, string AuthorToFollow);
	public void UnfollowAuthor(string LoggedInAuthor, string AuthorToFollow);

	public bool IsFollowing(string LoggedInAuthor, string AuthorToFollow);
}

public class AuthorService : IAuthorService
{
	private readonly IAuthorService _repository;
	public AuthorService(IAuthorService repository)
	{
		//used when methods underneathe will be implemented
		_repository = repository;
	}
	//Used when an Author initially registers for our website.
	//This method needs to invoke AddAuthor() from AuthorRepo, in order to add
	//this new Author to our database
	public void RegisterAuthor() { }

	//This method is used when an Author is already registrered and tries to login.
	public void LoginAuthor()
	{
		//needs to call on authentication and authorization for identity user
	}

	//This method is invoked when an Author clicks the 'Forget Me!' button.
	//Needs to call upon DeleteAuthorFromDatabase() from AuthorRepo to remove Author from DB.
	public void ForgetAuthor() { }

	//Method for adding another Author to acting Author's follower list
	//Probably calls on UpdateAuthor() from AuthorRepo
	public Task FollowAuthor(string LoggedInAuthor, string AuthorToFollow)
	{
		return _repository.FollowAuthor(LoggedInAuthor, AuthorToFollow);
	}

	//Method for removing another Author from acting Author's follower list
	//Probably calls on UpdateAuthor() from AuthorRepo
	public Task UnfollowAuthor(string LoggedInAuthor, string AuthorToFollow) {
		return _repository.UnfollowAuthor(LoggedInAuthor, AuthorToFollow);
	}

	public Task<bool> IsFollowing(string LoggedInAuthor, string AuthorToFollow)
	{
		return _repository.IsFollowing(LoggedInAuthor, AuthorToFollow);
	}

}