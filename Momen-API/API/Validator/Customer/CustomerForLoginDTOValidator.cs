using API.DTO.Customer;
using FluentValidation;

namespace API.Validator.Customer
{
    public class CustomerForLoginDTOValidator : AbstractValidator<CustomerForLoginDTO>
    {
        public CustomerForLoginDTOValidator()
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
