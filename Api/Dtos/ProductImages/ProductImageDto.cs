using AutoMapper;
using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.ProductImages
{
    [AutoMap(typeof(ProductImage))]
    public class ProductImageDto 
    {
        public long Id { get; set; }

        public string ImagePath { get; set; }

        public string Caption { get; set; }

        public bool IsDefault { get; set; }

        public int SortOrder { get; set; }
    }
}
