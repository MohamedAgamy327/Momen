using API.DTO.Category;
using FluentValidation;

namespace API.Validator.Category
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