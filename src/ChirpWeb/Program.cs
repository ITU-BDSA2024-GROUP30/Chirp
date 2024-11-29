using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChirpRepositories;
using ChirpInfrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using ChirpCore;
using ChirpCore.Domain;
using ChirpCore.DTOs;


var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("ChirpDatabaseConnection") ?? throw new InvalidOperationException("Connection string 'ChirpDatabaseConnection' not found.");

//string path = Environment.GetEnvironmentVariable("chirpdbpath") ?? throw new InvalidOperationException("Connection string 'ChirpDatabaseConnection' not found.");
// Load database connection via configuration, get string of database path from appsettings.json
//string connectionString = "Data Source=" + path;

//ChirpDBContext created with our database path - which is specified in appsettings.json
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

var dbcon = new SqliteConnection(connectionString);
//await dbcon.OpenAsync();

builder.Services.AddDefaultIdentity<Author>(options => options.SignIn.RequireConfirmedAccount = true)
.AddDefaultUI()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<ChirpDBContext>();

builder.Services.AddRazorPages();

//Below 2 lines helps create Cheeps on the website and show Cheeps.
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //removed !  might go back later
{
	// app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

//Below 'using' block from Group 3. Seeds our database, and ensures that the database is created

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ChirpDBContext>();
	await context.Database.MigrateAsync();
	context.Database.EnsureCreated();
	DbInitializer.SeedDatabase(context);
}

app.Run();

//class for API tests in Chirp.ChirpWeb.Tests
public partial class Program { }
