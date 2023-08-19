using AutoMapper;
using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Comments;
using Blog.WebAPI.DTO.Posts;
using Blog.WebAPI.DTO.Responses;
using Blog.WebAPI.DTO.Tags;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    // ПОЛУЧЕНИЕ ВСЕХ СТАТЕЙ ОПРЕДЕЛЕННОГО АВТОРА ПО ЕГО ИДЕНТИФИКАТОРУ РЕАЛИЗОВАНО В КОНТРОЛЛЕРЕ USER

    [ApiController]
    [Route("api/v1")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> AddPost(PostAddRequest addRequest)
        {
            var newPost = await _postService.AddPost(addRequest.PostContent, addRequest.Tags, addRequest.UserId);
            return StatusCode(201, _mapper.Map<PostResponse>(newPost));
        }

        [HttpDelete]
        [Route("posts/:postId")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var deletedPost = await _postService.DeletePost(_postService.GetPostById(postId));
            return StatusCode(201, _mapper.Map<PostResponse>(deletedPost));
        }

        [HttpPatch]
        [Route("posts/:postId")]
        public async Task<IActionResult> UpdatePost(int postId, PostUpdateRequest updateRequest)
        {
            var updatedPost = await _postService.UpdatePost(_postService.GetPostById(postId), updateRequest.PostContent, updateRequest.Tags);
            return StatusCode(201, _mapper.Map<PostResponse>(updatedPost));
        }

        [HttpGet]
        [Route("posts/search")]
        public IActionResult GetPostsByContent([FromQuery] string searchRequest)
        {
            var foundPosts = new List<PostResponse>();
            foreach (var p in _postService.GetPostsByContent(searchRequest))
                foundPosts.Add(_mapper.Map<PostResponse>(p));
            return StatusCode(201, foundPosts);
        }

        [HttpGet]
        [Route("posts")]
        public IActionResult GetAllPosts()
        {
            var allPosts = new List<PostResponse>();
            foreach (var p in _postService.GetAllPosts())
                allPosts.Add(_mapper.Map<PostResponse>(p));
            return StatusCode(201, allPosts);
        }

        [HttpGet]
        [Route("posts/:postId/tags")]
        public IActionResult GetTagsForPost(int postId)
        {
            var tags = _postService.GetPostById(postId).Tags;
            return StatusCode(201, _mapper.Map<TagResponse>(tags));
        }

        [HttpGet]
        [Route("posts/:postId/comments")]
        public IActionResult GetCommentsForPost(int postId)
        {
            var comments = _postService.GetPostById(postId).Comments;
            return StatusCode(201, _mapper.Map<CommentResponse>(comments));
        }

        [HttpGet]
        [Route("posts/:postId")]
        public IActionResult GetPostById(int postId)
        {
            try
            {
                var foundPost = _postService.GetPostById(postId);
                return StatusCode(201, _mapper.Map<PostResponse>(foundPost));
            }
            catch (InvalidOperationException)
            {
                var response = new ServerResponse()
                {
                    StatusCode = 404,
                    Comment = "Поста с таким идентификатором не существует.",
                };
                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
