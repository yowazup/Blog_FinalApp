
namespace Blog.WebAPI.DTO.Posts
{
    public class PostAddRequest
    {
        public required string PostContent { get; set; }
        public List<string> Tags { get; set; } = null!;
        public required int UserId { get; set; }
    }
}
