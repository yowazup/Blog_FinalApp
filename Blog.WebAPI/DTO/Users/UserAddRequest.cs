
namespace Blog.WebAPI.DTO.Users
{
    public class UserAddRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string Photo { get; set; } = string.Empty;
    }
}
