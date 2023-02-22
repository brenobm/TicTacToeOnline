namespace webapi.Dtos.Requests
{
    public class MakeMoveRequest
    {
        public string PlayerId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
