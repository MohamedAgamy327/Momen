using API.DTO.Vendor;
using FluentValidation;

namespace API.Validator.Vendor
{
    public class VendorForAcceptPendingDTOValidator : AbstractValidator<VendorForAcceptPendingDTO>
    {
        public VendorForAcceptPendingDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.ContractId)
                   .NotNull()
                   .NotEmpty();

        }
    }
}
