using Quickstart.BL.Services;
using System;
using System.Configuration;
using System.Linq;

namespace Quickstart.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            BlogService blogService = new BlogService();            

            Console.Write("Ingrese la cantidad de Blogs a crear: ");
            var cantidadBlogs = int.Parse(Console.ReadLine());

            for (int i = 0; i < cantidadBlogs; i++)
            {
                Console.Write("Ingrese el nombre para un nuevo Blog: ");
                blogService.CreateBlogEF(Console.ReadLine());
            }

            var blogs = blogService.GetBlogsEF(null);

            /*
               SELECT fields
               FROM table t
               WHERE condition
            */
            var query = (from b in blogs                         
                         where b.Name.Length < 50
                         select b).ToList();

            var query2 = blogs.Where(x => x.Name.Length < 50)                              
                              .Select(x => x)
                              .ToList();

            Console.WriteLine("Todos los Blogs guardados");
            foreach (var item in query)
                Console.WriteLine($"Id: {item.Id} Nombre: {item.Name}");

            Console.WriteLine("Ingresa el Id que vas a editar");
            var id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingresa el nuevo nombre");
            var name = Console.ReadLine();
            blogService.EditBlogEF(id, name);

            var blog = blogService.GetBlogsEF(id).FirstOrDefault();
            Console.WriteLine($"Id: {blog.Id} Nombre: {blog.Name}");

            Console.ReadKey();
        }

        #region SQL
        private static void SQL()
        {
            BlogService blogService = new BlogService();
            var param = ConfigurationManager.AppSettings["param"];

            Console.Write("Ingrese la cantidad de Blogs a crear: ");
            var cantidadBlogs = int.Parse(Console.ReadLine());

            for (int i = 0; i < cantidadBlogs; i++)
            {
                Console.Write("Ingrese el nombre para un nuevo Blog: ");
                var message = blogService.CreateBlog(Console.ReadLine());
            }

            var blogs = blogService.GetBlogs();

            /*
               SELECT fields
               FROM table t
               WHERE condition
            */
            var query = (from b in blogs
                         where b.Name.Length < 50
                         select b).ToList();

            if (query.Count() > 0) { }
            if (query.Any()) { }

            Console.WriteLine("Todos los Blogs guardados");
            foreach (var item in query)
                Console.WriteLine($"Id: {item.Id} Nombre: {item.Name}");

            //for (int i = 0; i < query.Count(); i++)
            //    Console.WriteLine(query[i].Name);

            Console.WriteLine("Ingresa el Id que vas a editar");
            var id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingresa el nuevo nombre");
            var name = Console.ReadLine();
            blogService.EditBlog(id, name);

            var blog = blogService.GetBlogs(id).FirstOrDefault();
            Console.WriteLine($"Id: {blog.Id} Nombre: {blog.Name}");
        }
        #endregion
    }
}
