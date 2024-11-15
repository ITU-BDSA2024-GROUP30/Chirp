using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChirpRepositories;
using ChirpInfrastructure;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);


// Load database connection via configuration, get string of database path from appsettings.json
string? connectionString = builder.Configuration.GetConnectionString("ChirpDatabaseConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//ChirpDBContext created with our database path - which is specified in appsettings.json
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

// builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
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

app.UseStaticFiles();  // Due to Onion Structure setup the implict path for wwwroot works again (same for addRazorPages)
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

//Below 'using' block from Group 3. Seeds our database, and ensures that the database is created
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ChirpDBContext>();
    context.Database.EnsureCreated();
    DbInitializer.SeedDatabase(context);
}*/

app.Run();
//public partial class Program { }

// Add services to the container.
/*builder.Services.AddRazorPages(options =>
{
    //Razor pages are in a different folder and therefore we use this customized path
    options.RootDirectory = "/Pages";
}); */

/*
app.UseStaticFiles(new StaticFileOptions
{
    //since our wwwroot is in a different folder than program.cs, we need this specific path
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "UserFacade", "wwwroot")),
    RequestPath = ""
});*/

/*
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChirpCore;
using ChirpCore.Domain; 
using ChirpCore.DTOs; 
*/