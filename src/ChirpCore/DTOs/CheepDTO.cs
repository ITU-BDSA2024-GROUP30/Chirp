namespace ChirpCore.DTOs
{
	/// <summary>
	/// CheepDTO is used to transfer data of cheep(s) to our Chirp! Application.
	/// Ensures that cheep(s) are immutable and the sensitive data beloging of our cheep is not exposed.
	/// </summary>
	/// <param name="CheepId">Primary key of our Cheep(s)</param>
	/// <param name="UserName">Requried that a cheep has the username of the author creating the cheep(s).</param>
	/// <param name="Text">Requried that a cheep has a text while the cheep(s) is created. </param>
	/// <param name="TimeStamp">Requried that a cheep always has a timestamp of the creating of the cheep(s).</param>
	/// <returns></returns>
	public record CheepDTO(int CheepId, string UserName, string Text, string TimeStamp);
}
