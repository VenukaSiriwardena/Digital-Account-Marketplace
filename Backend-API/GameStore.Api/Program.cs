using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Fetch connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DB context with SQL Server
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
