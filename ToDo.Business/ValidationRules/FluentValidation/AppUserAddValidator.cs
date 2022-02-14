using FluentValidation;
using ToDo.DTO.DTOs.AppUserDtos;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserAddValidator : AbstractValidator<AppUserAddDto>
    {
        public AppUserAddValidator()
        {
            RuleFor(I => I.UserName).NotNull();
            RuleFor(I => I.Password).NotNull();
            RuleFor(I => I.ConfirmPassword).NotNull();
            RuleFor(I => I.ConfirmPassword).Equal(I => I.Password);
            RuleFor(I => I.Email).NotNull().EmailAddress();
            RuleFor(I => I.Name).NotNull();
            RuleFor(I => I.SurName).NotNull();
        }
    }
}
