using Microsoft.AspNetCore.Identity;
using Chirp.EFCore;

namespace ChirpCore.Domain;

public class Author : IdentityUser
{
	public int AuthorId { get; set; }
	public required string Name { get; set; }

	//public string? Email { get; set; }
	public required ICollection<Cheep> Cheeps { get; set; }
}