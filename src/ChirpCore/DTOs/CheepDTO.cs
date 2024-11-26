using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChirpCore.DTOs
{
	public record CheepDTO(int CheepId, int UserId, string UserName, string Text, string TimeStamp);
}
