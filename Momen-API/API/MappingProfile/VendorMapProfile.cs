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
                      .ForMember(dest => dest.LogoPath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LogoFileName) ? $"Vendors/{src.Id}/Logo/{src.LogoFileName}" : null))
                      .ForMember(dest => dest.PersonalIdPath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.PersonalIdFileName) ? $"Vendors/{src.Id}/PersonalId/{src.LogoFileName}" : null))
                      .ForMember(dest => dest.LicensePath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LicenseFileName) ? $"Vendors/{src.Id}/License/{src.LogoFileName}" : null));
        }
    }
}
