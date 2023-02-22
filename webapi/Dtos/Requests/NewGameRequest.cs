namespace webapi.Dtos.Requests
{
    public class NewGameRequest
    {
        public string PlayerXId { get; set; }
        public string PlayerXName { get; set; }
        public string PlayerOId { get; set; }
        public string PlayerOName { get; set; }
    }
}
