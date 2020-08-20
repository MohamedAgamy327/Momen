using API.DTO.Vendor;
using FluentValidation;

namespace API.Validator.Contract
{
    public class VendorForLogoDTOValidator : AbstractValidator<VendorForLogoDTO>
    {
        public VendorForLogoDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Logo)
                   .NotNull()
                   .NotEmpty();
        }
    }
}