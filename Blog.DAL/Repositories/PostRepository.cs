using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddPost(Post post, int userId)
        {
            post.UserId = userId;

            var entry = _context.Entry(post);
            if (entry.State == EntityState.Detached)
                _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.Include(x => x.Tags).ToList();
        }

        public Post GetPostById(int postId)
        {
            return GetAllPosts().Single(x => x.Id == postId);
        }

        public List<Post> GetPostsByContent(string postContent)
        {
            return GetAllPosts().Where(t => t.PostContent.ToLower().Contains(postContent.ToLower())).ToList();
        }

        public async Task DeletePost(int postId)
        {
            _context.Remove(GetPostById(postId));
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePost(int postId, string postContent, List<Tag> tags)
        {
            var post = GetPostById(postId);
            post.PostContent = postContent;
            post.Tags = tags;

            _context.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
