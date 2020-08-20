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
                 .ForMember(dest => dest.ProfilePicturePath, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ProfilePictureName) ? $"Customers/{src.Id}/{src.ProfilePictureName}" : null));
        }
    }
}
