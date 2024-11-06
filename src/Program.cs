using Chirp.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Chirp.UserFacade.Chirp.Infrastructure.Chirp.Services;



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



app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "UserFacade", "wwwroot")),
    RequestPath = ""
});

app.UseRouting();

app.MapRazorPages();

app.Run();
