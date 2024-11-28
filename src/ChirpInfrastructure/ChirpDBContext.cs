using Microsoft.EntityFrameworkCore;
//using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ChirpCore.Domain;
using Microsoft.AspNetCore.Identity;

namespace ChirpInfrastructure;

public class ChirpDBContext : IdentityDbContext<Author, IdentityRole<int>, int>
{
	public DbSet<Cheep> Cheeps { get; set; }

	public DbSet<Author> Authors { get; set; }
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

		// Below lines 29-37 are from ChatGPT to help ensure the Id in Cheep is a foreign key.
		modelBuilder.Entity<Cheep>(entity =>
		{
			entity.HasKey(c => c.CheepId);

			entity.HasOne(c => c.Author)
			.WithMany(a => a.Cheeps)
			.HasForeignKey(c => c.Id)
			.OnDelete(DeleteBehavior.Cascade);
		});
	}

}