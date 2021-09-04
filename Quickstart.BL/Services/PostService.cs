using Quickstart.BL.DTOs;
using Quickstart.DAL.Data;
using Quickstart.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Quickstart.BL.Services
{
    public class PostService
    {
        /// <summary>
        /// GetBlogsEF
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PostDTO> GetPostsEF(int? id)
        {
            var context = new QuickstartContext();

            //  SELECT * FROM Post JOIN Blog....
            var query = context.Posts.Include("Blog").ToList();
            var posts = query.Select(x => PostToDTO(x)).ToList();

            if (id != null)
                //  SELECT * FROM Post WHERE Id = @id
                posts = posts.Where(x => x.PostId == id.Value).ToList();

            return posts;
        }

        private PostDTO PostToDTO(Post post) => new PostDTO
        {
            PostId = post.PostId,
            Content = post.Content,
            Title = post.Title,
            BlogId = post.BlogId.Value,
            Blog = new BlogDTO
            {
                Name = post.Blog.Name
            }
        };
    }
}
