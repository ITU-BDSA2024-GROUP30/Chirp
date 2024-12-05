using ChirpCore.DTOs;
using ChirpRepositories;




//namespace confuses the foreach loop on line 23, but we should find a way to implement
//namespace ChirpServices;

//public record Cheep(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepDTO> GetCheeps(int pageNumber);
    public List<CheepDTO> GetCheepsFromAuthor(string author, int pageNumber);
}

public class CheepService : ICheepService
{
	private readonly ICheepRepository _repository;

	public CheepService(ICheepRepository repository)
	{
		_repository = repository;
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
}