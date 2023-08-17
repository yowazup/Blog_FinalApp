using Blog.DAL.Models;

namespace Blog.DAL.Repositories.IRepositories
{
    public interface IRoleRepository
    {
        Task UpdateRole(int roleId, List<string> rolePermissions);
        List<Role> GetAllRoles();
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
    }
}
