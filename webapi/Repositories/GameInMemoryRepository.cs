using webapi.Models;

namespace webapi.Repositories
{
    public class GameInMemoryRepository : IGameRepository
    {
        private IDictionary<string, GameStatus> _games = new Dictionary<string, GameStatus>();

        public GameStatus? GetGame(string gameId)
        {
            return _games.ContainsKey(gameId) ? _games[gameId] : null;
        }

        public GameStatus NewGame(Player playerX, Player playerO, Player starts)
        {
            var game = new GameStatus(Guid.NewGuid().ToString())
            {
                PlayerX = playerX,
                PlayerO = playerO,
                Turn = starts
            };

            _games.Add(game.Id, game);
            return game;
        }

        public GameStatus? UpdateGame(GameStatus game)
        {
            if (!_games.ContainsKey(game.Id))
            {
                return null;
            }

            _games[game.Id] = game;
            return game;
        }
    }
}
