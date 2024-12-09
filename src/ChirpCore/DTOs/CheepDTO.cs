using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ChirpCore.DTOs
{
	public record CheepDTO(int CheepId, string UserName, string Text, string TimeStamp);
}
