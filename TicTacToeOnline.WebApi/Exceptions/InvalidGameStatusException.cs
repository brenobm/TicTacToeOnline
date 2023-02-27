using System.Net;

namespace TicTacToeOnline.WebApi.Exceptions;

public class InvalidGameStatusException : GameException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.PreconditionFailed;

    public InvalidGameStatusException(string message) : base(message) { }
}
