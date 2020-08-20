using API.DTO.VendorPicture;
using FluentValidation;

namespace API.Validator.VendorPicture
{
    public class VendorPictureForAddDTOValidator : AbstractValidator<VendorPictureForAddDTO>
    {
        public VendorPictureForAddDTOValidator()
        {
            RuleFor(x => x.Pictures)
                   .Cascade(CascadeMode.StopOnFirstFailure)
                   .NotNull()
                   .NotEmpty()
                   .Must(x => x.Count > 0);

            RuleFor(x => x.VendorId)
                   .NotNull()
                   .NotEmpty();
        }
    }
}

