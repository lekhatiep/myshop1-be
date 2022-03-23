using Api.Dtos.Categories;
using Api.Dtos.Products;
using Domain.Common.Paging;
using Domain.Entities.Catalog;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Categories
{
    public interface ICategoryService
    {
        PagedList<CategoryDto> GetCategoryPaging(PagedCategoryRequestDto pagedCategoryRequest);

        IQueryable<Product> GetAllProductByCategoryId(ProductPagedRequestDto pagedRequestDto, int categoryId);

        Task AddRandomCategoryToProduct();
    }
}
