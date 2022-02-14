using FluentValidation;
using ToDo.DTO.DTOs.RaporDtos;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class RaporUpdateValidator : AbstractValidator<RaporUpdateDto>
    {
        public RaporUpdateValidator()
        {
            RuleFor(I => I.Tanim).NotNull();
            RuleFor(I => I.Detay).NotNull();
        }
    }
}
