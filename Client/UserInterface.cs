using static Chirp.Cli.Client.Program;

namespace Chirp.Cli.Client;
public class UserInterface {
    public void PrintMessages(IEnumerable<Cheep> cheeps){
       foreach (Cheep cheep in cheeps) {
            DateTimeOffset time = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp);
            time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(time, "Central Europe Standard Time");
            string formattedDate = time.ToString("MM/dd/yy HH:mm:ss");
    
            Console.WriteLine($"{cheep.Author} @ {formattedDate}: {cheep.Message}");
            Thread.Sleep(100); //creates delay between each Cheep
        }
    }  
}