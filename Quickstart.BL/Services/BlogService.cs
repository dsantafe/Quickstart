using Quickstart.BL.DTOs;
using Quickstart.BL.Repositories.Implements;
using Quickstart.DAL.Data;
using Quickstart.DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task CreateBlogEF(string name)
        {
            var blogRepository = new BlogRepository();
            var blog = await blogRepository.Insert(new Blog { Name = name });            
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
        public async Task<IEnumerable<Blog>> GetBlogsEF(int? id)
        {
            var blogRepository = new BlogRepository();
            var blogs = await blogRepository.GetAll();   

            if (id != null)
                //  SELECT * FROM Blog WHERE Id = @id
                blogs = blogs.Where(x => x.Id == id.Value).ToList();

            return blogs;
        }

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
        public async Task EditBlogEF(int id,
            string name)
        {
            var blogRepository = new BlogRepository();
            var blog = await blogRepository.GetById(id);
            blog.Name = name;
            await blogRepository.Update(blog);           
        }

        /// <summary>
        /// DeleteBlogEF
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteBlogEF(int id)
        {
            var blogRepository = new BlogRepository();
            await blogRepository.Delete(id);            
        }
    }
}
