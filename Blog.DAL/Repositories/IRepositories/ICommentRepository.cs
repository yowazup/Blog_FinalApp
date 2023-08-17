using Blog.DAL.Models;

namespace Blog.DAL.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment, int userId, int postId);
        Task UpdateComment(int commentId, string commentContent);
        Task DeleteComment(int commentId);
        List<Comment> GetAllComments();
        Comment GetCommentById(int commentId);
        List<Comment> GetCommentsByContent(string commentContent);
    }
}
