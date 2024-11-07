using ChirpCore.EFCore;

namespace ChirpCore.Domain;


public class Cheep
{
	public int CheepId { get; set; }
    public required string Text { get; set; }
    public DateTime TimeStamp { get; set; }
    public required Author Author { get; set; }
    public int AuthorId { get; set; }
}