using AutoMapper;
using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Comments;
using Blog.WebAPI.DTO.Posts;
using Blog.WebAPI.DTO.Responses;
using Blog.WebAPI.DTO.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.WebAPI.Controllers
{
    // ПОЛУЧЕНИЕ ВСЕХ СТАТЕЙ ОПРЕДЕЛЕННОГО АВТОРА ПО ЕГО ИДЕНТИФИКАТОРУ РЕАЛИЗОВАНО В КОНТРОЛЛЕРЕ USER

    [ApiController]
    [Route("api/v1")]
    public class PostController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _postService = postService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> AddPost(PostAddRequest addRequest)
        {
            var newPost = await _postService.AddPost(addRequest.PostContent, addRequest.Tags, addRequest.UserId);
            return StatusCode(201, _mapper.Map<PostResponse>(newPost));
        }

        [Authorize]
        [HttpDelete]
        [Route("posts/:postId")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            if (Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)!.Value == "Администратор" 
                || Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)!.Value == "Модератор")
            {
                var deletedPost = await _postService.DeletePost(_postService.GetPostById(postId));
                return StatusCode(201, _mapper.Map<PostResponse>(deletedPost));
            }
            else if (_userService.GetUserByEmail(Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value).Id
                == _postService.GetPostById(postId).UserId)
            {
                var deletedPost = await _postService.DeletePost(_postService.GetPostById(postId));
                return StatusCode(201, _mapper.Map<PostResponse>(deletedPost));
            }

            return StatusCode(403, "У вас нет прав на удаление данного поста.");
        }

        [Authorize]
        [HttpPatch]
        [Route("posts/:postId")]
        public async Task<IActionResult> UpdatePost(int postId, PostUpdateRequest updateRequest)
        {
            if (Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)!.Value == "Администратор"
                || Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)!.Value == "Модератор")
            {
                var updatedPost = await _postService.UpdatePost(_postService.GetPostById(postId), updateRequest.PostContent, updateRequest.Tags);
                return StatusCode(201, _mapper.Map<PostResponse>(updatedPost));
            }
            else if (_userService.GetUserByEmail(Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value).Id 
                == _postService.GetPostById(postId).UserId)
            {
                var updatedPost = await _postService.UpdatePost(_postService.GetPostById(postId), updateRequest.PostContent, updateRequest.Tags);
                return StatusCode(201, _mapper.Map<PostResponse>(updatedPost));
            }

            return StatusCode(403, "У вас нет прав на обновление данного поста.");
        }

        [Authorize]
        [HttpGet]
        [Route("posts/search")]
        public IActionResult GetPostsByContent([FromQuery] string searchRequest)
        {
            var foundPosts = new List<PostResponse>();
            foreach (var p in _postService.GetPostsByContent(searchRequest))
                foundPosts.Add(_mapper.Map<PostResponse>(p));
            return StatusCode(201, foundPosts);
        }

        [Authorize]
        [HttpGet]
        [Route("posts")]
        public IActionResult GetAllPosts()
        {
            var allPosts = new List<PostResponse>();
            foreach (var p in _postService.GetAllPosts())
                allPosts.Add(_mapper.Map<PostResponse>(p));
            return StatusCode(201, allPosts);
        }

        [Authorize]
        [HttpGet]
        [Route("posts/:postId/tags")]
        public IActionResult GetTagsForPost(int postId)
        {
            var tags = _postService.GetPostById(postId).Tags;
            return StatusCode(201, _mapper.Map<TagResponse>(tags));
        }

        [Authorize]
        [HttpGet]
        [Route("posts/:postId/comments")]
        public IActionResult GetCommentsForPost(int postId)
        {
            var comments = _postService.GetPostById(postId).Comments;
            return StatusCode(201, _mapper.Map<CommentResponse>(comments));
        }

        [Authorize]
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
                var response = new StatusCodeResponse()
                {
                    StatusCode = 404,
                    Comment = "Поста с таким идентификатором не существует.",
                };
                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
