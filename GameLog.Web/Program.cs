using GameLog.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

var settings = new Settings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddControllers();
builder.Services.AddGameLogDbContext(settings.Database.ConnectionString);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();
