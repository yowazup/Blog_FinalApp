
namespace Blog.DAL.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public required string TagContent { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
