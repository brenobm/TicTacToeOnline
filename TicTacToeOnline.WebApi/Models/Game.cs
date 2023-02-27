using Newtonsoft.Json;

namespace TicTacToeOnline.WebApi.Models;

public class Game
{
    public Game(string id, string playerX)
    {
        Id = id;
        Board = new Element[3][]
        {
            new Element[3], 
            new Element[3], 
            new Element[3]
        };
        Status = GameStatus.WAITING;
        PlayerX = playerX;
    }
    public Game(string id, string playerX, string playerO, string starts) : this (id, playerX)
    {
        Status = GameStatus.RUNNING;
        PlayerO = playerO;
        Turn = starts;
    }

    [JsonConstructor]
    public Game(string id, string playerX, string playerO, Element[][] board, GameStatus status, string? winner, string starts)
    {
        Id = id;
        Board = board;
        PlayerX = playerX;
        PlayerO = playerO;
        Status = status;
        Winner = winner;
        Turn = starts;
    }

    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string PlayerX { get; set; }
    public string? PlayerO { get; set; }
    public Element[][] Board { get; set; }
    public GameStatus Status { get; set; }
    public string? Winner { get; set; }
    public string? Turn { get; set; }
}
