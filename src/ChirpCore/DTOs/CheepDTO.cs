using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChirpCore.DTOs
{
	public class CheepDTO
	{
		public int AuthorID { get; set; }
		[StringLength(160)] public string? Text { get; set; }
		public required string TimeStamp { get; set; } // DateTime changed to string
		public string? AuthorName { get; set; }

	}
}