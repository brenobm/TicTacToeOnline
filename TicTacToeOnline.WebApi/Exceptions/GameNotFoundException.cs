using System.Net;

namespace TicTacToeOnline.WebApi.Exceptions;

public class GameNotFoundException : GameException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
