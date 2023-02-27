using Microsoft.AspNetCore.Mvc;
using TicTacToeOnline.WebApi.Dtos.Requests;
using TicTacToeOnline.WebApi.Exceptions;
using TicTacToeOnline.WebApi.Models;
using TicTacToeOnline.WebApi.Services;

namespace TicTacToeOnline.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly ILogger<GameController> _logger;

    public GameController(IGameService gameService, ILogger<GameController> logger)
    {
        _gameService = gameService;
        _logger = logger;
    }

    [HttpPost("singleplayer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
    public Task<IActionResult> NewGameSinglePlayer([FromBody]NewGameSinglePlayerRequest request)
    {
        return HandleException(() =>
        {
            return _gameService.NewGame(request.PlayerX, request.PlayerO);
        });
    }

    [HttpPost("multiplayer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
    public Task<IActionResult> NewGameMultiPlayer([FromBody] NewGameMultiPlayerRequest request)
    {
        return HandleException(() =>
        {
            return _gameService.NewGame(request.Player);
        });
    }

    [HttpPut("{id}/Join")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(string))]
    public Task<IActionResult> JoinGame([FromRoute] string id, [FromBody] JoinGameRequest request)
    {
        return HandleException(() =>
        {
            return _gameService.JoinGame(id, request.Player);
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetGame([FromRoute]string id)
    {
        return HandleException(() =>
        {
            return _gameService.GetGame(id);
        });
    }

    [HttpPut("{id}/Move")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(string))]
    public Task<IActionResult> MakeMove([FromRoute]string id, [FromBody]MakeMoveRequest request)
    {
        return HandleException(() =>
        {
            return _gameService.MakeMove(id, request.Player, request.Row, request.Column);
        });
    }

    private async Task<IActionResult> HandleException(Func<Task<Game>> func)
    {
        try
        {
            return Ok(await func());
        }
        catch(GameException gex)
        {
            _logger.LogError(gex, "Invalid action on game.");
            return StatusCode((int)gex.StatusCode, gex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");
            return BadRequest(ex.Message);
        }
    }
}
