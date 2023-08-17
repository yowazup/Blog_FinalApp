using Blog.BLL.Services.IServices;
using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;

namespace Blog.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;

        public CommentService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<Comment> AddComment(string commentContent, int postId, int userId)
        {
            var comment = new Comment
            {
                CommentContent = commentContent,
                UserId = userId,
                PostId = postId
            };
            await _commentRepo.AddComment(comment, userId, postId);
            return comment;
        }

        public async Task<Comment> UpdateComment(Comment comment, string commentContent)
        {
            await _commentRepo.UpdateComment(comment.Id, commentContent);
            return _commentRepo.GetCommentById(comment.Id);
        }

        public async Task<Comment> DeleteComment(Comment comment)
        {
            var deletedComment = comment;
            await _commentRepo.DeleteComment(comment.Id);
            return deletedComment;
        }

        public List<Comment> GetAllComments()
        {
            return _commentRepo.GetAllComments();
        }

        public List<Comment> GetCommentsByContent(string commentContent)
        {
            return _commentRepo.GetCommentsByContent(commentContent);
        }

        public Comment GetCommentById(int commentId)
        {
            return _commentRepo.GetCommentById(commentId);
        }
    }
}
