using API.DTO.VendorUser;
using FluentValidation;

namespace API.Validator.VendorUser
{
    public class VendorUserForLoginDTOValidator : AbstractValidator<VendorUserForLoginDTO>
    {
        public VendorUserForLoginDTOValidator()
        {
            RuleFor(x => x.Email)
                   .NotNull()
                   .NotEmpty()
                   .EmailAddress();

            RuleFor(x => x.Password)
                   .NotNull()
                   .NotEmpty();
        }

    }
}
