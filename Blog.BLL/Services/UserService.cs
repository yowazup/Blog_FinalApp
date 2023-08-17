using Blog.BLL.Services.IServices;
using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;

namespace Blog.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleService _roleService;

        public UserService(IUserRepository userRepo, IRoleService roleService)
        {
            _userRepo = userRepo;
            _roleService = roleService; 
        }

        public async Task<User> AddUser(string firstName, string lastName, string password, string email, string photo)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                Email = email,
                Photo = photo,
                Role = _roleService.GetRoleById(1)
            };
            await _userRepo.AddUser(user);

            return user;
        }

        public async Task<User> UpdateUser(int userId, string firstName, string lastName, string password, string email, string photo, int roleId)
        {
            var role = _roleService.GetRoleById(roleId);
            await _userRepo.UpdateUser(userId, firstName, lastName, password, email, photo, role);
            return _userRepo.GetUserById(userId);
        }

        public async Task<User> DeleteUser(User user)
        {
            var deletedUser = user;
            await _userRepo.DeleteUser(user.Id);
            return deletedUser;
        }

        public List<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepo.GetUserById(id);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepo.GetUserByEmail(email);
        }
    }
}
