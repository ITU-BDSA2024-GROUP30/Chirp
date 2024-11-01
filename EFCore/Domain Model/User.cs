using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

//[PrimaryKey(nameof(user_id))]
public class User
{
    public required int user_id { get; set; }
    public required string user_name { get; set; }

    public required ICollection<Message> MessageProcessingHandler { get; set; }
    //public required string email { get; set; }
    //public required string pw_hash { get; set; }
}