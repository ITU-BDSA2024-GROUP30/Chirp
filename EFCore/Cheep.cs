using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

[PrimaryKey(nameof(message_id))] 
public class Cheep{
  public required int message_id {get; set;}
  public required int author_id {get; set;}
  public required string text {get; set;}
  public required int pub_date {get; set;}
  public required Author Author {get; set;}
}