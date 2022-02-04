using System.ComponentModel.DataAnnotations;

namespace ToDo.WebUI.Areas.Admin.Models
{
    public class AppUserListViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [Display(Name = "Soyad")]
        public string SurName { get; set; }

        [EmailAddress(ErrorMessage = "Eposta doğru formatta değil")]
        [Display(Name = "Eposta")]
        public string Email { get; set; }

        [Display(Name = "Resim")]
        public string Picture { get; set; }
    }
}
