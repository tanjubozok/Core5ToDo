using FluentValidation;
using ToDo.DTO.DTOs.AppUserDtos;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserAddValidator : AbstractValidator<AppUserAddDto>
    {
        public AppUserAddValidator()
        {
            RuleFor(I => I.UserName).NotNull().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(I => I.Password).NotNull().WithMessage("Şifre boş geçilemez");
            RuleFor(I => I.ConfirmPassword).NotNull().WithMessage("Şifre Tekrar boş geçilemez");
            RuleFor(I => I.ConfirmPassword).Equal(I => I.Password).WithMessage("Şifreleriniz eşleşmiyor");
            RuleFor(I => I.Email).NotNull().WithMessage("Email boş geçilemez").EmailAddress().WithMessage("Doğru formatta değil.");
            RuleFor(I => I.Name).NotNull();
            RuleFor(I => I.SurName).NotNull();
        }
    }
}
