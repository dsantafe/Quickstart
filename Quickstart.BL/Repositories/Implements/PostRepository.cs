using Quickstart.DAL.Data;
using Quickstart.DAL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Quickstart.BL.Repositories.Implements
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly QuickstartContext context;
        public PostRepository()
        {
            this.context = new QuickstartContext();
        }

        public new async Task<IEnumerable<Post>> GetAll()
        {
            return await context.Posts.Include("Blog").ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByBlogId(int id)
        {
            return await context.Posts.Include("Blog").Where(x => x.BlogId == id).ToListAsync();
        }
    }
}
