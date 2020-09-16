using API.DTO.VendorReject;
using AutoMapper;
using Domain.Entities;

namespace API.MappingProfile
{
    public class VendorRejectMappingProfile : Profile
    {
        public VendorRejectMappingProfile()
        {
            CreateMap<VendorRejectForAddDTO, VendorReject>();

            CreateMap<VendorReject, VendorRejectForGetDTO>();
        }
    }
}

