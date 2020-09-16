using API.DTO.Vendor;
using AutoMapper;
using Domain.Entities;

namespace API.MappingProfile
{
    public class VendorMappingProfile : Profile
    {
        public VendorMappingProfile()
        {
            CreateMap<VendorForAddDTO, Vendor>();
            CreateMap<VendorForEditDTO, Vendor>();

            CreateMap<Vendor, VendorForGetDTO>()
                      .ForMember(dest => dest.LogoPath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LogoName) ? $"Vendors/{src.Id}/Logo/{src.LogoName}" : null))
                      .ForMember(dest => dest.PersonalIdPath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.PersonalIdName) ? $"Vendors/{src.Id}/PersonalId/{src.PersonalIdName}" : null))
                      .ForMember(dest => dest.LicensePath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LicenseName) ? $"Vendors/{src.Id}/License/{src.LicenseName}" : null));
        }
    }
}
