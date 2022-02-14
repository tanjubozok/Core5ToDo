using FluentValidation;
using ToDo.DTO.DTOs.GorevDtos;

namespace ToDo.Business.ValidationRules.FluentValidation
{
    public class GorevAddValidator : AbstractValidator<GorevAddDto>
    {
        public GorevAddValidator()
        {
            RuleFor(I => I.Ad).NotNull();
            RuleFor(I => I.AciliyetId).ExclusiveBetween(0, int.MaxValue);
        }
    }
}
