
namespace Blog.DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public required string CommentContent { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
