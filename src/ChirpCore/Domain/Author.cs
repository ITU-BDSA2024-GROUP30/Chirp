using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ChirpCore.Domain
{
	public class Author : IdentityUser
	{
		[Required] override public string? UserName {get; set;}
		public required ICollection<Cheep> Cheeps { get; set; }

		public required ICollection<Author> Follows { get; set; } = [];


	}
}
