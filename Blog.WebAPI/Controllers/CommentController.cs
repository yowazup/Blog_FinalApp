using AutoMapper;
using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Comments;
using Blog.WebAPI.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.WebAPI.Controllers
{
    // ПОЛУЧЕНИЕ ВСЕХ КОММЕНТАРИЕВ ОПРЕДЕЛЕННОГО АВТОРА ПО ЕГО ИДЕНТИФИКАТОРУ РЕАЛИЗОВАНО В КОНТРОЛЛЕРЕ USER

    [ApiController]
    [Route("api/v1")]
    public class CommentController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _commentService = commentService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        [Route("comments")]
        public async Task<IActionResult> AddComment(CommentAddRequest addRequest)
        {
            var newComment = await _commentService.AddComment(addRequest.CommentContent, addRequest.PostId, addRequest.UserId);
            return StatusCode(201, _mapper.Map<CommentResponse>(newComment));
        }

        [Authorize]
        [HttpDelete]
        [Route("comments/:commentId")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            if (Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)!.Value == "Администратор"
                || Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)!.Value == "Модератор")
            {
                var deletedComment = await _commentService.DeleteComment(_commentService.GetCommentById(commentId));
                return StatusCode(201, _mapper.Map<CommentResponse>(deletedComment));
            }
            else if (_userService.GetUserByEmail(Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value).Id
                == _commentService.GetCommentById(commentId).UserId)
            {
                var deletedComment = await _commentService.DeleteComment(_commentService.GetCommentById(commentId));
                return StatusCode(201, _mapper.Map<CommentResponse>(deletedComment));
            }

            return StatusCode(403, "У вас нет прав на удаление данного комментария.");
        }

        [Authorize]
        [HttpPatch]
        [Route("comments/:commentId")]
        public async Task<IActionResult> UpdateComment(int commentId, CommentUpdateRequest updateRequest)
        {
            if (Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)!.Value == "Администратор" 
                || Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)!.Value == "Модератор")
            {
                var updatedComment = await _commentService.UpdateComment(_commentService.GetCommentById(commentId), updateRequest.CommentContent);
                return StatusCode(201, _mapper.Map<CommentResponse>(updatedComment));
            }
            else if (_userService.GetUserByEmail(Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value).Id
                == _commentService.GetCommentById(commentId).UserId)
            {
                var updatedComment = await _commentService.UpdateComment(_commentService.GetCommentById(commentId), updateRequest.CommentContent);
                return StatusCode(201, _mapper.Map<CommentResponse>(updatedComment));
            }

            return StatusCode(403, "У вас нет прав на обновление данного комментария.");
        }

        [Authorize]
        [HttpGet]
        [Route("comments/search")]
        public IActionResult GetCommentsByContent([FromQuery] string searchRequest)
        {
            var foundComments = new List<CommentResponse>();
            foreach (var c in _commentService.GetCommentsByContent(searchRequest))
                foundComments.Add(_mapper.Map<CommentResponse>(c));
            return StatusCode(201, foundComments);
        }

        [Authorize]
        [HttpGet]
        [Route("comments")]
        public IActionResult GetAllComments()
        {
            var allComments = new List<CommentResponse>();
            foreach (var c in _commentService.GetAllComments())
                allComments.Add(_mapper.Map<CommentResponse>(c));
            return StatusCode(201, allComments);
        }

        [Authorize]
        [HttpGet]
        [Route("comments/:commentId")]
        public IActionResult GetCommentById(int commentId)
        {
            try
            {
                var foundComment = _commentService.GetCommentById(commentId);
                return StatusCode(201, _mapper.Map<CommentResponse>(foundComment));
            }
            catch (InvalidOperationException)
            {
                var response = new StatusCodeResponse()
                {
                    StatusCode = 404,
                    Comment = "Комментария с таким идентификатором не существует.",
                };
                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
