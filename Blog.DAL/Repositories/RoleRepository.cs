using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BlogContext _context;

        public RoleRepository(BlogContext context)
        {
            _context = context;
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.Include(x => x.Users).ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return GetAllRoles().Single(x => x.Id == roleId);
        }

        public async Task UpdateRole(int roleId, List<string> rolePermissions)
        {
            var role = GetRoleById(roleId);
            role.Permissions = rolePermissions;

            _context.Update(role);
            await _context.SaveChangesAsync();
        }

        public Role GetRoleByName(string roleName)
        {
            return GetAllRoles().Single(x => x.Name == roleName);
        }
    }
}
