using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChirpRepositories;
using ChirpInfrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using AspNet.Security.OAuth.GitHub;
using ChirpCore;
using ChirpCore.Domain;
using ChirpCore.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

// Load database connection via configuration, get string of database path from appsettings.json
string connectionString = builder.Configuration.GetConnectionString("ChirpDatabaseConnection") ?? throw new InvalidOperationException("Connection string 'ChirpDatabaseConnection' not found.");

//ChirpDBContext created with our database path - which is specified in appsettings.json
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

var dbcon = new SqliteConnection(connectionString);
//await dbcon.OpenAsync();

builder.Services.AddIdentity<Author, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
.AddDefaultUI()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<ChirpDBContext>();

builder.Services.AddRazorPages();

//Below 2 lines helps create Cheeps on the website and show Cheeps.
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();


// Taken from StackOverflow
// https://stackoverflow.com/questions/46309653/github-oauth-provider-with-asp-net-core-2-0-does-not-work

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(o => o.LoginPath = new PathString("/login"))
.AddOAuth("GitHub", "Git Hub", options =>
{
    options.ClientId = builder.Configuration["authentication:github:clientId"];
    options.ClientSecret = builder.Configuration["authentication:github:clientSecret"];
    options.CallbackPath = "/signin-github";
    options.AuthorizationEndpoint = "http://github.com/login/oauth/authorize";
    options.TokenEndpoint = "https://github.com/login/oauth/access_token";
    //options.Scope.Add("user:email");
});

/*
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "GitHub";
    })
    .AddCookie()
    .AddGitHub(o =>
    {
        o.ClientId = builder.Configuration["authentication:github:clientId"];
        o.ClientSecret = builder.Configuration["authentication:github:clientSecret"];
        o.CallbackPath = "/signin-github";
        o.Scope.Add("user:email");
    });*/

//  options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

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
//app.UseSession();


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
