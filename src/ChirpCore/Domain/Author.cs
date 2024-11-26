using Microsoft.AspNetCore.Identity;


namespace ChirpCore.Domain
{
	public class Author : IdentityUser<int>
	{
		public required ICollection<Cheep> Cheeps { get; set; }

		//public required ICollection<Author> Follows { get; set; }
	}
}
