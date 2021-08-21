using Quickstart.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace Quickstart.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            var param = ConfigurationManager.AppSettings["param"];
            var blogs = new List<BlogDTO>();

            Console.Write("Ingrese la cantidad de Blogs a crear: ");
            var cantidadBlogs = int.Parse(Console.ReadLine());

            for (int i = 0; i < cantidadBlogs; i++)
            {
                Console.Write("Ingrese el nombre para un nuevo Blog: ");
                blogs.Add(new BlogDTO { Id = i, Name = Console.ReadLine() });
            }

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

            Console.ReadKey();
        }
    }
}
