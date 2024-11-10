using System.Data;
using Chirp.ChirpInfrastructure.Chirp.Repositories;
using ChirpCore;


//namespace Chirp.UserFacade.Chirp.Infrastructure.Chirp.Services;
//namespace confuses the foreach loop on line 23, but we should find a way to implement
public record CheepObject(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepObject> GetCheeps();
    public List<CheepObject> GetCheepsFromAuthor(string author);
}

public class CheepService(ICheepRepository repository) : ICheepService
{
    private readonly ICheepRepository _repository = repository;
    private static readonly List<CheepObject> _cheeps = [];

    public List<CheepObject> GetCheeps()
    {
        var list = _repository.ReadCheeps();

        //read each CheepObject from CheepRepository
        foreach (CheepObject cheep in list)
        {
            _cheeps.Add(cheep);
        }

        return _cheeps;
    }

    //Sorts cheep after the string author. We use this for author timelines
    public List<CheepObject> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        return _cheeps.Where(x => x.Author == author).ToList();
    }

}