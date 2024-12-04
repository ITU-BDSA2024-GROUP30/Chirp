using System.Data;
using ChirpRepositories;
using ChirpCore.DTOs;
using ChirpCore.Domain;
using ChirpInfrastructure;
using Microsoft.EntityFrameworkCore;

//namespace confuses the foreach loop on line 23, but we should find a way to implement

//public record Cheep(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepDTO> GetCheeps(int pageNumber);
    public List<CheepDTO> GetCheepsFromAuthor(string author, int pageNumber);
    public Task<int> CreateCheepAsync(int userId, string userName, string text);
}

public class CheepService : ICheepService//(ICheepRepository repository) : ICheepService
{
    private readonly ICheepRepository _repository;
    private readonly ChirpDBContext _context;
    public CheepService(ICheepRepository repository, ChirpDBContext context)
    {
        _repository = repository;
        _context = context;
    }
      private static readonly List<CheepDTO> _cheeps = [];

    public List<CheepDTO> GetCheeps(int pageNumber)
    {
        _cheeps.Clear();
        var list = _repository.ReadCheeps(pageNumber);

        //read each CheepObject from CheepRepository
        foreach (CheepDTO cheep in list)
        {
            _cheeps.Add(cheep);
        }

        return _cheeps;
    }

    //Sorts cheep after the string author. We use this for author timelines
    public List<CheepDTO> GetCheepsFromAuthor(string author, int pageNumber) 
    {

        _cheeps.Clear();
        var list = _repository.ReadCheepsFromAuthor(author, pageNumber);

        //read each CheepObject from CheepRepository
        foreach (CheepDTO cheep in list)
        {
            _cheeps.Add(cheep);
        }

        return _cheeps;
        // Below is the old code
        // filter by the provided author name (will this cause problems if 2 authors share the same name?)
        //return _cheeps.Where(x => x.UserName == author).ToList();    
    }

    public async Task<int> CreateCheepAsync(int userId, string userName, string text)
    {
        /*// Retrieve the author by their ID
		var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == userId);

		if (author == null)
		{
			throw new Exception("Author not found");
		}

		// Create a new Cheep object
		var newCheep = new Cheep
		{
			CheepId = await GenerateNextCheepIdAsync(),
			Id = userId,
			Author = author,
			Text = text,
			TimeStamp = DateTime.UtcNow
		};

		// Add the Cheep to the database context
		await _context.Cheeps.AddAsync(newCheep);

		// Add the Cheep to the author's list of Cheeps
		author.Cheeps.Add(newCheep);

		// Persist changes to the database
		await _context.SaveChangesAsync();

		// Return the ID of the newly created Cheep
		return newCheep.CheepId;*/
        Console.WriteLine($"Creating cheep for user {userId} with text: {text}");

        // If the user is anonymous, we don't associate them with an Author.
        Author? author = null;
        if (userId != 0)
        {
            author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == userId);
            if (author == null)
            {
                throw new Exception("Author not found");
            }
        }

        // Create a new Cheep object
        var newCheep = new Cheep
        {
            CheepId = await _repository.GenerateNextCheepIdAsync(),
            Id = userId,
            Author = author,
            Text = text,
            TimeStamp = DateTime.UtcNow
        };

        if (author != null)
        {
            author.Cheeps.Add(newCheep);
        }

        // Add the Cheep to the database context
        await _context.Cheeps.AddAsync(newCheep);
        await _context.SaveChangesAsync();
        Console.WriteLine("Cheep successfully saved to database.");
        return newCheep.CheepId;
    }

}