namespace Chirp.EFCore;


public class Cheep
{
	public required int CheepId { get; set; }
	public required string AuthorId { get; set; }
	public required string Text { get; set; }
	public required DateTimeOffset TimeStamp { get; set; }
	public Author Author { get; set; }
}