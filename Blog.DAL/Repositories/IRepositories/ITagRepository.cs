using Blog.DAL.Models;

namespace Blog.DAL.Repositories.IRepositories
{
    public interface ITagRepository
    {
        Task AddTag(Tag tag);
        Task UpdateTag(int tagId, string tagContent);
        Task DeleteTag(int tagId);
        List<Tag> GetAllTags();
        List <Tag> GetTagsByContent(string tagContent);
        Tag GetTagById(int tagId);
    }
}
