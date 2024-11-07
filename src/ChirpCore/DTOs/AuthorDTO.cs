using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChirpCore.DTOs
{
    public class AuthorDTO
    {
		public int UserId { get; set; }
		public string? Name { get; set; }
		//public string Email { get; set; }
		//public ICollection<Cheep> Cheeps { get; set; }
	}
}