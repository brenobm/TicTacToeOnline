namespace TicTacToeOnline.WebApi.Dtos.Requests;

public class NewGameMultiPlayerRequest
{
    public string Player { get; set; }

    public NewGameMultiPlayerRequest(string player)
    {
        Player = player;
    }
}
