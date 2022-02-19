using System.ComponentModel.DataAnnotations;

namespace Quickstart.Core.BL.DTOs
{
    public class BlogDTO
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Name es requerido")]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
