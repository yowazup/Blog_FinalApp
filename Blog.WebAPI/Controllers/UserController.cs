using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Users;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> AddUser(UserAddRequest addRequest)
        {
            var newUser = await _userService.AddUser(
                addRequest.FirstName, 
                addRequest.LastName, 
                addRequest.Password, 
                addRequest.Email, 
                addRequest.Photo);
            return StatusCode(201, $"Пользователь добавлен. Идентификатор: {newUser.Id}");
        }

        [HttpDelete]
        [Route("users/:userId")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var deletedUser = await _userService.DeleteUser(_userService.GetUserById(userId));
            return StatusCode(201, $"Пользователь удален. Идентификатор: {deletedUser.Id}");
        }

        [HttpPatch]
        [Route("users/:userId")]
        public async Task<IActionResult> UpdateUser(int userId, UserUpdateRequest updateRequest)
        {
            var updatedUser = await _userService.UpdateUser(
                userId, 
                updateRequest.FirstName, 
                updateRequest.LastName, 
                updateRequest.Password, 
                updateRequest.Email, 
                updateRequest.Photo, 
                updateRequest.RoleId);
            return StatusCode(201, $"Пользователь обновлен. Идентификатор: {updatedUser.Id}");
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers()
        {
            var allUsers = _userService.GetAllUsers();
            return StatusCode(201, $"Найдено пользователей: {allUsers.Count}");
        }

        [HttpGet]
        [Route("users/:userId/posts")]
        public IActionResult GetPostsForUser(int userId)
        {
            var posts = _userService.GetUserById(userId).Posts;
            return StatusCode(201, $"Найдено постов: {posts.Count}");
        }

        [HttpGet]
        [Route("users/:userId/comments")]
        public IActionResult GetCommentsForUser(int userId)
        {
            var comments = _userService.GetUserById(userId).Comments;
            return StatusCode(201, $"Найдено комментариев: {comments.Count}");
        }

        [HttpGet]
        [Route("users/:userEmail")]
        public IActionResult GetUserByEmail([FromQuery] string email)
        {
            var foundUser = _userService.GetUserByEmail(email);

            if (foundUser.Email == email)
            {
                return StatusCode(201, $"Пользователь с почтой {email} найден.");
            }
            else
            {
                return StatusCode(404, $"Пользователь с почтой {email} не существует.");
            }
        }

        [HttpGet]
        [Route("users/:userId")]
        public IActionResult GetUserById(int userId)
        {
            var foundUser = _userService.GetUserById(userId);

            if (foundUser.Id == userId)
            {
                return StatusCode(201, $"Пользователь с идентификатором {userId} найден.");
            }
            else
            {
                return StatusCode(404, $"Пользователь с идентификатором {userId} не найден.");
            }
        }
    }
}
