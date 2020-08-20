using API.DTO.VendorPicture;
using AutoMapper;
using Domain.Entities;

namespace API.MappingProfile
{
    public class VendorPictureMapProfile : Profile
    {
        public VendorPictureMapProfile()
        {
            CreateMap<VendorPicture, VendorPictureForGetDTO>()
                     .ForMember(dest => dest.Path, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.FileName) ? $"Vendors/{src.VendorId}/Pictures/{src.FileName}" : null));

        }
    }
}
