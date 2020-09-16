using API.DTO.VendorPicture;
using AutoMapper;
using Domain.Entities;

namespace API.MappingProfile
{
    public class VendorPictureMappingProfile : Profile
    {
        public VendorPictureMappingProfile()
        {
            CreateMap<VendorPicture, VendorPictureForGetDTO>()
                     .ForMember(dest => dest.PicturePath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.PictureName) ? $"Vendors/{src.VendorId}/Pictures/{src.PictureName}" : null));

        }
    }
}
