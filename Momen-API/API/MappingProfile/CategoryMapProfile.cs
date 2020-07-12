using API.DTO.Category;
using AutoMapper;
using Domain.Entities;

namespace API.MappingProfile
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryForAddDTO, Category>();
            CreateMap<CategoryForEditDTO, Category>();

            CreateMap<Category, CategoryForGetDTO>();
        }
    }
}
