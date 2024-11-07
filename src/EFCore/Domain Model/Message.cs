using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

//[PrimaryKey(nameof(user_id))]
public class Message
{
    public string Text { get; set; }
    public int AuthorID { get; set; }
    public string AuthorName { get; set; }
    public DateTime TimeStamp { get; set; }
}