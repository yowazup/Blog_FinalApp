using Blog.DAL.Models;
using Blog.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include(x => x.Posts).Include(y => y.Comments).ToList();
        }

        public User GetUserById(int userId)
        {
            return GetAllUsers().Single(x => x.Id == userId);
        }

        public User GetUserByEmail(string userEmail)
        {
            return GetAllUsers().Single(x => x.Email == userEmail);
        }

        public async Task DeleteUser(int userId)
        {
            _context.Remove(GetUserById(userId));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(int userId, string firstName, string lastName, string password, string email, string photo, Role role)
        {
            var user = GetUserById(userId);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Password = password;
            user.Email = email;
            user.Photo = photo;
            user.Role = role;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
