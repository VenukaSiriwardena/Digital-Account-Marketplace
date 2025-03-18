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

    public IEnumerable<Game> GetAll()
    {
        return games;
    }

    public Game? Get(int id)
    {
        return games.Find(game => game.Id == id);
    }

    public void Create(Game game)
    {
        game.Id = games.Max(game => game.Id) + 1;
        games.Add(game);
    }

    public void Update(Game updatedGame)
    {
        var index = games.FindIndex(game => game.Id == updatedGame.Id);
        games[index] = updatedGame;
    }

    public void Delete(int id)
    {
        var index = games.FindIndex(game => game.Id == id);
        games.RemoveAt(index);
    }
}