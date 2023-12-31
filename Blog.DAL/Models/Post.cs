﻿
namespace Blog.DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public required string PostContent { get; set; }
        public int UserId { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
