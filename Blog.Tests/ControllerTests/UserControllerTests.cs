using AutoMapper;
using Blog.BLL.Services;
using Blog.DAL;
using Blog.DAL.Models;
using Blog.DAL.Repositories;
using Blog.WebAPI;
using Blog.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;

namespace Blog.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public async Task UserCanBeDeletedById()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<BlogContext>().UseSqlite(connection).Options;

            using (var context = new BlogContext(options))
            {
                context.Database.EnsureCreated();
            }

            //создаем в БД пользователя
            using (var context = new BlogContext(options))
            {
                var user1 = new User
                {
                    FirstName = "Nikita",
                    LastName = "Zakharov",
                    Email = "nikita@gmail.com",
                    Password = "12345",
                    Role = new Role { Name = "Пользователь" }
                };
                context.Add(user1);
                context.SaveChanges();
            }

            using (var context = new BlogContext(options))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                var mapper = config.CreateMapper();

                var roleRepo = new RoleRepository(context);
                var roleService = new RoleService(roleRepo);

                var userRepo = new UserRepository(context);
                var userService = new UserService(userRepo, roleService);

                var userController = new UserController(userService, mapper);

                var result = await userController.DeleteUser(1) as ObjectResult;
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(201));
            }
        }
    }
}
