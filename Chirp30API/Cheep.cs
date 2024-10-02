public class Cheep(string Author, string Message, long Timestamp)
{
    required string Author;
    required string Message;
    required long Timestamp;

    public Cheep(string Author, string Message, long Timestamp)
    {
        this.Author = Author;
        this.Message = Message;
        this.Timestamp = Timestamp;
    }

    //public required string? Author { get; set; }
    //public required string? Message { get; set; }
    //public required long Timestamp { get; set; }

}