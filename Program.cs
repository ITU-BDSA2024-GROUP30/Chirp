using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using System;

// method for verifying file path. 
//public void makeFileReader(string fileName){}

//void Main(string[] args){}

if (args[0]=="read") { //if prompted to 'read' Cheeps
    Parsing.readFromFile("chirp_cli_db.csv");
} else if (args[0]=="cheep"){
    Parsing.cheep(args);
}
    

public class Parsing{
    [Name("Author")][Index(0)]
    public required string Author { get; set; }
    [Name("Message")][Index(1)]
    public required string Message { get; set; }
    [Name("Timestamp")][Index(2)]
    public required long Timestamp { get; set; }

    public static void cheep(string[] args){ // rename "cheep" to ex. formattedMessage (so it becomes more readable)
        storeToFile(args, "chirp_cli_db.csv");
        readFromFile("chirp_cli_db.csv");
    }

    public static void readFromFile(string file){ //write this properly
        List<Parsing> cheepList;
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                var records = csv.GetRecords<Parsing>();
                cheepList = records.ToList();
        }

        foreach (var cheep in cheepList) {
            DateTimeOffset time = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp);
            time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(time, "Central Europe Standard Time");
            string formattedDate = time.ToString("MM/dd/yy HH:mm:ss");

            Console.WriteLine($"{cheep.Author} @ {formattedDate}: {cheep.Message}");
            Thread.Sleep(100); //creates delay between each Cheep
        }    
    }

    
    public static void storeToFile(string[] args, string file){ //write this out properly
        var author = Environment.UserName; // UserName or UserDomainName for device name
        var message = String.Join(" ",args[1..]);
        var currentTimestamp = DateTimeOffset.Now; //only gets the current time.
        long timestamp = currentTimestamp.ToUnixTimeSeconds();

        var hello = new Parsing { Author = author, Message = message, Timestamp = timestamp };

        using (var writer = new StreamWriter(file, true))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
            writer.Write("\n");
            csv.WriteRecord(hello);
        }
    }
}