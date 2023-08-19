
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
        public Role Role { get; set; } = new Role();
        public List<Post> Posts { get; } = new List<Post>();
        public List<Comment> Comments { get; } = new List<Comment>();
    }
}
