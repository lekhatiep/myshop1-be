using Domain.Base;

namespace Api.Dtos.Products
{
    public class UpdateProductDto : BaseProductDto, IEntity<int>
    {
        public int Id { get; set; }
    }
}
