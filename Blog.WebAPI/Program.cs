using Blog.BLL.Services;
using Blog.BLL.Services.IServices;
using Blog.DAL;
using Blog.DAL.Repositories;
using Blog.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем объект класса контекста БД
builder.Services.AddDbContext<BlogContext>(opt =>
{
    opt.UseSqlite(@"Data Source=C:\Coding\Blog_FinalApp\Blog.DAL\DB\blog_db.db;");
    opt.EnableSensitiveDataLogging();
});

// Регистрируем объекты класса сервисов, чтобы существовали на время действия запроса
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IRoleService, RoleService>();

// Регистрируем объекты класса репозиториев, чтобы существовали на время действия запроса
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository > ();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
