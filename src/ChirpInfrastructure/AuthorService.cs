using System.Data;
using ChirpRepositories;
using ChirpCore.DTOs;

namespace ChirpServices;

public interface IAuthorService {
    public void RegisterAuthor();
    public void LoginAuthor();
    public void ForgetAuthor();
    public void FollowAuthor();
    public void UnfollowAuthor();
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
    public void RegisterAuthor(){}

    //This method is used when an Author is already registrered and tries to login.
    public void LoginAuthor(){
        //needs to call on authentication and authorization for identity user
    }

    //This method is invoked when an Author clicks the 'Forget Me!' button.
    //Needs to call upon DeleteAuthorFromDatabase() from AuthorRepo to remove Author from DB.
    public void ForgetAuthor(){}

    //Method for adding another Author to acting Author's follower list
    //Probably calls on UpdateAuthor() from AuthorRepo
    public void FollowAuthor(){}

    //Method for removing another Author from acting Author's follower list
    //Probably calls on UpdateAuthor() from AuthorRepo
    public void UnfollowAuthor(){}

}