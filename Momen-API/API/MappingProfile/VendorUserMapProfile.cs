using API.DTO.VendorUser;
using AutoMapper;
using Domain.Entities;

namespace API.MappingProfile
{
    public class VendorUserMappingProfile : Profile
    {
        public VendorUserMappingProfile()
        {
            CreateMap<VendorUserForAddDTO, VendorUser>();
            CreateMap<VendorUserForEditDTO, VendorUser>();

            CreateMap<VendorUser, VendorUserForGetDTO>();
        }
    }
}
