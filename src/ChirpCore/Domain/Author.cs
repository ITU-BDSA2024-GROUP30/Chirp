using Microsoft.AspNetCore.Identity;
using Chirp.EFCore;

namespace ChirpCore.Domain;

public class Author : IdentityUser
{
	public required string Name { get; set; }
    public int AuthorId { get; set; }
	//public string? Email { get; set; }
	public required ICollection<Cheep> Cheeps { get; set; }
}