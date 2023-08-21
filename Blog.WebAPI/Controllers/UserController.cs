using AutoMapper;
using Blog.BLL.Services.IServices;
using Blog.WebAPI.DTO.Comments;
using Blog.WebAPI.DTO.Posts;
using Blog.WebAPI.DTO.Responses;
using Blog.WebAPI.DTO.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
            return StatusCode(201, _mapper.Map<UserResponse>(newUser));
        }

        [Authorize(Roles = "Администратор")]
        [HttpDelete]
        [Route("users/:userId")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var deletedUser = await _userService.DeleteUser(_userService.GetUserById(userId));
            return StatusCode(201, _mapper.Map<UserResponse>(deletedUser));
        }

        [Authorize]
        [HttpPatch]
        [Route("users/me")]
        public async Task<IActionResult> UpdateUser(UserMeUpdateRequest updateRequest)
        {
            var userId = _userService.GetUserByEmail(Request.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType)!.Value).Id;

            var updatedUser = await _userService.UpdateUser(
                userId,
                updateRequest.FirstName,
                updateRequest.LastName,
                updateRequest.Password,
                updateRequest.Email,
                updateRequest.Photo,
                _userService.GetUserById(userId).Role.Id);
            return StatusCode(201, _mapper.Map<UserResponse>(updatedUser));
        }

        [Authorize(Roles = "Администратор")]
        [HttpPatch]
        [Route("users/:userId")]
        public async Task<IActionResult> UpdateUserRole(int userId, UserAdminUpdateRequest updateRequest)
        {
            var updatedUser = await _userService.UpdateUser(
                userId,
                updateRequest.FirstName,
                updateRequest.LastName,
                updateRequest.Password,
                updateRequest.Email,
                updateRequest.Photo,
                updateRequest.RoleId);
            return StatusCode(201, _mapper.Map<UserResponse>(updatedUser));
        }

        [Authorize]
        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers()
        {
            var allUsers = new List<UserResponse>();
            foreach (var u in _userService.GetAllUsers())
                allUsers.Add(_mapper.Map<UserResponse>(u));
            return StatusCode(201, allUsers);
        }

        [Authorize]
        [HttpGet]
        [Route("users/:userId/posts")]
        public IActionResult GetPostsForUser(int userId)
        {
            var posts = _userService.GetUserById(userId).Posts;
            return StatusCode(201, _mapper.Map<PostResponse>(posts));
        }

        [Authorize]
        [HttpGet]
        [Route("users/:userId/comments")]
        public IActionResult GetCommentsForUser(int userId)
        {
            var comments = _userService.GetUserById(userId).Comments;
            return StatusCode(201, _mapper.Map<CommentResponse>(comments));
        }

        [Authorize]
        [HttpGet]
        [Route("users/:userEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var foundUser = _userService.GetUserByEmail(email);
                return StatusCode(201, _mapper.Map<UserResponse>(foundUser));
            }
            catch (InvalidOperationException)
            {
                var response = new StatusCodeResponse()
                {
                    StatusCode = 404,
                    Comment = "Пользователя с такой почтой не существует.",
                };
                return StatusCode(response.StatusCode, response);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("users/:userId")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var foundUser = _userService.GetUserById(userId);
                return StatusCode(201, _mapper.Map<UserResponse>(foundUser));
            }
            catch (InvalidOperationException)
            {
                var response = new StatusCodeResponse()
                {
                    StatusCode = 404,
                    Comment = "Пользователя с таким идентификатором не существует.",
                };
                return StatusCode(response.StatusCode, response);
            }
        }
    }
}
