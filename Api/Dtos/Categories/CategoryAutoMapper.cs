using Api.Helper;
using AutoMapper;
using Domain.Common.Paging;
using Domain.Entities.Catalog;

namespace Api.Dtos.Categories
{
    public class CategoryAutoMapper : Profile
    {
        public CategoryAutoMapper()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto,Category>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category,CategoryDto>();
            CreateMap<PagedList<Category>, PagedList<CategoryDto>>()
                .ConvertUsing<PagedListTypeConverter<Category, CategoryDto>>();
        }
    }
}
