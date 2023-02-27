using System.Net;

namespace TicTacToeOnline.WebApi.Exceptions;

public class NonExpectedError : GameException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public NonExpectedError(string message) : base(message) { }
}
