using API.DTO.VendorUser;
using FluentValidation;

namespace API.Validator.VendorUser
{
    public class VendorUserForChangePasswordDTOValidator : AbstractValidator<VendorUserForChangePasswordDTO>
    {
        public VendorUserForChangePasswordDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Password)
                   .NotNull()
                   .NotEmpty()
                   .MinimumLength(8)
                   .Equal(x => x.ConfirmPassword)
                   .When(x => !string.IsNullOrWhiteSpace(x.Password));

            RuleFor(x => x.ConfirmPassword)
                   .NotNull()
                   .NotEmpty()
                   .MinimumLength(8);
        }

    }
}
