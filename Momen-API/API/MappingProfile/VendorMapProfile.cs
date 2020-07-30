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
                      .ForMember(dest => dest.LogoPath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LogoFileName) ? $"Vendor/Logo/{src.Id}/{src.LogoFileName}" : null))
                      .ForMember(dest => dest.PersonalIdPath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.PersonalIdFileName) ? $"Vendor/PersonalId/{src.Id}/{src.LogoFileName}" : null))
                      .ForMember(dest => dest.LicensePath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.LicenseFileName) ? $"Vendor/License/{src.Id}/{src.LogoFileName}" : null));
        }
    }
}
