using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Repositories
{
    public class GameInMemoryRepository : IGameRepository
    {
        private IDictionary<string, GameStatus> _games = new Dictionary<string, GameStatus>();

        public Task<GameStatus?> GetGame(string gameId)
        {
            return Task.FromResult(_games.ContainsKey(gameId) ? _games[gameId] : null);
        }

        public Task<GameStatus> NewGame(Player playerX, Player playerO, Player starts)
        {
            var game = new GameStatus(Guid.NewGuid().ToString())
            {
                Status = PlayStatus.RUNNING,
                PlayerX = playerX,
                PlayerO = playerO,
                Turn = starts
            };

            _games.Add(game.Id, game);
            return Task.FromResult(game);
        }

        public Task<GameStatus> NewGame(Player playerX)
        {
            var game = new GameStatus(Guid.NewGuid().ToString())
            {
                Status = PlayStatus.WAITING,
                PlayerX = playerX
            };

            _games.Add(game.Id, game);
            return Task.FromResult(game);
        }

        public Task<GameStatus?> UpdateGame(GameStatus game)
        {
            if (!_games.ContainsKey(game.Id))
            {
                return Task.FromResult<GameStatus?>(null);
            }

            _games[game.Id] = game;
            return Task.FromResult<GameStatus?>(game);
        }
    }
}
