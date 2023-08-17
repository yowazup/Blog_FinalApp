using Blog.DAL.Models;

namespace Blog.BLL.Services.IServices
{
    public interface IPostService
    {
        Task<Post> AddPost(string postContent, List<string> tags, int userId);
        Task<Post> UpdatePost(Post post, string postContent, List<string> tags);
        Task<Post> DeletePost(Post post);
        List<Post> GetAllPosts();
        List<Post> GetPostsByContent(string postContent);
        Post GetPostById(int postId);
    }
}
