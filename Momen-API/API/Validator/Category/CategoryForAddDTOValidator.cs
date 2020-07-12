using API.DTO.Category;
using FluentValidation;

namespace API.Validator.Knowing
{
    public class CategoryForAddDTOValidator : AbstractValidator<CategoryForAddDTO>
    {
        public CategoryForAddDTOValidator()
        {
            RuleFor(x => x.Name)
                   .NotNull()
                   .NotEmpty();
        }
    }
}