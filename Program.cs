var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGet("/api/salud", () => Results.Ok(new { estado = "ok" }));

app.Run();
