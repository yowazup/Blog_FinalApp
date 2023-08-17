
using Blog.DAL.Models;

namespace Blog.BLL.Services.IServices
{
    public interface IUserService
    {
        Task<User> AddUser(string firstName, string lastName, string password, string email, string photo);
        Task<User> UpdateUser(int userId, string firstName, string lastName, string password, string email, string photo, int roleId);
        Task<User> DeleteUser(User user);
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
    }
}
