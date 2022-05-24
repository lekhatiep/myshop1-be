using Domain.Base;

namespace Api.Dtos.Products
{
    public class ProductDto : BaseProductDto, IEntity<int>
    {
        public int Id { get ; set ; }

        public string ImagePath { get; set; }

        public int CategoryId { get; set; }

    }
}
