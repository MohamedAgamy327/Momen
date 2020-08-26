using API.DTO.Customer;
using FluentValidation;

namespace API.Validator.Customer
{
    public class CustomerForEditDTOValidator : AbstractValidator<CustomerForEditDTO>
    {
        public CustomerForEditDTOValidator()
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

        }
    }
}
