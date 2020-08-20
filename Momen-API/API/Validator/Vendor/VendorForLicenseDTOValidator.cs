using API.DTO.Vendor;
using FluentValidation;

namespace API.Validator.Contract
{
    public class VendorForLicenseDTOValidator : AbstractValidator<VendorForLicenseDTO>
    {
        public VendorForLicenseDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.License)
                   .NotNull()
                   .NotEmpty();
        }
    }
}