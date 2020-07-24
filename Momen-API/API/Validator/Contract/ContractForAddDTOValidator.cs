using API.DTO.Contract;
using FluentValidation;

namespace API.Validator.Contract
{
    public class ContractForAddDTOValidator : AbstractValidator<ContractForAddDTO>
    {
        public ContractForAddDTOValidator()
        {
            RuleFor(x => x.Name)
                   .NotNull()
                   .NotEmpty();
        }
    }
}