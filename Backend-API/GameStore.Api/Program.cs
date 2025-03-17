using GameStore.Api.Entities;

const string GetGameEndpointName = "GetGame";

List<Game> games = new()
{
    new Game(){
        Id = 1,
        Name = "Game 1",
        Genre = "Fighting",
        Price = 10.00M,
        ReleaseDate = new DateTime(2022, 01, 01),
        ImageUri = "https://via.placeholder.com/150",
    },
    new Game(){
        Id = 2,
        Name = "Game 2",
        Genre = "Fighting",
        Price = 10.00M,
        ReleaseDate = new DateTime(2022, 01, 01),
        ImageUri = "https://via.placeholder.com/150",
    },
    new Game(){
        Id = 3,
        Name = "Game 3",
        Genre = "Fighting",
        Price = 10.00M,
        ReleaseDate = new DateTime(2022, 01, 01),
        ImageUri = "https://via.placeholder.com/150",
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var group = app.MapGroup("/games")
    .WithParameterValidation();

group.MapGet("/", () => games);


group.MapGet("/{id}", (int id) => 
{
    Game? game = games.Find(game => game.Id == id);

    if (game == null){
        return Results.NotFound();
    } else {
        return Results.Ok(game);
    }
})
.WithName(GetGameEndpointName);


group.MapPost("/", (Game game) =>
{ 
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

group.MapPut("/{id}", (int id, Game updatedGame) =>
{
    Game? existingGame = games.Find(game => game.Id == id);

    if (existingGame == null){
        return Results.NotFound();
    } else {
        existingGame.Name = updatedGame.Name;
        existingGame.Genre = updatedGame.Genre;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
        existingGame.ImageUri = updatedGame.ImageUri;

        return Results.NoContent();
    }

});

group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is not null)
    {
        games.Remove(game);
    }
        return Results.NoContent();    

});

app.Run();
