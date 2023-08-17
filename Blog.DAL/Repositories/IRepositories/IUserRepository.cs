using Blog.DAL.Models;

namespace Blog.DAL.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task UpdateUser(int userId, string firstName, string lastName, string password, string email, string photo, Role role);
        Task DeleteUser(int userId);
        List<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByEmail(string userEmail);
    }
}
