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

            RuleFor(x => x.Phone)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Address)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.BranchesCount)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.CategoryId)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Description)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Email)
                   .NotNull()
                   .NotEmpty()
                   .EmailAddress();

            RuleFor(x => x.Map)
                   .NotNull()
                   .NotEmpty();

        }
    }
}