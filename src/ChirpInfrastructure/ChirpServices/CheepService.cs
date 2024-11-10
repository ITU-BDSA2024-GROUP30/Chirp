using System.Data;
using ChirpRepositories;
using ChirpCore.DTOs;


//namespace Chirp.UserFacade.Chirp.Infrastructure.Chirp.Services;
//namespace confuses the foreach loop on line 23, but we should find a way to implement


//public record Cheep(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepDTO> GetCheeps();
    public List<CheepDTO> GetCheepsFromAuthor(string author);
}

public class CheepService(ICheepRepository repository) : ICheepService
{
    private readonly ICheepRepository _repository = repository;
    private static readonly List<CheepDTO> _cheeps = [];

    public List<CheepDTO> GetCheeps()
    {
        var list = _repository.ReadCheeps();

        //read each CheepObject from CheepRepository
        foreach (CheepDTO cheep in list)
        {
            _cheeps.Add(cheep);
        }

        return _cheeps;
    }

    //Sorts cheep after the string author. We use this for author timelines
    public List<CheepDTO> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name (will this cause problems if 2 authors share the same name?)
        return _cheeps.Where(x => x.AuthorName == author).ToList();
    }

}