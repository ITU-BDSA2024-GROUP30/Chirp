using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChirpRepositories;
using ChirpInfrastructure;
using Microsoft.Extensions.Configuration;
using ChirpCore.Domain;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);


// Load database connection via configuration, get string of database path from appsettings.json
string? connectionString = builder.Configuration.GetConnectionString("ChirpDatabaseConnection") ?? throw new InvalidOperationException("Connection string 'ChirpDatabaseConnection' not found.");

//ChirpDBContext created with our database path - which is specified in appsettings.json
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

var dbcon = new SqliteConnection(connectionString);
await dbcon.OpenAsync();

// builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<Author>(options => options.SignIn.RequireConfirmedAccount = true)
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

// From ChatGPT to ensure the database is up to date with migrations when run 
/*
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ChirpDBContext>();
    dbContext.Database.Migrate();
}
*/

//Below 'using' block from Group 3. Seeds our database, and ensures that the database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ChirpDBContext>();
    //context.Database.EnsureCreated();
    //await context.Database.MigrateAsync();
    DbInitializer.SeedDatabase(context);
}


app.Run();
