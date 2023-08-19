using Blog.BLL.Services.IServices;
using Blog.DAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Blog.WebAPI.DTO.Responses;

namespace Blog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("user/authentication")]
        public async Task<IActionResult> Authenticate([FromQuery] string email, string password)
        {
            try
            {
                var response = new ServerResponse()
                {
                    StatusCode = 201,
                    Comment = "Пользователь авторизован.",
                };

                User user = _userService.GetUserByEmail(email);
                response.Id = user.Id;

                if (user.Password != password)
                {
                    response.StatusCode = 401;
                    response.Comment = "Логин или пароль не верный. В доступе отказано.";
                    return Unauthorized(response);
                }

                var claims = new List<Claim>()
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    "BlogCookie",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return StatusCode(response.StatusCode, response);
            }
            catch (InvalidOperationException)
            {
                var response = new ServerResponse()
                {
                    StatusCode = 404,
                    Comment = "Пользователя с такой почтой не существует.",
                };
                return StatusCode(response.StatusCode, response);
            }
        }
    }
}

