using Blog.DAL.Models;

namespace Blog.WebAPI.DTO.Tags
{
    public class TagResponse
    {
        public int Id { get; set; }
        public string TagContent { get; set; }
        public List<Post> Posts { get; set; }

        public TagResponse(Tag tag) 
        {
            Id = tag.Id;
            TagContent = tag.TagContent;
            Posts = tag.Posts;
        }
    }
}
