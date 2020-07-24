using API.DTO.Contract;
using AutoMapper;
using Domain.Entities;

namespace API.MappingProfile
{
    public class ContractMappingProfile : Profile
    {
        public ContractMappingProfile()
        {
            CreateMap<ContractForAddDTO, Contract>();
            CreateMap<ContractForEditDTO, Contract>();

            CreateMap<Contract, ContractForGetDTO>()
                      .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.FileName) ? $"Contract/{src.Id}/{src.FileName}" : null));
        }
    }
}
