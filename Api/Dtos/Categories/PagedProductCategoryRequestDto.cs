using Domain.Common.Paging;

namespace Api.Dtos.Categories
{
    public class PagedProductCategoryRequestDto : PagedRequestBase
    {
        public int CategoryId { get; set; }
    }
}
