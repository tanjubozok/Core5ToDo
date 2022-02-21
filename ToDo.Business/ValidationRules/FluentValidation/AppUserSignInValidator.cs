using FluentValidation;
using ToDo.DTO.DTOs.AppUserDtos;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserSignInValidator : AbstractValidator<AppUserSignInDto>
    {
        public AppUserSignInValidator()
        {
            RuleFor(I => I.UserName).NotNull();
            RuleFor(I => I.Password).NotNull();
        }
    }
}
