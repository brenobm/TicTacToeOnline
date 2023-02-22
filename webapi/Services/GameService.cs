using webapi.Exceptions;
using webapi.Models;
using webapi.Repositories;

namespace webapi.Services
{
    public class GameService : IGameService
    {
        private readonly Random _random = new Random();
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GameStatus? GetGame(string gameId)
        {
            return _gameRepository.GetGame(gameId);
        }

        public GameStatus? MakeMove(string gameId, string playerId, int row, int column)
        {
            var game = _gameRepository.GetGame(gameId);

            if (game == null)
            {
                return null;
            }

            if (game.Status != PlayStatus.RUNNING)
            {
                throw new InvalidGameStatusException($"Game {gameId} is not in RUNNING status");
            }

            if (game.Turn.Id != playerId)
            {
                throw new InvalidGameStatusException($"Is not the player {playerId} turn");
            }

            var move = playerId == game.PlayerX.Id
                ? Element.X
                : (playerId == game.PlayerO.Id
                    ? Element.O
                    : throw new InvalidGameStatusException($"Player {playerId} is not in the game"));

            if (game.Board[row][column] != Element.EMPTY)
            {
                throw new InvalidGameStatusException($"The position {row}, {column} is not empty");
            }

            game = UpdateGameStatus(game, row, column, move);

            if (_gameRepository.UpdateGame(game) == null)
            {
                return null;
            }

            return game;
        }

        public GameStatus NewGame(string playerXId, string playerXName, string playerOId, string playerOName)
        {
            var playerX = new Player(playerXId, playerXName);
            var playerO = new Player(playerOId, playerOName);
            var starts = _random.Next(0, 2) == 0 ? playerX : playerO;
            return _gameRepository.NewGame(playerX, playerO, starts);
        }

        private GameStatus UpdateGameStatus(GameStatus game, int row, int column, Element move)
        {
            game.Board[row][column] = move;

            if (VerifyWin(game, row, column, move))
            {
                game.Status = PlayStatus.WON;
                game.Winner = move == Element.X ? game.PlayerX : game.PlayerO;
            }
            else if (VerifyBoardFull(game))
            {
                game.Status = PlayStatus.DRAW;
            }
            else
            {
                game.Turn = game.Turn == game.PlayerX ? game.PlayerO : game.PlayerX;
            }

            return game;
        }

        private bool VerifyBoardFull(GameStatus game)
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

        private bool VerifyWin(GameStatus game, int row, int column, Element move)
        {
            return VerifyWinRow(game, row, move) || VerifyWinColumn(game, column, move) || VerifyWinTransversal(game, move);
        }

        private bool VerifyWinRow(GameStatus game, int row, Element move)
        {
            //Verify row
            for (int i = 0; i < game.Board.Length; i++)
            {
                if (game.Board[row][i] != move)
                {
                    return false;
                }
            }

            return true;
        }

        private bool VerifyWinColumn(GameStatus game, int column, Element move)
        {
            //Verify row
            for (int i = 0; i < game.Board.Length; i++)
            {
                if (game.Board[i][column] != move)
                {
                    return false;
                }
            }

            return true;
        }

        private bool VerifyWinTransversal(GameStatus game, Element move)
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


            return winTrans1 || winTrans2;
        }
    }
}
