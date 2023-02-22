namespace webapi.Exceptions;

public class InvalidGameStatusException : Exception
{
    public InvalidGameStatusException(string message) : base(message) { }
}
