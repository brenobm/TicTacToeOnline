namespace TicTacToeOnline.WebApi.Dtos.Requests;

public class NewGameSinglePlayerRequest
{
    public string PlayerX { get; set; }
    public string PlayerO { get; set; }

    public NewGameSinglePlayerRequest(string playerX, string playerO)
    {
        PlayerX = playerX;
        PlayerO = playerO;
    }
}
