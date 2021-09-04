namespace Quickstart.DAL.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Post", Schema = "dbo")]
    public partial class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [ForeignKey("Blog")]
        public Nullable<int> BlogId { get; set; }    
        public virtual Blog Blog { get; set; }
    }
}
