
namespace Blog.WebAPI.DTO.Posts
{
    public class PostUpdateRequest
    {
        public required string PostContent { get; set; }
        public List<string> Tags { get; set; } = null!;
    }
}
