using API.DTO.Customer;
using FluentValidation;

namespace API.Validator.Customer
{
    public class CustomerForChangePasswordDTOValidator : AbstractValidator<CustomerForChangePasswordDTO>
    {
        public CustomerForChangePasswordDTOValidator()
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
