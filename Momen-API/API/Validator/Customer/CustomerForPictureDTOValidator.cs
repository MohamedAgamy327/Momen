using API.DTO.Customer;
using FluentValidation;

namespace API.Validator.Customer
{
    public class CustomerForPictureDTOValidator : AbstractValidator<CustomerForPictureDTO>
    {
        public CustomerForPictureDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Picture)
                   .NotNull()
                   .NotEmpty();
        }
    }
}
