using API.DTO.Vendor;
using FluentValidation;

namespace API.Validator.Vendor
{
    public class VendorForAddDTOValidator : AbstractValidator<VendorForAddDTO>
    {
        public VendorForAddDTOValidator()
        {
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

            RuleFor(x => x.VendorUser)
                   .NotNull()
                   .NotEmpty()
                   .SetValidator(new UserForVendorAddDTOValidator());

        }
    }
}