using Domain.Base;

namespace Api.Dtos.Categories
{
    public class CategoryDto : BaseCategoryDto, IEntity<int>
    {
        public int Id { get; set ; }
    }
}
