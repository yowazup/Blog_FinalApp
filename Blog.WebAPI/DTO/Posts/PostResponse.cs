using Blog.DAL.Models;

namespace Blog.WebAPI.DTO.Posts
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string PostContent { get; set; }
        public int UserId { get; set; }

        public PostResponse(Post post)
        {
            Id = post.Id;
            PostContent = post.PostContent;
            UserId = post.UserId;
        }
    }
}
