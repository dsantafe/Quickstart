using Quickstart.BL.DTOs;
using Quickstart.DAL.Data;
using Quickstart.DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Quickstart.BL.Services
{
    public class BlogService
    {
        /// <summary>
        /// CreateBlog
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        public string CreateBlog(string name)
        {
            var connection = ConnectionDB.GetInstance();
            SqlCommand command = new SqlCommand("CreateBlog", connection);
            command.CommandType = CommandType.StoredProcedure;

            //parameters
            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
            command.Parameters["@Name"].Value = name;

            //output
            SqlParameter message = new SqlParameter("@Message", SqlDbType.VarChar, 100);
            message.Direction = ParameterDirection.Output;
            command.Parameters.Add(message);

            //execute
            command.ExecuteNonQuery();
            return message.Value.ToString();
        }

        /// <summary>
        /// CreateBlogEF
        /// </summary>
        /// <param name="name"></param>
        public void CreateBlogEF(string name)
        {
            //  INSERT INTO Blog(Name) VALUES(@name);
            var context = new QuickstartContext();
            context.Blogs.Add(new Blog { Name = name });
            context.SaveChanges();
        }

        /// <summary>
        /// GetBlog
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public List<BlogDTO> GetBlogs(int id = -1)
        {
            var connection = ConnectionDB.GetInstance();
            SqlCommand command = new SqlCommand("GetBlogs", connection);
            command.CommandType = CommandType.StoredProcedure;

            //parameters
            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters[0].Value = id;

            //execute
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet data = new DataSet();
            adapter.Fill(data);

            var blogs = new List<BlogDTO>();
            foreach (DataRow item in data.Tables[0].Rows)
                blogs.Add(new BlogDTO
                {
                    Id = (int)item["Id"],
                    Name = item["Name"].ToString()
                });

            return blogs;
        }

        /// <summary>
        /// GetBlogsEF
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BlogDTO> GetBlogsEF(int? id)
        {
            var context = new QuickstartContext();

            //var blogs = (from q in context.Blogs
            //             select BlogToDTO(q)).ToList();

            //  SELECT * FROM Blog
            var query = context.Blogs.ToList();
            var blogs = query.Select(x => BlogToDTO(x)).ToList();

            if (id != null)
                //  SELECT * FROM Blog WHERE Id = @id
                blogs = blogs.Where(x => x.Id == id.Value).ToList();

            return blogs;
        }

        private BlogDTO BlogToDTO(Blog blog) => new BlogDTO
        {
            Id = blog.Id,
            Name = blog.Name
        };

        /// <summary>
        /// EditBlog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string EditBlog(int id,
            string name)
        {
            var connection = ConnectionDB.GetInstance();
            SqlCommand command = new SqlCommand("EditBlog", connection);
            command.CommandType = CommandType.StoredProcedure;

            //parameters
            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters["@Id"].Value = id;

            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
            command.Parameters["@Name"].Value = name;

            //output
            SqlParameter message = new SqlParameter("@Message", SqlDbType.VarChar, 100);
            message.Direction = ParameterDirection.Output;
            command.Parameters.Add(message);

            //execute
            command.ExecuteNonQuery();
            return message.Value.ToString();
        }

        /// <summary>
        /// EditBlogEF
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void EditBlogEF(int id,
            string name)
        {
            var context = new QuickstartContext();
            var blog = context.Blogs.Find(id);
            //var blog = context.Blogs.FirstOrDefault(x => x.Id == id);

            //reemplazando
            //  UPDATE Blog SET Name = @name WHERE Id = @id
            blog.Name = name;

            //persistis la información
            context.SaveChanges();
        }

        /// <summary>
        /// DeleteBlogEF
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBlogEF(int id)
        {
            var context = new QuickstartContext();          
            //var blog = context.Blogs.FirstOrDefault(x => x.Id == id);

            //  DELETE FROM Blog WHERE Id = @id
            context.Blogs.Remove(context.Blogs.Find(id));

            //persistis la información
            context.SaveChanges();
        }
    }
}
