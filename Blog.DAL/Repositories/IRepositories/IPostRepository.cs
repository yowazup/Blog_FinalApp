using Blog.DAL.Models;

namespace Blog.DAL.Repositories.IRepositories
{
    public interface IPostRepository
    {
        Task AddPost(Post post, int userId);
        Task UpdatePost(int postId, string postContent, List<Tag> tags);
        Task DeletePost(int postId);
        List<Post> GetAllPosts();
        Post GetPostById(int postId);
        List<Post> GetPostsByContent(string postContent);
    }
}
