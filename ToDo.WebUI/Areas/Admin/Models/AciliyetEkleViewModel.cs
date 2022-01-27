using System.ComponentModel.DataAnnotations;

namespace ToDo.WebUI.Areas.Admin.Models
{
    public class AciliyetEkleViewModel
    {
        [Display(Name = "Tanım")]
        [Required(ErrorMessage = "Zorunlu alandır.")]
        [MaxLength(100, ErrorMessage = "En fazla 100 karakter girebilirsiniz")]
        public string Tanim { get; set; }
    }
}
