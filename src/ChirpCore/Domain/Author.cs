using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChirpCore.Domain
{
	public class Author : IdentityUser
	{
		[Required] override public string? UserName { get; set; }
		public required ICollection<Cheep> Cheeps { get; set; }

		public required ICollection<Author> Follows { get; set; } = [];


	}
}
