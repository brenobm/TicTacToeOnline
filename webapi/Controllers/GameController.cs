using Microsoft.AspNetCore.Mvc;
using webapi.Dtos.Requests;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("singleplayer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatus))]
    public async Task<IActionResult> NewGameSinglePlayer([FromBody]NewGameSinglePlayerRequest request)
    {
        var game = await _gameService.NewGame(request.PlayerXId, request.PlayerXName, request.PlayerOId, request.PlayerOName);      
        return Ok(game);
    }

    [HttpPost("multiplayer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatus))]
    public async Task<IActionResult> NewGameMultiPlayer([FromBody] NewGameMultiPlayerRequest request)
    {
        var game = await _gameService.NewGame(request.PlayerXId, request.PlayerXName);
        return Ok(game);
    }

    [HttpPut("{id}/Join")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatus))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(string))]
    public async Task<IActionResult> JoinGame([FromRoute] string id, [FromBody] JoinGameRequest request)
    {
        try
        {
            var game = await _gameService.JoinGame(id, request.PlayerOId, request.PlayerOName);
            return game != null ? Ok(game) : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatus))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGame([FromRoute]string id)
    {
        var game = await _gameService.GetGame(id);
        return game != null ? Ok(game) : NotFound("");
    }

    [HttpPut("{id}/Move")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatus))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(string))]
    public async Task<IActionResult> MakeMove([FromRoute]string id, [FromBody]MakeMoveRequest request)
    {
        try
        {
            var game = await _gameService.MakeMove(id, request.PlayerId, request.Row, request.Column);
            return game != null ? Ok(game) : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
