using System.ComponentModel.DataAnnotations;

namespace Quickstart.BL.DTOs
{
    public class BlogDTO
    {
        [Required(ErrorMessage = "El campo Id es requerido")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Name es requerido")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
