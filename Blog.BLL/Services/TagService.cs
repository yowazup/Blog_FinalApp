using Blog.BLL.Services.IServices;
using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;
using Microsoft.IdentityModel.Tokens;

namespace Blog.BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepo;

        public TagService(ITagRepository tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public async Task<Tag> AddTag(string tagContent)
        {
            var tag = new Tag
            { 
                TagContent = tagContent
            };

            if (GetTagsByContent(tagContent).IsNullOrEmpty())
            {
                await _tagRepo.AddTag(tag);
            }
            else
            {
                tag = GetTagsByContent(tagContent).First();
            }

            return tag;
        }

        public async Task<Tag> UpdateTag(Tag tag, string tagContent)
        {
            var updatedTag = new Tag { TagContent = tagContent };
            await _tagRepo.UpdateTag(updatedTag.Id, updatedTag.TagContent);
            return updatedTag;
        }

        public async Task<Tag> DeleteTag(Tag tag)
        {
            var deletedTag = tag;
            await _tagRepo.DeleteTag(tag.Id);
            return deletedTag;
        }

        public List<Tag> GetAllTags()
        {
            return _tagRepo.GetAllTags();
        }

        public List<Tag> GetTagsByContent(string tagContent)
        {
            return _tagRepo.GetTagsByContent(tagContent);
        }

        public Tag GetTagById(int tagId)
        {
            return _tagRepo.GetTagById(tagId);
        }
    }
}
