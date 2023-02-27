using Microsoft.Azure.Cosmos;
using System.Net;
using TicTacToeOnline.WebApi.Configuration;
using TicTacToeOnline.WebApi.Exceptions;
using TicTacToeOnline.WebApi.Models;

namespace TicTacToeOnline.WebApi.Repositories;

public class GameCosmosDBRepository : IGameRepository
{
    private CosmosClient? _client;
    private readonly CosmosDbConfig _cosmosDbConfig;
    private Container? _container;
    private Container Container => _container ??= CosmosClient.GetDatabase(_cosmosDbConfig.DatabaseName).GetContainer(_cosmosDbConfig.Container);

    private CosmosClient CosmosClient => _client ??= _client = new CosmosClient(_cosmosDbConfig.Endpoint, _cosmosDbConfig.AuthKey, new CosmosClientOptions() { ApplicationName = "TicTacToeOnline" });

    public GameCosmosDBRepository(CosmosDbConfig cosmoDbConfig)
    {
        _cosmosDbConfig = cosmoDbConfig;
    }

    public async Task<Game> GetGame(string gameId)
    {
        var partitionKey = new PartitionKey(gameId);

        try
        {
            return await Container.ReadItemAsync<Game>(gameId, partitionKey);
        } 
        catch (CosmosException cex)
        {
            if (cex.StatusCode == HttpStatusCode.NotFound)
                throw new GameNotFoundException();
            else
                throw new NonExpectedError(cex.Message);
        }
    }

    public async Task<Game> NewGame(string playerX, string playerO, string starts)
    {
        var game = new Game(Guid.NewGuid().ToString(), playerX, playerO, starts);

        try
        {
            if (await GetGame(game.Id) != null)
            {
                throw new GameAlreadyExistsException();
            }
        }
        catch (GameNotFoundException)
        {
            // Expected
        }

        var newGame = await Container.CreateItemAsync(game);

        if (newGame.StatusCode != HttpStatusCode.Created)
        {
            throw new NonExpectedError($"Error on saving game on db {newGame.StatusCode}");
        }

        return newGame.Resource;
    }

    public async Task<Game> UpdateGame(Game game)
    {
        await GetGame(game.Id);

        var updatedGame = await Container.ReplaceItemAsync<Game>(game, game.Id);

        if (updatedGame.StatusCode != HttpStatusCode.OK)
        {
            throw new NonExpectedError($"Error on updating game on db {updatedGame.StatusCode}");
        }

        return updatedGame.Resource;
    }

    public async Task<Game> NewGame(string playerX)
    {
        var game = new Game(Guid.NewGuid().ToString(), playerX);

        try
        {
            if (await GetGame(game.Id) != null)
            {
                throw new GameAlreadyExistsException();
            }
        }
        catch (GameNotFoundException)
        {
            // Expected
        }

        var newGame = await Container.CreateItemAsync(game);

        if (newGame.StatusCode != HttpStatusCode.Created)
        {
            throw new NonExpectedError($"Error on saving game on db {newGame.StatusCode}");
        }

        return newGame.Resource;
    }
}
