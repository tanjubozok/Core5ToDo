using System.ComponentModel.DataAnnotations;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI.Areas.Member.Models
{
    public class RaporAddViewModel
    {
        [Display(Name = "Tanım")]
        [Required(ErrorMessage = "Tanım alanı boş geçilemez")]
        public string Tanim { get; set; }

        [Display(Name = "Detay")]
        [Required(ErrorMessage = "Detay alanı boş geçilemez")]
        public string Detay { get; set; }

        public int GorevId { get; set; }
        public Gorev Gorev { get; set; }
    }
}
