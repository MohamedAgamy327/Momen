using API.DTO.VendorUser;
using Domain.Enums;
using FluentValidation;

namespace API.Validator.VendorUser
{
    public class VendorUserForEditDTOValidator : AbstractValidator<VendorUserForEditDTO>
    {
        public VendorUserForEditDTOValidator()
        {
            RuleFor(x => x.Id)
                 .NotNull()
                 .NotEmpty();

            RuleFor(x => x.Name)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Email)
                   .NotNull()
                   .NotEmpty()
                   .EmailAddress();

            RuleFor(x => x.Phone)
                   .NotNull()
                   .NotEmpty()
                   .Matches(@"^\d*$");

            RuleFor(x => x.Role)
                   .NotNull()
                   .NotEmpty()
                   .IsEnumName(typeof(VendorUserRoleEnum));

        }
    }
}