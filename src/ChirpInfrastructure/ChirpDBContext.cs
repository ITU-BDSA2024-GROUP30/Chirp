using ChirpCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChirpInfrastructure;

public class ChirpDBContext : IdentityDbContext<Author>
{
	public required DbSet<Cheep> Cheeps { get; set; }
	public required DbSet<Author> Authors { get; set; }
	public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Author>().HasIndex(a => a.UserName).IsUnique();
		modelBuilder.Entity<Author>().HasIndex(a => a.Email).IsUnique();
	//Below method structure from ChatGpt
		modelBuilder.Entity<Author>()
						.HasMany(a => a.Follows)
						.WithMany()
						.UsingEntity(join => join.ToTable("AuthorFollows"));

	}

	internal void RemoveRange(object cheepToRemove)
	{
		throw new NotImplementedException();
	}
}
