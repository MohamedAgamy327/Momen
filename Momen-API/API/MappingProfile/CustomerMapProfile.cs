using API.DTO.Customer;
using AutoMapper;
using Domain.Entities;

namespace API.MappingProfile
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<CustomerForAddDTO, Customer>();
            CreateMap<CustomerForEditDTO, Customer>();

            CreateMap<Customer, CustomerForGetDTO>()
                 .ForMember(dest => dest.PicturePath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.PictureName) ? $"Customers/{src.Id}/{src.PictureName}" : null));
        }
    }
}
