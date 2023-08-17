using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogContext _context;

        public TagRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddTag(Tag tag)
        {
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                _context.Add(tag);
            await _context.SaveChangesAsync();
        }

        public List<Tag> GetAllTags()
        {
            return _context.Tags.Include(x => x.Posts).ToList();
        }

        public List<Tag> GetTagsByContent(string tagContent)
        {
            return GetAllTags().Where(t => t.TagContent.ToLower().Contains(tagContent.ToLower())).ToList();
        }

        public Tag GetTagById(int tagId)
        {
            return GetAllTags().Single(x => x.Id == tagId);
        }

        public async Task DeleteTag(int tagId)
        {
            _context.Remove(GetTagById(tagId));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTag(int tagId, string tagContent)
        {
            var tag = GetTagById(tagId);
            tag.TagContent = tagContent;

            _context.Update(tag);
            await _context.SaveChangesAsync();
        }
    }
}
