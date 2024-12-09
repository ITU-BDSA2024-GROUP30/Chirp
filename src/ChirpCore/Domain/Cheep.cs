using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ChirpCore.Domain
{
	public class Cheep
	{
		[Key]
		public int CheepId { get; set; }
		public required Author Author { get; set; }
		[Required]
		[StringLength(160, MinimumLength = 1)]
		public required string Text { get; set; }
		public required DateTime TimeStamp { get; set; }
	}
}