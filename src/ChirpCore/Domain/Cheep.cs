using System.ComponentModel.DataAnnotations;
namespace ChirpCore.Domain
{
	/// <summary>
	/// Definition of Cheeps fields. A Cheep is a message on our Chirp! Application.
	/// </summary>
	public class Cheep
	{
		/// <summary>
		/// Primary Key which is always generated for cheep(s)</summary>
		/// <value> CheepId </value>
		[Key]
		public int CheepId { get; set; }
		public required Author Author { get; set; }
		/// <summary>
		/// The text of a cheep is a maximum length of 160 characters
		/// </summary>
		[Required]
		[StringLength(160, MinimumLength = 1)]
		public required string Text { get; set; }
		public required DateTime TimeStamp { get; set; }
	}
}