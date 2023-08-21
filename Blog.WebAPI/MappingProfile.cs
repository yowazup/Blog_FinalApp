using AutoMapper;
using Blog.DAL.Models;
using Blog.WebAPI.DTO.Comments;
using Blog.WebAPI.DTO.Posts;
using Blog.WebAPI.DTO.Tags;
using Blog.WebAPI.DTO.Users;

namespace Blog.WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>()
                .ConstructUsing(v => new UserResponse(v));

            CreateMap<Post, PostResponse>()
                .ConstructUsing(v => new PostResponse(v));

            CreateMap<Comment, CommentResponse>()
                .ConstructUsing(v => new CommentResponse(v));

            CreateMap<Tag, TagResponse>()
                .ConstructUsing(v => new TagResponse(v));
        }
    }
}
