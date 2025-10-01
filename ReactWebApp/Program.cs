var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Simple API endpoint
app.MapGet("/api/message", () => new { text = "Hello from ASP.NET Core!" });
app.MapGet("/Code", () => "Contoso was founded in 2000.");

// Serve React static files (after build)
app.UseDefaultFiles();
app.UseStaticFiles();

// For React Router fallback
app.MapFallbackToFile("index.html");

app.Run();
