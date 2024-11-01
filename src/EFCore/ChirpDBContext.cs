using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

public class ChirpDBContext : IdentityDbContext<Author, IdentityRole<int>, int>
{
	public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options) { }
	public ChirpDBContext()
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Author>()
								.HasIndex(c => c.Name)
								.IsUnique();
		modelBuilder.Entity<Author>()
								.HasIndex(c => c.Email)
								.IsUnique();
		modelBuilder.Entity<Author>()
								.HasIndex(c => c.AuthorId)
								.IsUnique();
		/*modelBuilder.Entity<Author>()
								.HasKey(k => new { k.FollowerId, k.FollowingId });*/
	}
	public DbSet<Message> Messages { get; set; }

	public DbSet<User> Users { get; set; }
}