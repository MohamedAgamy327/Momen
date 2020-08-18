using API.DTO.VendorUser;
using Domain.Enums;
using FluentValidation;

namespace API.Validator.VendorUser
{
    public class VendorUserForAddDTOValidator : AbstractValidator<VendorUserForAddDTO>
    {
        public VendorUserForAddDTOValidator()
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

            RuleFor(x => x.VendorId)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Role)
                   .NotNull()
                   .NotEmpty()
                   .IsEnumName(typeof(VendorUserRoleEnum));

        }
    }
}