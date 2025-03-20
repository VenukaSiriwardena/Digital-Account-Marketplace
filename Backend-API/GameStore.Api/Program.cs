using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Manually get environment variables
var dbUsername = Environment.GetEnvironmentVariable("DB_USERNAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Get the connection string template from appsettings.json
var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");

// Replace placeholders with actual values
var connectionString = connectionStringTemplate
    .Replace("${DB_USERNAME}", dbUsername)
    .Replace("${DB_PASSWORD}", dbPassword);

// Register Entity Framework with the final connection string
builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

// Ensure database migrations are applied
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    await dbContext.Database.MigrateAsync(); // Applies pending migrations
}

// Initialize database
await app.Services.InitializeDbAsync();

app.MapGamesEndpoints();
app.Run();
