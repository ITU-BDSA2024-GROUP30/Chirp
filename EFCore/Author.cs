using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

[PrimaryKey(nameof(user_id))] 
public class Author {
  public required int user_id {get; set;}
  public required string username {get; set;}
  public required string email {get; set;}
  public required string pw_hash {get; set;}
}