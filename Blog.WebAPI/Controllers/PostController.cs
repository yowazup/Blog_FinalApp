using Blog.BLL.Services;
using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    // ПОЛУЧЕНИЕ ВСЕХ СТАТЕЙ ОПРЕДЕЛЕННОГО АВТОРА ПО ЕГО ИДЕНТИФИКАТОРУ РЕАЛИЗОВАНО В КОНТРОЛЛЕРЕ USER
    
    [ApiController]
    [Route("api/v1")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> AddPost(PostAddRequest addRequest)
        {
            var newPost = await _postService.AddPost(addRequest.PostContent, addRequest.Tags, addRequest.UserId);
            return StatusCode(201, $"Пост пользователя {newPost.UserId} добавлен. Идентификатор: {newPost.Id}");
        }

        [HttpDelete]
        [Route("posts/:postId")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var deletedPost = await _postService.DeletePost(_postService.GetPostById(postId));
            return StatusCode(201, $"Пост пользователя {deletedPost.UserId} удален. Идентификатор: {deletedPost.Id}");
        }

        [HttpPatch]
        [Route("posts/:postId")]
        public async Task<IActionResult> UpdatePost(int postId, PostUpdateRequest updateRequest)
        {
            var updatedPost = await _postService.UpdatePost(_postService.GetPostById(postId), updateRequest.PostContent, updateRequest.Tags);
            return StatusCode(201, $"Пост пользователя {updatedPost.UserId} обновлен. Идентификатор: {updatedPost.Id}");
        }

        [HttpGet]
        [Route("posts/search")]
        public IActionResult GetPostsByContent([FromQuery] string searchRequest)
        {
            var foundPosts = _postService.GetPostsByContent(searchRequest);
            return StatusCode(201, $"Найдено постов: {foundPosts.Count}");
        }

        [HttpGet]
        [Route("posts")]
        public IActionResult GetAllPosts()
        {
            var allPosts = _postService.GetAllPosts();
            return StatusCode(201, $"Найдено постов: {allPosts.Count}");
        }

        [HttpGet]
        [Route("posts/:postId/tags")]
        public IActionResult GetTagsForPost(int postId)
        {
            var tags = _postService.GetPostById(postId).Tags;
            return StatusCode(201, $"Найдено тегов: {tags.Count}");
        }

        [HttpGet]
        [Route("posts/:postId/comments")]
        public IActionResult GetCommentsForPost(int postId)
        {
            var comments = _postService.GetPostById(postId).Comments;
            return StatusCode(201, $"Найдено комментариев: {comments.Count}");
        }

        [HttpGet]
        [Route("posts/:postId")]
        public IActionResult GetPostById(int postId)
        {
            var foundPost = _postService.GetPostById(postId);

            if (foundPost.Id == postId)
            {
                return StatusCode(201, $"Пост с идентификатором {postId} найден.");
            }
            else
            {
                return StatusCode(404, $"Пост с идентификатором {postId} не найден.");
            }
        }
    }
}
