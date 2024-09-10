using System.ComponentModel.Design;

namespace SimpleDB;
 

interface IDatabaseRepository<T>
    {
    public IEnumerable<T> Read(int? limit = null);
    public void Store(T record);
    }

sealed class CSVDatabase<T>
{
    

}

//public record Cheep(string Author, string Message, long Timestamp);
//IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();

//var cheepTest1 = new Cheep("Henri", "Hello MAMA!", DateTimeOffset.UtcNow.ToUnixTimeSeconds());
//database.Store(cheepTest1);

//1. alt hvad der er i Program.cs nu skal herind, vi skal refactorisere det til at læse det her
//2. går ud fra program.cs skal bare have en main run methode så?
//3. vi skal lave Read herinde med enumerate
//4. vi skal lave Store herinde med T record
//når det hele spilder skal vi se om vi kan få det til at kører med CSVHelper... 