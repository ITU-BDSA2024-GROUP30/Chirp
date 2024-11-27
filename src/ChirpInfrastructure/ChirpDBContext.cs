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

		modelBuilder.Entity<Author>(entity =>
		{
			entity.Property(a => a.UserName).IsRequired(); //ensures that username is not null
			entity.Property(u => u.AuthorId)  // Adds AuthorId as key to Id column in author table
            .HasColumnName("Id");  

			entity.HasIndex(a => a.UserName).IsUnique();
			entity.HasIndex(a => a.Email).IsUnique();

			
		});

            

	}

}