using Blog.DAL.Models;

namespace Blog.BLL.Services.IServices
{
    public interface IRoleService
    {
        Task<Role> UpdateRole(Role role, List<string> rolePermissions);
        List<Role> GetAllRoles();
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
    }
}
