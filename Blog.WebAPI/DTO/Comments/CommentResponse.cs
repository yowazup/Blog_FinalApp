using Blog.DAL.Models;

namespace Blog.WebAPI.DTO.Comments
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public CommentResponse(Comment comment) 
        {
            Id = comment.Id;
            CommentContent = comment.CommentContent;
            UserId = comment.UserId;
            PostId = comment.PostId;
        }
    }
}
