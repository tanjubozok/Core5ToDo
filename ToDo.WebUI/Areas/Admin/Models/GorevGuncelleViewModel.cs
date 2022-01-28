using System.ComponentModel.DataAnnotations;

namespace ToDo.WebUI.Areas.Admin.Models
{
    public class GorevGuncelleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Görev")]
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Ad { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Aciliyet Seçiniz")]
        [Range(0, int.MaxValue, ErrorMessage = "Lütfen bir aciliyet durumu seçiniz")]
        public int AciliyetId { get; set; }
    }
}
