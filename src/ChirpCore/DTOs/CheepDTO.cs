namespace ChirpCore.DTOs
{
	/// <summary>
	/// CheepDTO is used to transfer data of cheep(s) to our Chirp! Application.
	/// Ensures that cheep(s) are unimmutable and sensitive data of our cheep is not exposed.
	/// </summary>
	/// <param name="CheepId"></param>
	/// <param name="UserName"></param>
	/// <param name="Text"></param>
	/// <param name="TimeStamp"></param>
	/// <returns></returns>
	public record CheepDTO(int CheepId, string UserName, string Text, string TimeStamp);
}
