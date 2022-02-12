using Microsoft.AspNetCore.Mvc;
using Quickstart.Core.BL.DTOs;
using System.Collections.Generic;

namespace Quickstart.Core.Web.Controllers
{
    public class BlogsController : Controller
    {
        public static List<BlogDTO> Blogs { get; set; } = new List<BlogDTO> {
            new BlogDTO { Id = 1, Name = "Azure" },
            new BlogDTO { Id = 2, Name = "AWS" },
            new BlogDTO { Id = 3, Name = "GCP" },
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexJson()
        {
            return Json(Blogs);
        }
    }
}
