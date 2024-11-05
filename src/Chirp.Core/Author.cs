using Microsoft.AspNetCore.Identity;

namespace Chirp.EFCore;

public class Author : IdentityUser
{
	public required string Name { get; set; }
	public required string Email { get; set; }
	public required string AuthorId { get; set; }
	public ICollection<Cheep> Cheeps { get; set; }
}