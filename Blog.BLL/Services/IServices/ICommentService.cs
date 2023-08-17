using Blog.DAL.Models;

namespace Blog.BLL.Services.IServices
{
    public interface ICommentService
    {
        Task<Comment> AddComment(string commentContent, int postId, int userId);
        Task<Comment> UpdateComment(Comment comment, string commentContent);
        Task<Comment> DeleteComment(Comment comment);
        List<Comment> GetAllComments();
        List<Comment> GetCommentsByContent(string commentContent);
        Comment GetCommentById(int commentId);
    }
}
