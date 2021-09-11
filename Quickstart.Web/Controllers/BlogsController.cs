using Quickstart.BL.DTOs;
using Quickstart.BL.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Quickstart.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BlogService blogService = new BlogService();

        // GET: Blogs
        public async Task<ActionResult> Index()
        {
            var blogs = await blogService.GetBlogsEF(null);
            return View(blogs);
        }

        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(BlogDTO blogDTO)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            try
            {
                await blogService.CreateBlogEF(blogDTO.Name);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(ModelState);
        }
    }
}