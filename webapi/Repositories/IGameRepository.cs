using webapi.Models;

namespace webapi.Repositories
{
    public interface IGameRepository
    {
        Task<GameStatus?> GetGame(string gameId);
        Task<GameStatus> NewGame(Player playerX, Player playerO, Player starts);
        Task<GameStatus> NewGame(Player playerX);
        Task<GameStatus?> UpdateGame(GameStatus game);
    }
}