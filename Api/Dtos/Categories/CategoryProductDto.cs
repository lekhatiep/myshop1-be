using Api.Dtos.ProductImages;
using Api.Dtos.Products;
using AutoMapper;
using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Categories
{
    [AutoMap(typeof(Category))]
    public class CategoryProductDto
    {
        public CategoryProductDto()
        {
            
        }

        public CategoryProductDto(Category c, ProductDto p, ProductImageDto i)
        {
            Id = c.Id;
            Name = c.Name;
            Product = p;
            //Product.ProductImages = new List<ProductImageDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ProductDto Product { get; set; }
    }
}
