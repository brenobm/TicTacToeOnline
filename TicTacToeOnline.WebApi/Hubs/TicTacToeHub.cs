using Microsoft.AspNetCore.SignalR;
using TicTacToeOnline.WebApi.Models;

namespace TicTacToeOnline.WebApi.Hubs;

public class TicTacToeHub : Hub
{
    public async Task SendMessage(string player, Game game)
    {
        await Clients.All.SendAsync("GameMove", player, game);
    }
}
