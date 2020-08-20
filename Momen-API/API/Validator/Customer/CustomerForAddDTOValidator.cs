using API.DTO.Customer;
using FluentValidation;

namespace API.Validator.Customer
{
    public class CustomerForAddDTOValidator : AbstractValidator<CustomerForAddDTO>
    {
        public CustomerForAddDTOValidator()
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
