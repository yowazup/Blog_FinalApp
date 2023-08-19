
namespace Blog.WebAPI.DTO.Users
{
    public class UserUpdateRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string Photo { get; set; } = string.Empty;
        public int RoleId { get; set; } = 1;
    }
}