using Quickstart.DAL.Data;
using System.Data.SqlClient;
using System.Data;
using Quickstart.BL.DTOs;
using System.Collections.Generic;

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
    }
}
