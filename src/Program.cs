using System.Reflection;
using AspNetCoreGeneratedDocument;
using Chirp.EFCore;
using Chirp.UserFacade.Chirp.Infrastructure.Chirp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

if (!Directory.Exists("/tmp/data"))
    {
            string DBFilePath = Path.GetTempPath();
            string DBFilePathWithFile = Path.Combine(DBFilePath + "chirp.db");
            Directory.CreateDirectory(DBFilePath);
            File.Create(DBFilePathWithFile);
    }


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    //Razor pages are in a different folder and therefore we use this customized path
    options.RootDirectory = "/UserFacade/Pages";
});

// Load database connection via configuration, get string of database path from appsettings.json
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//ChirpDBContext created with our database path - which is specified in appsettings.json
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));
//Below 2 lines helps create Cheeps on the website and show Cheeps.
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseHttpsRedirection();

var embeddedFileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), "wwwroot");

app.UseStaticFiles(new StaticFileOptions
{
    //since our wwwroot is in a different folder than program.cs, we need this specific path
    //FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "UserFacade", "wwwroot")),
    
    FileProvider = embeddedFileProvider,
    RequestPath = "/wwwroot"
});

app.UseRouting();

app.MapRazorPages();

//Below 'using' block from Group 3. Seeds our database, and ensures that the database is created
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ChirpDBContext>();
	context.Database.EnsureCreated();
	DbInitializer.SeedDatabase(context);
}

app.Run();