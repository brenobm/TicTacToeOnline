using webapi.Models;
using webapi.Repositories;

namespace webapi.Services
{
    public interface IGameService
    {
        Task<GameStatus?> GetGame(string gameId);
        Task<GameStatus?> MakeMove(string gameId, string playerId, int row, int column);
        Task<GameStatus> NewGame(string playerXId, string playerXName, string playerOId, string playerOName);
        Task<GameStatus> NewGame(string playerXId, string playerXName);
        Task<GameStatus> JoinGame(string gameId, string playerOName, string playerOId);
    }
}