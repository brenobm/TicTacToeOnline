namespace TicTacToeOnline.WebApi.Dtos.Requests;

public class JoinGameRequest
{
    public string Player { get; set; }

    public JoinGameRequest(string player)
    {
        Player = player;
    }
}
