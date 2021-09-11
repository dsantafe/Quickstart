using Quickstart.BL.DTOs;
using Quickstart.BL.Repositories.Implements;
using Quickstart.DAL.Data;
using Quickstart.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quickstart.BL.Services
{
    public class PostService
    {
        /// <summary>
        /// GetBlogsEF
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<PostDTO>> GetPostsEF(int? id)
        {
            var postRepository = new PostRepository();

            //  SELECT * FROM Post JOIN Blog....
            var query = await postRepository.GetAll();
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

        public async Task<List<PostDTO>> GetPostsByBlogId(int blogId)
        {
            var postRepository = new PostRepository();

            //  SELECT * FROM Post JOIN Blog.... WHERE BlogId = @blogId
            var query = await postRepository.GetByBlogId(blogId);
            var posts = query.Select(x => PostToDTO(x)).ToList();            

            return posts;
        }
    }
}
