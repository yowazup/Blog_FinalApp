using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Comments;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    // ПОЛУЧЕНИЕ ВСЕХ КОММЕНТАРИЕВ ОПРЕДЕЛЕННОГО АВТОРА ПО ЕГО ИДЕНТИФИКАТОРУ РЕАЛИЗОВАНО В КОНТРОЛЛЕРЕ USER

    [ApiController]
    [Route("api/v1")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("comments")]
        public async Task<IActionResult> AddComment(CommentAddRequest addRequest)
        {
            var newComment = await _commentService.AddComment(addRequest.CommentContent, addRequest.PostId, addRequest.UserId);
            return StatusCode(201, $"Комментарий пользователя {newComment.UserId} добавлен. Идентификатор: {newComment.Id}");
        }

        [HttpDelete]
        [Route("comments/:commentId")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var deletedComment = await _commentService.DeleteComment(_commentService.GetCommentById(commentId));
            return StatusCode(201, $"Комментарий пользователя {deletedComment.UserId} добавлен. Идентификатор: {deletedComment.Id}");
        }

        [HttpPatch]
        [Route("comments/:commentId")]
        public async Task<IActionResult> UpdateComment(int commentId, CommentUpdateRequest updateRequest)
        {
            var updatedComment = await _commentService.UpdateComment(_commentService.GetCommentById(commentId), updateRequest.CommentContent);
            return StatusCode(201, $"Комментарий пользователя {updatedComment.UserId} обновлен. Идентификатор: {updatedComment.Id}");
        }

        [HttpGet]
        [Route("comments/search")]
        public IActionResult GetCommentsByContent([FromQuery] string searchRequest)
        {
            var foundComments = _commentService.GetCommentsByContent(searchRequest);
            return StatusCode(201, $"Найдено комментариев: {foundComments.Count}");
        }

        [HttpGet]
        [Route("comments")]
        public IActionResult GetAllComments()
        {
            var allComments = _commentService.GetAllComments();
            return StatusCode(201, $"Найдено комментариев: {allComments.Count}");
        }

        [HttpGet]
        [Route("comments/:commentId")]
        public IActionResult GetCommentById(int commentId)
        {
            var foundComment = _commentService.GetCommentById(commentId);

            if (foundComment.Id == commentId)
            {
                return StatusCode(201, $"Комментарий с идентификатором {commentId} найден.");
            }
            else
            {
                return StatusCode(404, $"Комментарий с идентификатором {commentId} не найден.");
            }
        }
    }
}
