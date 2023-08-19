using Blog.DAL.Models;

namespace Blog.WebAPI.DTO.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Photo { get; set; }
        public int RoleId { get; set; }

        public UserResponse(User user)
        {
            Id = user.Id;
            Name = String.Concat(user.FirstName, " ", user.LastName);
            Email = user.Email;
            Photo = user.Photo;
            RoleId = user.Role.Id;
        }
    }
}
