using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ToDo.WebUI.Areas.Admin.Models
{
    public class GorevEkleViewModel
    {
        [Display(Name = "Görev")]
        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Ad { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Aciliyet Seçiniz")]
        [Range(0, int.MaxValue, ErrorMessage = "Lütfen bir aciliyet durumu seçiniz")]
        public int AciliyetId { get; set; }


        public SelectList Aciliyetler { get; set; }
    }
}
