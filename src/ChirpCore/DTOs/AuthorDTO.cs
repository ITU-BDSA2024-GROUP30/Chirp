namespace ChirpCore.DTOs
{
	/// <summary>
	/// AuhtorDTO is used to transfer data of an author to our Chirp! Application.
	/// Ensures that author(s) are unimmutable and sensitive data of our author is not exposed.
	/// </summary>
	public record AuthorDTO(int Id, string Name);
}