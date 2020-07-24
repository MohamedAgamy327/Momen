using API.DTO.Contract;
using FluentValidation;

namespace API.Validator.Contract
{
    public class ContractForEditDTOValidator : AbstractValidator<ContractForEditDTO>
    {
        public ContractForEditDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.Name)
                   .NotNull()
                   .NotEmpty();
        }
    }
}