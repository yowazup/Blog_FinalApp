using Blog.DAL.Models;

namespace Blog.BLL.Services.IServices
{
    public interface ITagService
    {
        Task<Tag> AddTag(string tagContent);
        Task<Tag> UpdateTag(Tag tag, string tagContent);
        Task<Tag> DeleteTag(Tag tag);
        List<Tag> GetAllTags();
        List<Tag> GetTagsByContent(string tagContent);
        Tag GetTagById(int tagId);
    }
}
