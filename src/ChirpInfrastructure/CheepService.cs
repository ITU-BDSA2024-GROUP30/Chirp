using ChirpCore.DTOs;
using ChirpRepositories;



namespace ChirpServices;

//namespace confuses the foreach loop on line 23, but we should find a way to implement
//namespace ChirpServices;

//public record Cheep(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepDTO> GetCheeps(int pageNumber);
    public Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string author, int pageNumber);
    public Task<List<CheepDTO>> GetCheepsFromOtherAuthorAsync(string author, int pageNumber);
    
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

    public async Task<List<CheepDTO>> GetCheepsFromOtherAuthorAsync(string author, int pagenumber) {
        _cheeps.Clear();
        var list = await _repository.ReadCheepsFromFollowListAsync(author, pagenumber);
        
        //read each CheepObject from CheepRepository
        foreach (CheepDTO cheep in list)
        {
            _cheeps.Add(cheep);
        }

        return _cheeps;
        
    }
    //Sorts cheep after the string author. We use this for author timelines
    public async Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string author, int pagenumber)

    {

        _cheeps.Clear();
        var list = await _repository.ReadCheepsFromAuthorAsync(author, pagenumber);
        
        //read each CheepObject from CheepRepository
        foreach (CheepDTO cheep in list)
        {
            _cheeps.Add(cheep);
        }

        return _cheeps;
    }
}