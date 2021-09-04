using Quickstart.DAL.Models;
using System.Configuration;
using System.Data.Entity;

namespace Quickstart.DAL.Data
{
    public class QuickstartContext : DbContext
    {
        public QuickstartContext() : base(ConfigurationManager.ConnectionStrings["Quickstart"].ToString())
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
