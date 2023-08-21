using AutoMapper;
using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Posts;
using Blog.WebAPI.DTO.Responses;
using Blog.WebAPI.DTO.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        [Route("tags")]
        public async Task<IActionResult> AddTag(TagAddRequest addRequest)
        {
            var newTag = await _tagService.AddTag(addRequest.TagContent);
            return StatusCode(201, _mapper.Map<TagResponse>(newTag));
        }

        [Authorize(Roles = "Администратор, Модератор")]
        [HttpDelete]
        [Route("tags/:tagId")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            var deletedTag = await _tagService.DeleteTag(_tagService.GetTagById(tagId));
            return StatusCode(201, _mapper.Map<TagResponse>(deletedTag));
        }

        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPatch]
        [Route("tags/:tagId")]
        public async Task<IActionResult> UpdateTag(int tagId, TagUpdateRequest updateRequest)
        {
            var updatedTag = await _tagService.UpdateTag(_tagService.GetTagById(tagId), updateRequest.TagContent);
            return StatusCode(201, _mapper.Map<TagResponse>(updatedTag));

        }

        [Authorize]
        [HttpGet]
        [Route("tags/search")]
        public IActionResult GetTagsByContent(string searchRequest)
        {
            var foundTags = new List<TagResponse>();
            foreach (var t in _tagService.GetTagsByContent(searchRequest))
                foundTags.Add(_mapper.Map<TagResponse>(t));
            return StatusCode(201, foundTags);
        }

        [Authorize]
        [HttpGet]
        [Route("tags")]
        public IActionResult GetAllTags()
        {
            var allTags = new List<TagResponse>();
            foreach (var t in _tagService.GetAllTags())
                allTags.Add(_mapper.Map<TagResponse>(t));
            return StatusCode(201, allTags);
        }

        [Authorize]
        [HttpGet]
        [Route("tags/:tagId/posts")]
        public IActionResult GetPostsForTag(int tagId)
        {
            var posts = _tagService.GetTagById(tagId).Posts;
            return StatusCode(201, _mapper.Map<PostResponse>(posts));
        }

        [Authorize]
        [HttpGet]
        [Route("tags/:tagId")]
        public IActionResult GetTagById(int tagId)
        {
            try
            {
                var foundTag = _tagService.GetTagById(tagId);
                return StatusCode(201, _mapper.Map<TagResponse>(foundTag));
            }
            catch (InvalidOperationException)
            {
                var response = new StatusCodeResponse()
                {
                    StatusCode = 404,
                    Comment = "Тега с таким идентификатором не существует.",
                };
                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
