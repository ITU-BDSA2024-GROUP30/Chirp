using Microsoft.AspNetCore.Identity;

namespace Chirp.Identity;
public class IdentityUser
{
	public required string name { get; set; }
	public required string email { get; set; }

}