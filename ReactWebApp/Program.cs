using ReactWebApp.Data;
using ReactWebApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// DB Registry
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

// Game Endpoints
app.MapGameEndpoints();

// OTHER REACT + OTHER ROUTES
app.MapGet("/api/message", () => new { text = "Hello from ASP.NET Core!" });
app.MapGet("/Code", () => "Contoso was founded in 2000.");

// Serve React static files (after build)
app.UseDefaultFiles();
app.UseStaticFiles();

// For React Router fallback
app.MapFallbackToFile("index.html");

app.Run();
