using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChirpCore.Domain
{
	/// <summary>
	/// Definition of Author fields. A Author is a user on our Chirp! Application.
	/// It inherits the IdentityUser with its fields.
	/// UserName field is a string for an author in our Chirp! Application.
	/// </summary>
	public class Author : IdentityUser
	{
		[Required] override public string? UserName { get; set; }
		public required ICollection<Cheep> Cheeps { get; set; }
		public required ICollection<Author> Follows { get; set; } = [];

	}
}
