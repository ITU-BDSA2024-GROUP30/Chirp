using Chirp.Cli.SimpleDB;
using CsvHelper.Configuration.Attributes;
using CommandLine;

namespace Chirp.Cli;


public static class Program
{
    //static string File = Path.Combine(Directory.GetCurrentDirectory(), "../data/chirp_cli_db.csv");
    // This Options class is where we write in all the commands we can call on our chirp application. 
    // At the moment it contains information for "read" and "cheep" 
    // (the names )
    public class Options
    {
        [Option('r', "read", Default = 100, Required = false, HelpText = "Read a cheep from the old cheeps")]
        public int WantToReadCheeps { get; set; } // named option of type scalar (it has one value connected to it)

        [Option('c', "cheep", Required = false, HelpText = "Write your own cheep and have it added to the list of cheeps")]
        public IEnumerable<string> WantToCheep { get; set; } = []; // named option of type sequence (it has a whole sequence/list connected to it.)
    }
    // This is our main method 
    public static void Main(string[] args)
    {
        // this is the part that actually parses the code. 
        Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
        {
            var csvDB = CSVDatabase<Cheep>.getInstance();

            //IDatabaseRepository<Cheep> csvDB = new CSVDatabase<Cheep>();

            // for cheeping
            if (o.WantToCheep.Count() != 0)
            {
                var cheepers = MakeCheep(o.WantToCheep);
                csvDB.Store(cheepers, "../../data/chirp_cli_db.csv"); // The path to the file changes depending   
                                                                      // on normal run or in a solution.
                                                                      // normal "../../data/chirp_cli_db.csv"
                                                                      // solution "../../../../../data/chirp_cli_db.csv"
            }

            // Code for reading:
            var cheeps = csvDB.Read(o.WantToReadCheeps, "../../data/chirp_cli_db.csv");
            UserInterface.PrintMessages(cheeps);
        });
    }

    static Cheep MakeCheep(IEnumerable<string> record) // changing string[] record to an IEnumerable
    {
        var author = Environment.UserName; // UserName or UserDomainName for device name
        var message = String.Join(" ", record);
        var currentTimestamp = DateTimeOffset.Now; //only gets the current time.
        long timestamp = currentTimestamp.ToUnixTimeSeconds();

        var recordCheep = new Cheep { Author = author, Message = message, Timestamp = timestamp };

        return recordCheep;
    }

    public record Cheep()
    {
        [Name("Author")]
        [Index(0)]
        public required string Author { get; set; }
        [Name("Message")]
        [Index(1)]
        public required string Message { get; set; }
        [Name("Timestamp")]
        [Index(2)]
        public required long Timestamp { get; set; }
    }

}

