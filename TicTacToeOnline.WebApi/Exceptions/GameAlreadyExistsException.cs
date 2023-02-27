using System.Net;

namespace TicTacToeOnline.WebApi.Exceptions;

public class GameAlreadyExistsException : GameException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}
