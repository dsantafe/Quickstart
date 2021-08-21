using System.ComponentModel.DataAnnotations;

namespace Quickstart.BL.DTOs
{
    public class PostDTO
    {
        [Required(ErrorMessage = "El campo Id es requerido")]
        [Display(Name = "ID")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "El campo Title es requerido")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El campo Content es requerido")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        public int BlogId { get; set; }
        public BlogDTO Blog { get; set; }
    }
}
