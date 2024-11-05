using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

//[PrimaryKey(nameof(user_id))]
public class User
{
    public int UserId { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<Cheep> Cheeps { get; set; }
}