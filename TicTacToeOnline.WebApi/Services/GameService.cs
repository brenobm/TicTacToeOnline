using Microsoft.AspNetCore.SignalR;
using TicTacToeOnline.WebApi.Exceptions;
using TicTacToeOnline.WebApi.Hubs;
using TicTacToeOnline.WebApi.Models;
using TicTacToeOnline.WebApi.Repositories;

namespace TicTacToeOnline.WebApi.Services;

public class GameService : IGameService
{
    private readonly Random _random = new Random();
    private readonly IGameRepository _gameRepository;
    private readonly IHubContext<TicTacToeHub> _hubContext;

    public GameService(IGameRepository gameRepository, IHubContext<TicTacToeHub> hubContext)
    {
        _gameRepository = gameRepository;
        _hubContext = hubContext;
    }

    public Task<Game> GetGame(string gameId)
    {
        return _gameRepository.GetGame(gameId);
    }

    public async Task<Game> MakeMove(string gameId, string player, int row, int column)
    {
        var game = await _gameRepository.GetGame(gameId);

        if (game.Status != GameStatus.RUNNING)
        {
            throw new InvalidGameStatusException($"Game {gameId} is not in RUNNING status");
        }

        if (game.Turn != player)
        {
            throw new InvalidGameStatusException($"Is not the player {player} turn");
        }

        var move = player == game.PlayerX
            ? Element.X
            : (player == game.PlayerO
                ? Element.O
                : throw new InvalidGameStatusException($"Player {player} is not in the game"));

        if (game.Board[row][column] != Element.EMPTY)
        {
            throw new InvalidGameStatusException($"The position {row}, {column} is not empty");
        }

        game = UpdateGameStatus(game, row, column, move);

        game = await _gameRepository.UpdateGame(game);

        await _hubContext.Clients.All.SendAsync("Game", game.Turn!, game);
        
        return game;
    }

    public Task<Game> NewGame(string playerX, string playerO)
    {
        var starts = _random.Next(0, 2) == 0 ? playerX : playerO;
        return _gameRepository.NewGame(playerX, playerO, starts);
    }

    public Task<Game> NewGame(string playerX)
    {
        return _gameRepository.NewGame(playerX);
    }

    public async Task<Game> JoinGame(string gameId, string playerO)
    {
        var game = await _gameRepository.GetGame(gameId);

        if (game.Status != GameStatus.WAITING)
        {
            throw new InvalidGameStatusException("Game already started");
        }

        var starts = _random.Next(0, 2) == 0 ? game.PlayerX : playerO;
        game.PlayerO = playerO;
        game.Turn = starts;
        game.Status = GameStatus.RUNNING;

        game = await _gameRepository.UpdateGame(game);

        await _hubContext.Clients.All.SendAsync("Game", game.Turn!, game);
        
        return game;
    }
    private Game UpdateGameStatus(Game game, int row, int column, Element move)
    {
        game.Board[row][column] = move;

        if (VerifyWin(game, row, column, move))
        {
            game.Status = GameStatus.WON;
            game.Winner = move == Element.X ? game.PlayerX : game.PlayerO;
        }
        else if (VerifyBoardFull(game))
        {
            game.Status = GameStatus.DRAW;
        }
        else
        {
            game.Turn = game.Turn == game.PlayerX ? game.PlayerO : game.PlayerX;
        }

        return game;
    }

    private bool VerifyBoardFull(Game game)
    {
        for (int row = 0; row < game.Board.Length; row++)
        {
            for (int column = 0;  column < game.Board[row].Length; column++)
            {
                if (game.Board[row][column] == Element.EMPTY)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool VerifyWin(Game game, int row, int column, Element move)
    {
        return VerifyWinRow(game, row, move) || VerifyWinColumn(game, column, move) || VerifyWinTransversal(game, move);
    }

    private bool VerifyWinRow(Game game, int row, Element move)
    {
        //Verify row
        for (int i = 0; i < game.Board.Length; i++)
        {
            if (game.Board[row][i] != move)
            {
                return false;
            }
        }
        for (int i = 0; i < game.Board.Length; i++)
        {
            game.Board[row][i] = move == Element.X ? Element.WIN_X : Element.WIN_O;
        }
        return true;
    }

    private bool VerifyWinColumn(Game game, int column, Element move)
    {
        //Verify row
        for (int i = 0; i < game.Board.Length; i++)
        {
            if (game.Board[i][column] != move)
            {
                return false;
            }
        }
        for (int i = 0; i < game.Board.Length; i++)
        {
            game.Board[i][column] = move == Element.X ? Element.WIN_X : Element.WIN_O;
        }

        return true;
    }

    private bool VerifyWinTransversal(Game game, Element move)
    {
        var winTrans1 = true;
        var winTrans2 = true;

        for (int i = 0; i < game.Board.Length; i++)
        {
            if (game.Board[i][i] != move)
            {
                winTrans1 = false;
            }
            
            if (game.Board[i][game.Board.Length - i - 1] != move)
            {
                winTrans2 = false;
            }
        }
        if (winTrans1 || winTrans2)
        {
            for (int i = 0; i < game.Board.Length; i++)
            {
                if (winTrans1)
                {
                    game.Board[i][i] = move == Element.X ? Element.WIN_X : Element.WIN_O;
                }
                if (winTrans2)
                {
                    game.Board[i][game.Board.Length - i - 1] = move == Element.X ? Element.WIN_X : Element.WIN_O;
                }
            }

            return true;
        }

        return false;
    }
}
