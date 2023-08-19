namespace Blog.WebAPI.DTO.Responses
{
    public class ServerResponse
    {
        public int StatusCode { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Id { get; set; } = 0;
    }
}
