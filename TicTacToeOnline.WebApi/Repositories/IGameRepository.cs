using TicTacToeOnline.WebApi.Models;

namespace TicTacToeOnline.WebApi.Repositories;

public interface IGameRepository
{
    Task<Game> GetGame(string gameId);
    Task<Game> NewGame(string playerX, string playerO, string starts);
    Task<Game> NewGame(string playerX);
    Task<Game> UpdateGame(Game game);
}