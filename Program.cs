using System.IO;
using System.Text.RegularExpressions;

StreamReader input = new("chirp_cli_db.csv"); //reads from file
StreamWriter output = File.AppendText("chirp_cli_db.csv"); //writes to file

List<string> cheeps = []; //list of Cheeps to print
var nextLine = input.ReadLine(); //first line from file; unused on purpose
while ((nextLine = input.ReadLine()) != null) { //while there is a nextLine, add to list of Cheeps
   cheeps.Add(nextLine);
}

if (args[0]=="read") { //if prompted to 'read' Cheeps
        read();
} else if (args[0]=="cheep"){
    var authorname = Environment.UserName; // We can get UserName or UserDomainName
    var cheepString = String.Join(" ", args[1..]);
    var currentTimestamp = DateTimeOffset.Now; //only gets the current time.
    long unixTimestamp = currentTimestamp.ToUnixTimeSeconds();

    //join authorname, cheepString and timestamp
    cheepString = authorname + ",\"" + cheepString + "\"," + unixTimestamp.ToString();
    output.WriteLine(cheepString); //add new Cheep to file
    cheeps.Add(cheepString);  //add new Cheep to list
    output.Close(); //close StreamWriter
    read();
}

void read() {
    foreach (var cheep in cheeps) {
        var columns = cheep.Split(","); //Splits cheep by comma

        string author = columns[0];
        string message = string.Join(",", columns[1..^1]).Trim('"');
        long timestamp = long.Parse(columns[^1]);
        //Makes timestamp readable
        DateTimeOffset time = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        string formattedDate = time.ToString("MM/dd/yy HH:mm:ss");

        Console.WriteLine($"{author} @ {formattedDate}: {message} ");
        Thread.Sleep(1000); //creates delay between each Cheep
    }
}