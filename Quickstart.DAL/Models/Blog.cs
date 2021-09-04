
namespace Quickstart.DAL.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Blog", Schema = "dbo")]
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
