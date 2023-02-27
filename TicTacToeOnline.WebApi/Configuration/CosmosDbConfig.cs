namespace TicTacToeOnline.WebApi.Configuration;

public class CosmosDbConfig
{
    public string Endpoint { get; set; }
    public string AuthKey { get; set; }
    public string DatabaseName { get; set; }
    public string Container { get; set; }
}
