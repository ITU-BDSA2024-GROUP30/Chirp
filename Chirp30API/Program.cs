using System.Collections;
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//builder.Services.AddRazorPages(); 

// Wherever in the project an ICheepService implementation is needed, use the CheepService 

// Use CheepService as a Singleton during application life time. 

//builder.Services.AddSingleton<ICheepService, CheepService>(); 



app.MapGet("/", () => "Hello World!");

//Cheep(string Author, string Message, long Timestamp); 



//app.MapGet("/cheeps", () => a); 
var a = new ArrayList();
//var c1 = new Cheep();
Cheep c1 = new Cheep("me", "Hej!", 1684229348);
a.Add(c1);


app.MapPost("/cheep", (Cheep c1) => { ("me", "Hej!", 1684229348) });

app.MapGet("/cheeps", () => c1);





app.Run();