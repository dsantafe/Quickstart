using Quickstart.Core.BL.Models;

namespace Quickstart.Core.BL.Repositories.Implements
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(QuickstartContext context) : base(context)
        {

        }
    }
}
