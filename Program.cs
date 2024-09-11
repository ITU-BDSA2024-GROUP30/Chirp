using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using System;
using SimpleDB;

// method for verifying file path. 
//public void makeFileReader(string fileName){}

//void Main(string[] args){}


if (args[0]=="read") { //if prompted to 'read' Cheeps
    //Parsing.readFromFile("chirp_cli_db.csv");
    if (args.Length > 1){
        int argument = Int32.Parse(args[1]);
        IDatabaseRepository<Cheep> csvDB = new SimpleDB.CSVDatabase<Cheep>();
        foreach (var cheep in csvDB.Read(argument, "chirp_cli_db.csv")) {
            DateTimeOffset time = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp);
            time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(time, "Central Europe Standard Time");
            string formattedDate = time.ToString("MM/dd/yy HH:mm:ss");
    
            Console.WriteLine($"{cheep.Author} @ {formattedDate}: {cheep.Message}");
            Thread.Sleep(100); //creates delay between each Cheep
        }
    }
    
} else if (args[0]=="cheep"){
    IDatabaseRepository<Cheep> csvDB = new SimpleDB.CSVDatabase<Cheep>();
    var cheepers = makeCheep(args[1..]);
    csvDB.Store(cheepers, "chirp_cli_db.csv");
    //Parsing.cheep(args);
}

Cheep makeCheep(string[] record){
    var author = Environment.UserName; // UserName or UserDomainName for device name
    var message = String.Join(" ", record);
    var currentTimestamp = DateTimeOffset.Now; //only gets the current time.
    long timestamp = currentTimestamp.ToUnixTimeSeconds();

    var recordCheep = new Cheep { Author = author, Message = message, Timestamp = timestamp };

    return recordCheep;
}

public record Cheep() {
    [Name("Author")][Index(0)]
    public required string Author { get; set; }
    [Name("Message")][Index(1)]
    public required string Message { get; set; }
    [Name("Timestamp")][Index(2)]
    public required long Timestamp { get; set; }
    //public static void cheep(string[] args){ // rename "cheep" to ex. formattedMessage (so it becomes more readable)
    //    storeToFile(args, "chirp_cli_db.csv");
    //    readFromFile("chirp_cli_db.csv");
    //}
    
}



/* public record Cheep(string Author, string Message, long Timestamp);
IDatabaseRepository<Cheep> databaseTest = new CSVDatabase<Cheep>();

var cheepTest1 = new Cheep("Henri", "Hello MAMA!", DateTimeOffset.UtcNow.ToUnixTimeSeconds());
databaseTest.Store(cheepTest1);
cheepTest1.Read(); */