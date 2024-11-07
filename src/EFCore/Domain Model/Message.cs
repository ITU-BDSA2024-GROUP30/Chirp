namespace Chirp.EFCore;
public class Message
{
    public string Text { get; set; }
    public int AuthorID { get; set; }
    public string AuthorName { get; set; }
    public DateTime TimeStamp { get; set; }
}