using Microsoft.AspNetCore.Identity;

namespace Chirp.EFCore;

public class Author : IdentityUser
{
	public string Name { get; set; }
    public int AuthorId { get; set; }
	public required ICollection<Cheep> Cheeps { get; set; }
}