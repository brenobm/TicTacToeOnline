using webapi.Models;

namespace webapi.Services
{
    public interface IGameService
    {
        GameStatus? GetGame(string gameId);
        GameStatus? MakeMove(string gameId, string playerId, int row, int column);
        GameStatus NewGame(string playerXId, string playerXName, string playerOId, string playerOName);
    }
}