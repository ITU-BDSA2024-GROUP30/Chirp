using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

//[PrimaryKey(nameof(user_id))]
public class User
{
    public required int UserId { get; set; }
    public required string Name { get; set; }

    public required ICollection<Message> Messages { get; set; }
    //public required string email { get; set; }
    //public required string pw_hash { get; set; }
}