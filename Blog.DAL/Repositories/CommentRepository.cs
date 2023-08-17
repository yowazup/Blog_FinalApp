using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public CommentRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment, int userId, int postId)
        {
            comment.UserId = userId;
            comment.PostId = postId;

            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
                _context.Add(comment);
            await _context.SaveChangesAsync();
        }

        public List<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }

        public Comment GetCommentById(int commentId)
        {
            return GetAllComments().Single(x => x.Id == commentId);
        }

        public List<Comment> GetCommentsByContent(string commentContent)
        {
            return GetAllComments().Where(t => t.CommentContent.ToLower().Contains(commentContent.ToLower())).ToList();
        }

        public async Task DeleteComment(int commentId)
        {
            _context.Remove(GetCommentById(commentId));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(int commentId, string commentContent)
        {
            var comment = GetCommentById(commentId);
            comment.CommentContent = commentContent;

            _context.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}
