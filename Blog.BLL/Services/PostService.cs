using Blog.BLL.Services.IServices;
using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;

namespace Blog.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepo;
        private readonly ITagService _tagService;

        public PostService(IPostRepository postRepo, ITagService tagService)
        {
            _postRepo = postRepo;
            _tagService = tagService;
        }

        public async Task<Post> AddPost(string postContent, List<string> tags, int userId)
        {
            var postTags = await Task.WhenAll(tags.Select(async t => await _tagService.AddTag(t)));
            var post = new Post
            {
                PostContent = postContent,
                Tags = postTags.ToList()
            };

            await _postRepo.AddPost(post, userId);
            return post;
        }

        public async Task<Post> UpdatePost(Post post, string postContent, List<string> tags)
        {
            var postTags = await Task.WhenAll(tags.Select(async t => await _tagService.AddTag(t)));
            var updatedPost = new Post
            {
                Id = post.Id,
                PostContent = postContent,
                UserId = post.UserId,
                Tags = postTags.ToList(),
                Comments = post.Comments
            };

            await _postRepo.UpdatePost(updatedPost.Id, updatedPost.PostContent, updatedPost.Tags);
            return updatedPost;
        }

        public async Task<Post> DeletePost(Post post)
        {
            var deletedPost = post;
            await _postRepo.DeletePost(post.Id);
            return deletedPost;
        }

        public List<Post> GetAllPosts()
        {
            return _postRepo.GetAllPosts();
        }

        public List<Post> GetPostsByContent(string postContent)
        {
            return _postRepo.GetPostsByContent(postContent);
        }

        public Post GetPostById(int postId)
        {
            return _postRepo.GetPostById(postId);
        }
    }
}
