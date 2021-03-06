﻿using API.DTO.Category;
using FluentValidation;

namespace API.Validator.Category
{
    public class CategoryForEditDTOValidator : AbstractValidator<CategoryForEditDTO>
    {
        public CategoryForEditDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Name)
                   .NotNull()
                   .NotEmpty();
        }
    }
}