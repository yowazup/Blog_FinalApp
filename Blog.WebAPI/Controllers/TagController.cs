using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Tags;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        [Route("tags")]
        public async Task<IActionResult> AddTag(TagAddRequest addRequest)
        {
            var newTag = await _tagService.AddTag(addRequest.TagContent);
            return StatusCode(201, $"Тег добавлен. Идентификатор: {newTag.Id}");
        }

        [HttpDelete]
        [Route("tags/:tagId")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            var deletedTag = await _tagService.DeleteTag(_tagService.GetTagById(tagId));
            return StatusCode(201, $"Тег удален. Идентификатор: {deletedTag.Id}");
        }

        [HttpPatch]
        [Route("tags/:tagId")]
        public async Task<IActionResult> UpdateComment(int tagId, TagUpdateRequest updateRequest)
        {
            var updatedTag = await _tagService.UpdateTag(_tagService.GetTagById(tagId), updateRequest.TagContent);
            return StatusCode(201, $"Тег обновлен. Идентификатор: {updatedTag.Id}");
        }

        [HttpGet]
        [Route("tags/search")]
        public IActionResult GetTagsByContent([FromQuery] string searchRequest)
        {
            var foundTags = _tagService.GetTagsByContent(searchRequest);
            return StatusCode(201, $"Найдено тегов: {foundTags.Count}");
        }

        [HttpGet]
        [Route("tags")]
        public IActionResult GetAllTags()
        {
            var allTags = _tagService.GetAllTags();
            return StatusCode(201, $"Найдено тегов: {allTags.Count}");
        }

        [HttpGet]
        [Route("tags/:tagId/posts")]
        public IActionResult GetPostsForTag(int tagId)
        {
            var posts = _tagService.GetTagById(tagId).Posts;
            return StatusCode(201, $"Найдено постов: {posts.Count}");
        }

        [HttpGet]
        [Route("tags/:tagId")]
        public IActionResult GetTagById(int tagId)
        {
            var foundTag = _tagService.GetTagById(tagId);

            if (foundTag.Id == tagId)
            {
                return StatusCode(201, $"Тег с идентификатором {tagId} найден.");
            }
            else
            {
                return StatusCode(404, $"Тег с идентификатором {tagId} не найден.");
            }
        }
    }
}
