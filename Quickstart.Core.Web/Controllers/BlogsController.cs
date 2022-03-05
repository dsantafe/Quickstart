using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quickstart.Core.BL.Controls;
using Quickstart.Core.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Quickstart.Core.Web.Controllers
{
    public class BlogsController : Controller
    {
        public static List<BlogDTO> blogs { get; set; } = new List<BlogDTO> {
            new BlogDTO { Id = 1, Name = "Azure" },
            new BlogDTO { Id = 2, Name = "AWS" },
            new BlogDTO { Id = 3, Name = "GCP" },
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult IndexJson()
        {
            return Json(new ResponseDTO
            {
                Code = (int)HttpStatusCode.OK,
                Data = blogs,
                IsSuccessful = true
            });
        }

        [HttpGet]
        public IActionResult GetBlogsSelect()
        {
            var selectControl = blogs.Select(x => new SelectControl
            {
                Id = x.Id,
                Text = x.Name
            }).ToList();

            return Json(new ResponseDTO
            {
                Code = (int)HttpStatusCode.OK,
                Data = JsonConvert.SerializeObject(selectControl),
                IsSuccessful = true
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView(new BlogDTO());
        }

        [HttpPost]
        public IActionResult CreateJson(BlogDTO blogDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new ResponseDTO
                    {
                        IsSuccessful = false,
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                blogs.Add(blogDTO);

                return Json(new ResponseDTO
                {
                    IsSuccessful = true,
                    Code = (int)HttpStatusCode.OK,
                    Message = "Se ha realizado el proceso con exito."
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    IsSuccessful = false,
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blog = blogs.FirstOrDefault(x => x.Id == id);
            return PartialView(blog);
        }

        [HttpPost]
        public IActionResult EditJson(BlogDTO blogDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new ResponseDTO
                    {
                        IsSuccessful = false,
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                blogs.Remove(blogs.FirstOrDefault(x => x.Id == blogDTO.Id));
                blogs.Add(blogDTO);
                blogs = blogs.OrderBy(x => x.Id).ToList();

                return Json(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "Se ha realizado el proceso con exito.",
                    IsSuccessful = true
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    IsSuccessful = false,
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult DeleteJson(int id)
        {
            try
            {
                blogs.Remove(blogs.FirstOrDefault(x => x.Id == id));

                return Json(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "Se ha realizado el proceso con exito.",
                    IsSuccessful = true
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    IsSuccessful = false,
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }
    }
}
