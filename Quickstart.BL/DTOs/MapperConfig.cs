using AutoMapper;
using Quickstart.DAL.Models;

namespace Quickstart.BL.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Blog, BlogDTO>();
                cfg.CreateMap<BlogDTO, Blog>();

                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<PostDTO, Post>();
            });
        }
    }
}
