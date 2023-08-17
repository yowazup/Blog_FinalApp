
namespace Blog.WebAPI.DTO.Comments
{
    public class CommentAddRequest
    {
        public required string CommentContent { get; set; }
        public required int UserId { get; set; }
        public required int PostId { get; set; }
    }
}
