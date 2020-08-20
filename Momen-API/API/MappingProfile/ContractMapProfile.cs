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
                      .ForMember(dest => dest.PdfPath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.PdfName) ? $"Contracts/{src.Id}/{src.PdfName}" : null));
        }
    }
}
