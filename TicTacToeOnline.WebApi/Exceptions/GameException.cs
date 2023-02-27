using System.Net;

namespace TicTacToeOnline.WebApi.Exceptions;

abstract public class GameException : Exception
{
    public virtual HttpStatusCode StatusCode { get; }

    public GameException() : base() { }

    public GameException(string message) : base(message) { }

}
