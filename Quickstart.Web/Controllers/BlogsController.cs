using AutoMapper;
using Quickstart.BL.DTOs;
using Quickstart.BL.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Quickstart.Web.Controllers
{
    public class BlogsController : Controller
    {
        private IMapper mapper;
        private readonly BlogService blogService = new BlogService();
        private readonly PostService postService = new PostService();

        public BlogsController()
        {
            this.mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        // GET: Blogs
        public async Task<ActionResult> Index()
        {
            var blogs = await blogService.GetBlogsEF(null);
            var blogsDTO = blogs.Select(x => mapper.Map<BlogDTO>(x));

            ViewBag.Data = "Data prueba";
            ViewBag.Message = "Mensaje prueba";

            //ViewData["Data"] = "Data prueba";
            //ViewData["Message"] = "Mensaje prueba";

            //Session["Data"] = "Data prueba";
            //Session.RemoveAll();

            return View(blogsDTO);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var blog = (await blogService.GetBlogsEF(id)).FirstOrDefault();
            var blogDTO = mapper.Map<BlogDTO>(blog);

            return View(blogDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BlogDTO blogDTO)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            try
            {
                await blogService.EditBlogEF(blogDTO.Id, blogDTO.Name);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(ModelState);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id, bool flag = false)
        {
            var blog = (await blogService.GetBlogsEF(id)).FirstOrDefault();
            var blogDTO = mapper.Map<BlogDTO>(blog);

            if (flag)
                ModelState.AddModelError(string.Empty, $"El Blog {blogDTO.Name} tiene dependencias.");

            return View(blogDTO);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var posts = await postService.GetPostsByBlogId(id);
            if (!posts.Any())
                await blogService.DeleteBlogEF(id);
            else
                return RedirectToAction("Delete", new { id, flag = true });

            return RedirectToAction("Index");
        }
    }
}