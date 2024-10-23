using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

public class AppDBContext : DbContext {
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {}
    public AppDBContext() {}
    
    public DbSet<Cheep> Cheeps { get; set; }

    public DbSet<Author> Authors { get; set; }
}