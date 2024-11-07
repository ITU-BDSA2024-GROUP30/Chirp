using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ChirpCore.Domain;

namespace Chirp.EFCore;

public class ChirpDBContext : IdentityDbContext<Author>
{
	public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options) {

	}
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
								//above will be relevant later
	}
	public DbSet<Cheep> Cheeps { get; set; }

	public DbSet<Author> Authors { get; set; }
}