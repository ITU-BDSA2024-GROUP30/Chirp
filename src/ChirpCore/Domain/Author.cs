using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChirpCore.Domain
{
	public class Author : IdentityUser<int>
	{
		//public int UserId { get; set; }
		//public required string Name { get; set; }
		//public override required string? Email { get; set; }
		public required ICollection<Cheep> Cheeps { get; set; }

		public required ICollection<Author> Follows { get; set; }
	}
}