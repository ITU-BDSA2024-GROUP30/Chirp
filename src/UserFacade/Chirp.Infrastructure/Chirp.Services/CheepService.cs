using System.Data;
using Chirp.UserFacade.Chirp.Infrastructure.Chirp.Repositories;

public record CheepObject(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepObject> GetCheeps();
    public List<CheepObject> GetCheepsFromAuthor(string author);
}

public class CheepService (ICheepRepository repository) : ICheepService
{
    private readonly ICheepRepository _repository = repository;
    private static readonly List<CheepObject> _cheeps = [];

    public List<CheepObject> GetCheeps()
    {
        var list = _repository.ReadCheeps();

        foreach (CheepObject cheep in list)
        {
            _cheeps.Add(cheep);
        }

        return _cheeps;
    }

    // sorts cheep after the string author.
    public List<CheepObject> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        return _cheeps.Where(x => x.Author == author).ToList();
    }

}