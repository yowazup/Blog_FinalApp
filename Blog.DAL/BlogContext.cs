using Blog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Reflection.Metadata;

namespace Blog.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }

        public BlogContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Coding\Blog_FinalApp\Blog.DAL\DB\blog_db.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
             .HasData(
                new Role { Id = 1, Name = "Пользователь" },
                new Role { Id = 2, Name = "Модератор" },
                new Role { Id = 3, Name = "Администратор" }
                );

            modelBuilder.Entity<Role>()
            .Property(role => role.Permissions)
            .HasColumnType("text")
            .HasConversion(
                strings => string.Join(",", strings),
                s => s.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
