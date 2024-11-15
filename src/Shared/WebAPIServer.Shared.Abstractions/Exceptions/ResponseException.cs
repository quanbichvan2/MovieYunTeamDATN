namespace WebAPIServer.Shared.Abstractions.Exceptions
{
    public class ResponseException
    {
        public string Message { get; set; } = default!;
        public bool IsSuccess { get; set; }
        public Dictionary<string, List<object>>? Errors { get; set; } = new Dictionary<string, List<object>>();
    }
}