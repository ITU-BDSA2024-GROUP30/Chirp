using AspNet.Security.OAuth.GitHub;
using ChirpCore;
using ChirpCore.Domain;
using ChirpCore.DTOs;
using ChirpInfrastructure;
using ChirpRepositories;
using ChirpServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


//partial class for API tests in Chirp.ChirpWeb.Tests

public partial class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		string connectionString = "";

		connectionString = builder.Configuration.GetConnectionString("CHIRPDBPATH") ?? throw new InvalidOperationException("Connection string not found.");

		var dbcon = new SqliteConnection(connectionString);
		dbcon.Open();

		//ChirpDBContext created with our database path - which is specified in appsettings.json
		builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(dbcon));



		builder.Services.AddAuthentication(options =>
			{
				options.DefaultChallengeScheme = "GitHub";
			})
			.AddCookie()
			.AddGitHub(o =>
			{
				o.ClientId = builder.Configuration["authentication_github_clientId"];
				o.ClientSecret = builder.Configuration["authentication_github_clientSecret"];
				o.CallbackPath = "/signin-github";
				o.Scope.Add("user:email");
			});

		builder.Services.AddDefaultIdentity<Author>(options => options.SignIn.RequireConfirmedAccount = false)
		.AddDefaultUI()
		.AddDefaultTokenProviders()
		.AddEntityFrameworkStores<ChirpDBContext>();

		builder.Services.AddRazorPages();

		//Below 2 lines helps create Cheeps on the website and show Cheeps.
		builder.Services.AddScoped<ICheepService, CheepService>();
		builder.Services.AddScoped<ICheepRepository, CheepRepository>();
		builder.Services.AddScoped<IAuthorService, AuthorService>();
		builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

		var app = builder.Build();


		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment()) //removed !  might go back later
		{
			app.UseExceptionHandler("/Error");
			app.UseHsts();     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		}
		else
		{
			// app.UseMigrationsEndPoint();
		}

		//Below 'using' block from Group 3. Seeds our database, and ensures that the database is created

		using (var scope = app.Services.CreateScope())
		{
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<ChirpDBContext>();
			context.Database.Migrate();
			DbInitializer.SeedDatabase(context);
		}
		app.UseHttpsRedirection();

		app.UseStaticFiles();
		app.UseRouting();


		app.UseAuthentication();
		app.UseAuthorization();
		//app.UseSession();

		app.MapRazorPages();



		app.Run();
	}
}
