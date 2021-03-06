﻿using API.DTO.Vendor;
using FluentValidation;

namespace API.Validator.Vendor
{
    public class VendorForEditDTOValidator : AbstractValidator<VendorForEditDTO>
    {
        public VendorForEditDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Name)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.BranchesCount)
                   .NotNull()
                   .NotEmpty()
                   .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Description)
                   .NotNull()
                   .NotEmpty();


        }
    }
}