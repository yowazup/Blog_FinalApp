using Blog.DAL.Models;

namespace Blog.WebAPI.DTO.Roles
{
    public class RoleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<string> Permissions { get; set; } = new List<string>();

        public RoleResponse(Role role) 
        {
            Id = role.Id;
            Name = role.Name;
            Permissions = role.Permissions;
        }
    }
}
