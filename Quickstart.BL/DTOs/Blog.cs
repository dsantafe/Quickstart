using System.ComponentModel.DataAnnotations;

namespace Quickstart.BL.DTOs
{
    public class Blog
    {
        [Required(ErrorMessage = "El campo Id es requerido")]
        [Display(Name = "ID")]
        public int Id { get; set; }
    }
}
