using API.DTO.Vendor;
using FluentValidation;

namespace API.Validator.Contract
{
    public class VendorForPersonalIdDTOValidator : AbstractValidator<VendorForPersonalIdDTO>
    {
        public VendorForPersonalIdDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.File)
                   .NotNull()
                   .NotEmpty();
        }
    }
}