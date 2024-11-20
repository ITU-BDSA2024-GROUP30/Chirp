using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChirpCore.DTOs
{
	public record CheepDTO(int UserId, int CheepId, string Text, string Timestamp);
	/*
	{
		public int UserId { get; set; }
		[StringLength(160, MinimumLength = 1)] public string? Text { get; set; }
		public required string TimeStamp { get; set; } // DateTime changed to string
		public string? AuthorName { get; set; }

	}*/
}