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

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatus))]
    public IActionResult NewGame([FromBody]NewGameRequest request)
    {
        var game = _gameService.NewGame(request.PlayerXId, request.PlayerXName, request.PlayerOId, request.PlayerOName);
        return Ok(game);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatus))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetGame([FromRoute]string id)
    {
        var game = _gameService.GetGame(id);
        return game != null ? Ok(game) : NotFound("");
    }

    [HttpPut("{id}/Move")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GameStatus))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable, Type = typeof(string))]
    public IActionResult MakeMove([FromRoute]string id, [FromBody]MakeMoveRequest request)
    {
        try
        {
            var game = _gameService.MakeMove(id, request.PlayerId, request.Row, request.Column);
            return game != null ? Ok(game) : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
