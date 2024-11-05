namespace Chirp.EFCore;


public class Cheep
{
	public int CheepId { get; set; }
    public string Text { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public Author Author { get; set; }
    public int AuthorId { get; set; }
}