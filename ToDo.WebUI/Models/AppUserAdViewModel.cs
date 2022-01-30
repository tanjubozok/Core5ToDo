using System.ComponentModel.DataAnnotations;

namespace ToDo.WebUI.Models
{
    public class AppUserAdViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı zorunlu alandır.")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre zorunlu alandır.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        [Required(ErrorMessage = "Şifre Tekrar zorunlu alandır.")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi")]
        [Required(ErrorMessage = "E-Posta zorunlu alandır.")]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ad zorunlu alandır.")]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad zorunlu alandır.")]
        [Display(Name = "Soyad")]
        public string SurName { get; set; }
    }
}
