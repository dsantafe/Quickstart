using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quickstart.Core.BL.DTOs;
using Quickstart.Core.BL.Models;
using Quickstart.Core.BL.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Quickstart.Core.API.Controllers
{
    /*
     * api/Blogs/GetAll (GET)
     * api/Blogs/GetById/1 (GET)
     * api/Blogs (POST)
     * api/Blogs/1 (PUT)
     * api/Blogs/1 (DELETE)
     */

    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository blogRepository;
        private readonly IMapper mapper;

        public BlogsController(IBlogRepository blogRepository,
            IMapper mapper)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var blogs = (await blogRepository.GetAll()).Select(x => mapper.Map<BlogDTO>(x)).ToList();
            return Ok(new ResponseDTO
            {
                Code = (int)HttpStatusCode.OK,
                Data = blogs
            });
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = mapper.Map<BlogDTO>(await blogRepository.GetById(id));
            return Ok(new ResponseDTO
            {
                Code = (int)HttpStatusCode.OK,
                Data = blog
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogDTO blogDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var blog = mapper.Map<Blog>(blogDTO);
                blog = await blogRepository.Insert(blog);
                blogDTO.Id = blog.Id;

                return Ok(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.OK,
                    Data = blogDTO
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, BlogDTO blogDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var blog = await blogRepository.GetById(id);
                if (blog == null)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = "Not Found"
                    });

                blog = mapper.Map<Blog>(blogDTO);
                await blogRepository.Update(blog);

                return Ok(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.OK,
                    Data = blogDTO
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var blog = await blogRepository.GetById(id);
                if (blog == null)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = "Not Found"
                    });

                await blogRepository.Delete(id);

                return Ok(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "Se ha realizado el proceso con exito."
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }
    }
}
