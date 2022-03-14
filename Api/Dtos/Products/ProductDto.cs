using Api.Dtos.ProductImages;
using Domain.Base;
using System.Collections.Generic;
using System.Linq;

namespace Api.Dtos.Products
{
    public class ProductDto : BaseProductDto, IEntity<int>
    {


        public int Id { get ; set ; }

        public string ImagePath { get; set; }

      
    }
}
