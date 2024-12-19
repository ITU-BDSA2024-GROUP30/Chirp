using System.ComponentModel.DataAnnotations;
namespace ChirpCore.Domain
{
	public class Cheep
	{
		[Key]
		public int CheepId { get; set; }
		public required Author Author { get; set; }
		[Required]
		[StringLength(160, MinimumLength = 1)]
		public required string Text { get; set; }
		public required DateTime TimeStamp { get; set; }
	}
}