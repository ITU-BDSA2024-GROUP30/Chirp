using Microsoft.AspNetCore.Identity;

namespace Chirp.EFCore;

public class Author : IdentityUser
{
	public int AuthorId { get; set; }
	public string? Name { get; set; }
	public string Email { get; set; }

	public required ICollection<Cheep> Cheeps { get; set; }
}