﻿using Api.Dtos.Products;
using Domain.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Products
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProduct();

        PagedList<ProductDto> GetAllProductPaging(ProductPagedRequestDto pagedRequestDto);

        Task<ProductDto> GetProductById(int id);

        Task<int> CreateProduct(CreateProductDto product);

        Task<ProductDto> UpdateProduct(UpdateProductDto product);

        Task DeleteProduct(int id);

        Task UpdateDefaultImage(UpdateProductDto product);

        PagedList<ProductDto> GetProductByCategoryId(ProductPagedRequestDto pagedRequestDto, int categoryId);
    }
}
