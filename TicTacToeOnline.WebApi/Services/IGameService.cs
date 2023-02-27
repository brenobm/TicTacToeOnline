using TicTacToeOnline.WebApi.Models;

namespace TicTacToeOnline.WebApi.Services;

public interface IGameService
{
    Task<Game> GetGame(string gameId);
    Task<Game> MakeMove(string gameId, string player, int row, int column);
    Task<Game> NewGame(string playerX, string playerO);
    Task<Game> NewGame(string playerX);
    Task<Game> JoinGame(string gameId, string playerO);
}