namespace webapi.Models;

public class GameStatus
{
    public GameStatus(string id)
    {
        Id = id;
        Board = new Element[3][]
        {
            new Element[3], 
            new Element[3], 
            new Element[3]
        };
        Status = PlayStatus.RUNNING;
    }

    public string Id { get; set; }
    public Player PlayerX { get; set; }
    public Player PlayerO { get; set; }
    public Element[][] Board { get; set; }
    public PlayStatus Status { get; set; }
    public Player? Winner { get; set; }
    public Player Turn { get; set; }
}
