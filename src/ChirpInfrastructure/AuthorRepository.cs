using System.Data;
using ChirpCore.DTOs;
using ChirpInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChirpRepositories;

public interface IAuthorRepository {
    public void AddAuthorToDatabase();
    public void LoginAuthor();
    public void UpdateFollowlistAuthor();
    public void DeleteAuthor();
}

public class AuthorRepository (ChirpDBContext context) : IAuthorRepository {
    private readonly ChirpDBContext _context = context;
    
    //Below adds a new author to the database and logs their info for later login
    public void AddAuthorToDatabase(){}
    
    //Below method takes username/email and password and matches it with an 
    //author in the db
    public void LoginAuthor(){}

    //This method is used when an Author follows or unfollow another Author,
    //and their followlist needs to be updated.
    public void UpdateFollowlistAuthor(){}
    
    //Below is a 'delete' function - delete is called 'Forget Me!' in the program
    public void DeleteAuthor(){}
}