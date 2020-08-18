using API.DTO.Vendor;
using FluentValidation;

namespace API.Validator.Vendor
{
    public class UserForVendorAddDTOValidator : AbstractValidator<UserForVendorAddDTO>
    {
        public UserForVendorAddDTOValidator()
        {
            RuleFor(x => x.Name)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Email)
                   .NotNull()
                   .NotEmpty()
                   .EmailAddress();

            RuleFor(x => x.Phone)
                   .NotNull()
                   .NotEmpty();
        }
    }
}
