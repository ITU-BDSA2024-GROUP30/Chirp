using System.Data;

public record CheepObject(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepObject> GetCheeps();
    public List<CheepObject> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{
    // These would normally be loaded from a database for example
    private static readonly List<CheepObject> _cheeps = new();

    public List<CheepObject> GetCheeps()
    {

        var cheepDB = new DBFacade();
        var list = cheepDB.DatabaseConnection();

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

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

}
