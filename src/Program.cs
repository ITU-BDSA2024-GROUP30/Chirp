using Chirp.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


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

//builder.Services.AddScoped<IChatService, ChatService>();
//builder.Services.AddScoped<IMessageRepository, MessageRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "UserFacade");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(wwwrootPath),
    RequestPath = "/wwwroot"
});
//app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
