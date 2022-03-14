using Api.Dtos.Categories;
using Domain.Common.Paging;

namespace Api.Services.Categories
{
    public interface ICategoryService
    {
        PagedList<CategoryDto> GetCategoryPaging(PagedCategoryRequestDto pagedCategoryRequest);
    }
}
