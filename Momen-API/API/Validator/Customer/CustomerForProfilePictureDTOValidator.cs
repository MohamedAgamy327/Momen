using API.DTO.Customer;
using FluentValidation;

namespace API.Validator.Customer
{
    public class CustomerForProfilePictureDTOValidator : AbstractValidator<CustomerForProfilePictureDTO>
    {
        public CustomerForProfilePictureDTOValidator()
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
