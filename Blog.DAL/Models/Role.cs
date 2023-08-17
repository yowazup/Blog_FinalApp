
namespace Blog.DAL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Пользователь";
        public List<string> Permissions { get; set; } = new List<string>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
