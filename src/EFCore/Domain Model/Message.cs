using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

//[PrimaryKey(nameof(user_id))]
public class Message
{
    public required int MessageId { get; set; }
    public required int UserId { get; set; }
    public required string Text { get; set; }
    public required User User { get; set; }
}