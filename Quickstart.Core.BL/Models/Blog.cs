using System;
using System.Collections.Generic;

#nullable disable

namespace Quickstart.Core.BL.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
