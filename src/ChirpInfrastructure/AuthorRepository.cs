using System.Data;
using ChirpCore.DTOs;
using ChirpInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChirpRepositories;

public interface IAuthorRepository {
    public void AddAuthorToDatabase();
    public void LoginAuthor();
    public void UpdateFollowlistAuthor();
    public void DeleteAuthorFromDatabase();
}

public class AuthorRepository : IAuthorRepository {
    private readonly ChirpDBContext _context;
    public AuthorRepository(ChirpDBContext context)
    {
        _context = context;
    }
    
    //Below adds a new author to the database and logs their info for later login
    public void AddAuthorToDatabase(){}
    
    //Below method takes username/email and password and matches it with an 
    //author in the db
    public void LoginAuthor(){}

    //This method is used when an Author follows or unfollow another Author,
    //and their followlist needs to be updated.
    public void UpdateFollowlistAuthor(){}  // To avoid complicated code, maybe separate this into 2 methods: 
                                            // AddToFollowlistAuthor() and RemoveFromFollowlistAuthor() 
                                            
    //Below is a 'delete' function - delete is called 'Forget Me!' in the program
    public void DeleteAuthorFromDatabase(){}
}