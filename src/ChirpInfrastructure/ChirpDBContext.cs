using Microsoft.EntityFrameworkCore;
//using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ChirpCore.Domain;
using Microsoft.AspNetCore.Identity;

namespace ChirpInfrastructure;

public class ChirpDBContext : IdentityDbContext<Author, IdentityRole<int>, int>
{
	public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
	{

	}
	// public ChirpDBContext() {}
	public DbSet<Cheep> Cheeps { get; set; }

	public DbSet<Author> Authors { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Author>().HasIndex(a => a.UserName).IsUnique();
		modelBuilder.Entity<Author>().HasIndex(a => a.Email).IsUnique();
	}

}