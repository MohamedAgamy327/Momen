using API.DTO.VendorReject;
using FluentValidation;

namespace API.Validator.VendorPicture
{
    public class VendorRejectForAddDTOValidator : AbstractValidator<VendorRejectForAddDTO>
    {
        public VendorRejectForAddDTOValidator()
        {
            RuleFor(x => x.Reason)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.VendorId)
                   .NotNull()
                   .NotEmpty();
        }
    }
}


