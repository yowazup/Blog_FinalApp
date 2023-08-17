using Blog.BLL.Services.IServices;
using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;


namespace Blog.BLL.Services
{
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepo;

        public RoleService(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<Role> UpdateRole(Role role, List<string> rolePersmissions)
        {
            var updatedRole = new Role { Permissions = rolePersmissions };
            await _roleRepo.UpdateRole(updatedRole.Id, updatedRole.Permissions);
            return updatedRole;
        }

        public List<Role> GetAllRoles()
        {
            return _roleRepo.GetAllRoles();
        }

        public Role GetRoleById(int roleId)
        {
            return _roleRepo.GetRoleById(roleId);
        }

        public Role GetRoleByName(string roleName)
        {
            return _roleRepo.GetRoleByName(roleName);
        }
    }
}

