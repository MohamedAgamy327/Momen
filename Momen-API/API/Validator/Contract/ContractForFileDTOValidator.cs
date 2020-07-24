using API.DTO.Contract;
using FluentValidation;

namespace API.Validator.Contract
{
    public class ContractForFileDTOValidator : AbstractValidator<ContractForFileDTO>
    {
        public ContractForFileDTOValidator()
        {
            RuleFor(x => x.Id)
                   .NotNull()
                   .NotEmpty();

            RuleFor(x => x.File)
                   .NotNull()
                   .NotEmpty();
        }
    }
}