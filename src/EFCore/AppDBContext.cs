using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

public class AppDBContext : DbContext {
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {}
    public AppDBContext() {}
    
    public DbSet<Message> Messages { get; set; }

    public DbSet<User> Users { get; set; }
}