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

public partial class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddRazorPages();

		//Below 2 lines helps create Cheeps on the website and show Cheeps.
		builder.Services.AddScoped<ICheepService, CheepService>();
		builder.Services.AddScoped<ICheepRepository, CheepRepository>();

		var app = builder.Build();


		// app.UseMigrationsEndPoint();
		string path = Environment.GetEnvironmentVariable("chirpdbpath") ?? throw new InvalidOperationException("Connection string 'ChirpDatabaseConnection' not found.");
		// Load database connection via configuration, get string of database path from appsettings.json
		string connectionString = "Data Source=" + path;

		//ChirpDBContext created with our database path - which is specified in appsettings.json
		//builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

		using var dbcon = new SqliteConnection(connectionString);
		dbcon.Open();


		app.UseHttpsRedirection();

		app.UseStaticFiles();
		app.UseRouting();


		app.UseAuthentication();
		app.UseAuthorization();

		app.MapRazorPages();


		app.Run();

	}
}
