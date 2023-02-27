namespace TicTacToeOnline.WebApi.Dtos.Requests;

public class MakeMoveRequest
{
    public string Player { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }

    public MakeMoveRequest(string player, int row, int column)
    {
        Player = player;
        Row = row;
        Column = column;
    }

}
