using System.IO;

StreamReader input = new("chirp_cli_db.csv");

List<string> cheeps = [];
var nextLine = input.ReadLine();
while ((nextLine = input.ReadLine()) != null) {
   cheeps.Add(nextLine);
}

if (args[0]=="read") {
    foreach (var cheep in cheeps) {
        Console.WriteLine(cheep);
        Thread.Sleep(1000);
    }
} else if (args[0]=="cheep"){
    //var authorname
    var cheepString = String.Join(" ", args[1..]);
    //var timestamp
    //join authorname, cheepString and timestamp
    cheeps.Add(cheepString);
    foreach (var cheep in cheeps) {
        Console.WriteLine(cheep);
        Thread.Sleep(1000);
    }
}