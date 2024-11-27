using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChirpCore.Domain
{
	public class Author : IdentityUser<int>
	{
		public int AuthorId
		{
			get => Id;  // Return the value of Id
			set => Id = value;  // Set the value of Id
		}
		public required ICollection<Cheep> Cheeps { get; set; }

		//public required ICollection<Author> Follows { get; set; }
	}
}
