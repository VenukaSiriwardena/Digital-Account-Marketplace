using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new()
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

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(game => game.Id == id));
    }

    public async Task CreateAsync(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);

        await Task.CompletedTask;
    }
}