using FluentValidation;
using ToDo.DTO.DTOs.RaporDtos;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class RaporAddValidator : AbstractValidator<RaporAddDto>
    {
        public RaporAddValidator()
        {
            RuleFor(I => I.Tanim).NotNull();
            RuleFor(I => I.Detay).NotNull();
        }
    }
}
