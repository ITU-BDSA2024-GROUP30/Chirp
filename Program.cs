using CsvHelper.Configuration.Attributes;

if (args[0]=="read") { //if prompted to 'read' Cheeps
    IDatabaseRepository<Cheep> csvDB = new SimpleDB.CSVDatabase<Cheep>();
    //ovenstående - skift til var eller lign? I stedet for IDatabase....
        
    if (args.Length > 1){
        int argument = Int32.Parse(args[1]);
        var cheepList = csvDB.Read(argument, "chirp_cli_db.csv");
        for (int i = 0; i < argument ; i++){
            Cheep cheep = cheepList.ElementAt(i);
            DateTimeOffset time = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp);
            time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(time, "Central Europe Standard Time");
            string formattedDate = time.ToString("MM/dd/yy HH:mm:ss");
    
            Console.WriteLine($"{cheep.Author} @ {formattedDate}: {cheep.Message}");
            Thread.Sleep(100); //creates delay between each Cheep
        }
    } else {
        var argument = 100; //we need to change this. what limit to give,
        //when there is none given from user input?

        var cheepList = csvDB.Read(argument, "chirp_cli_db.csv");
        foreach (Cheep cheep in cheepList) {
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

    //below is code duplication, can we simplify it?
    var argument = 100; //we need to change this. what limit to give,
        //when there is none given from user input?

    var cheepList = csvDB.Read(argument, "chirp_cli_db.csv");
    foreach (Cheep cheep in cheepList) {
        DateTimeOffset time = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp);
        time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(time, "Central Europe Standard Time");
        string formattedDate = time.ToString("MM/dd/yy HH:mm:ss");
    
        Console.WriteLine($"{cheep.Author} @ {formattedDate}: {cheep.Message}");
        Thread.Sleep(100); //creates delay between each Cheep
    }
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
}