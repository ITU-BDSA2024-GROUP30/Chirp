using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.EFCore;

public class AppDBContext : DbContext {

    //public string DbPath {get;}
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options){};
    public AppDBContext() {
        string DbPath;
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "/data/chirp.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public void ConfigureServices(IServiceCollection services) {
    var connectionString = "connection string to database";

    services.AddDbContext<AppDBContext>(options =>
        options.UseSqlite(connectionString));
    
    }
    public DbSet<Cheep> Cheeps { get; set; }

    public DbSet<Author> Authors { get; set; }
}