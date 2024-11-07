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
	options.RootDirectory = "/UserFacade/Pages";
});
//builder.Services.AddSingleton<ICheepService, CheepService>();

// Load database connection via configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));
//should below be addsingleton instead of addscoped?
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



app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "UserFacade", "wwwroot")),
	RequestPath = ""
});

app.UseRouting();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ChirpDBContext>();
	context.Database.EnsureCreated();
	DbInitializer.SeedDatabase(context);
}

app.Run();
