using AutoMapper;
using Blog.BLL.Services;
using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Comments;
using Blog.WebAPI.DTO.Posts;
using Blog.WebAPI.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    // ПОЛУЧЕНИЕ ВСЕХ КОММЕНТАРИЕВ ОПРЕДЕЛЕННОГО АВТОРА ПО ЕГО ИДЕНТИФИКАТОРУ РЕАЛИЗОВАНО В КОНТРОЛЛЕРЕ USER

    [ApiController]
    [Route("api/v1")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("comments")]
        public async Task<IActionResult> AddComment(CommentAddRequest addRequest)
        {
            var newComment = await _commentService.AddComment(addRequest.CommentContent, addRequest.PostId, addRequest.UserId);
            return StatusCode(201, _mapper.Map<CommentResponse>(newComment));
        }

        [HttpDelete]
        [Route("comments/:commentId")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var deletedComment = await _commentService.DeleteComment(_commentService.GetCommentById(commentId));
            return StatusCode(201, _mapper.Map<CommentResponse>(deletedComment));
        }

        [HttpPatch]
        [Route("comments/:commentId")]
        public async Task<IActionResult> UpdateComment(int commentId, CommentUpdateRequest updateRequest)
        {
            var updatedComment = await _commentService.UpdateComment(_commentService.GetCommentById(commentId), updateRequest.CommentContent);
            return StatusCode(201, _mapper.Map<CommentResponse>(updatedComment));
        }

        [HttpGet]
        [Route("comments/search")]
        public IActionResult GetCommentsByContent([FromQuery] string searchRequest)
        {
            var foundComments = new List<CommentResponse>();
            foreach (var c in _commentService.GetCommentsByContent(searchRequest))
                foundComments.Add(_mapper.Map<CommentResponse>(c));
            return StatusCode(201, foundComments);
        }

        [HttpGet]
        [Route("comments")]
        public IActionResult GetAllComments()
        {
            var allComments = new List<CommentResponse>();
            foreach (var c in _commentService.GetAllComments())
                allComments.Add(_mapper.Map<CommentResponse>(c));
            return StatusCode(201, allComments);
        }

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
                var response = new ServerResponse()
                {
                    StatusCode = 404,
                    Comment = "Комментария с таким идентификатором не существует.",
                };
                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
