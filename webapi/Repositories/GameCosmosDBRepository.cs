using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using System.Net;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Repositories
{
    public class GameCosmosDBRepository : IGameRepository
    {
        private readonly CosmosClient _client;
        private Container _container;
        private Container container
        {
            get
            {
                if (_container == null)
                    _container = _client.GetDatabase("tic-tac-toe-db").GetContainer("tic-tac-toe-game-container\r\n");
                return _container;
            }
                
        }

        public GameCosmosDBRepository()
        {
            _client = new CosmosClient("https://tic-tac-toe-db.documents.azure.com:443", "51aKH9n6pTf0cIf29ffNk0RbOSabpHtJkfr6MBkqbl8fk3Wy22ofEZ8tj7dVJp8h2rpbikHXWKZ7ACDbt6FZaQ==", new CosmosClientOptions() { ApplicationName = "TicTacToeOnline" });
        }

        public async Task<GameStatus?> GetGame(string gameId)
        {
            var partitionKey = new PartitionKey(gameId);
            var existingGame = await container.ReadItemAsync<GameStatus>(gameId, partitionKey);
            if (existingGame.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            return existingGame.Resource;
        }

        public async Task<GameStatus> NewGame(Player playerX, Player playerO, Player starts)
        {
            var game = new GameStatus(Guid.NewGuid().ToString())
            {
                PlayerX = playerX,
                PlayerO = playerO,
                Turn = starts
            };

            if (await GetGame(game.Id) != null)
            {
                throw new InvalidGameStatusException($"Game ${game.Id} already exists.");
            }

            var newGame = await container.CreateItemAsync(game);

            if (newGame.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidGameStatusException($"Error on saving game on db {newGame.StatusCode}");
            }

            return newGame.Resource;
        }

        public async Task<GameStatus?> UpdateGame(GameStatus game)
        {
            if (await GetGame(game.Id) == null)
            {
                throw new InvalidGameStatusException($"Game ${game.Id} does not exist.");
            }

            var updatedGame = await container.ReplaceItemAsync<GameStatus>(game, game.Id);

            if (updatedGame.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidGameStatusException($"Error on updating game on db {updatedGame.StatusCode}");
            }

            return updatedGame.Resource;
        }

        public Task<GameStatus> NewGame(Player playerX)
        {
            throw new NotImplementedException();
        }
    }
}
