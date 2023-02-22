using webapi.Models;

namespace webapi.Repositories
{
    public interface IGameRepository
    {
        GameStatus? GetGame(string gameId);
        GameStatus NewGame(Player playerX, Player playerO, Player starts);
        GameStatus? UpdateGame(GameStatus game);
    }
}