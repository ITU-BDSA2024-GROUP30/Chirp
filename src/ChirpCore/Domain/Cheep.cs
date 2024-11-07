using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChirpCore.Domain
{
    public class Cheep
    {
		public int CheepId { get; set; }
		public int AuthorId { get; set; }
		public required Author Author { get; set; }
		[StringLength(160)] public required string Text { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}