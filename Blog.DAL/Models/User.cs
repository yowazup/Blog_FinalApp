
namespace Blog.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string? Photo { get; set; }
        public Role Role { get; set; } = null!;
        public List<Post> Posts { get; } = null!;
        public List<Comment> Comments { get; } = null!;
    }
}
