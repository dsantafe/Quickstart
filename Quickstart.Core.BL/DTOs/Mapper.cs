using AutoMapper;
using Quickstart.Core.BL.Models;

namespace Quickstart.Core.BL.DTOs
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();
        }
    }
}
