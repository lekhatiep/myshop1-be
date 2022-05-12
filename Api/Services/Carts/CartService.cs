using Api.Dtos.Categories;
using Api.Dtos.Products;
using Api.Services.Cart;
using AutoMapper;
using Domain.Common.Paging;
using Domain.Entities.Catalog;
using Infastructure.Repositories.Catalogs.CategoryRepo;
using Infastructure.Repositories.Catalogs.ProductCategoryRepo;
using Infastructure.Repositories.ProductRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Api.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public CartService(
            IMapper mapper,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository, 
            IProductCategoryRepository productCategoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }
 

        public PagedList<CategoryDto> GetCategoryPaging(PagedCategoryRequestDto pagedCategoryRequest)
        {
            //Query
            var queryCategory = _categoryRepository.List();

            //List category

            var listCategory = queryCategory.Where(x => x.IsDelete == false);
            var data = PagedList<Category>.ToPagedList(ref listCategory, pagedCategoryRequest.PageNumber, pagedCategoryRequest.PageSize);

            var dataResult = _mapper.Map<PagedList<CategoryDto>>(data);

            return dataResult;
        }

        public IQueryable<Product> GetAllProductByCategoryId(ProductPagedRequestDto pagedRequestDto, int categoryId)
        {
            var queryProduct = from c in _categoryRepository.List()
                     join cp in _productCategoryRepository.List() on c.Id equals cp.CategoryId into cpt
                     from cp in cpt.DefaultIfEmpty()
                     join p in _productRepository.List()
                              .Include(x=>x.ProductImages.Where(x=>x.IsDefault == true && x.IsDelete == false)) on cp.ProductId equals p.Id
                     where c.Id == categoryId &&
                     c.IsDelete == false && p.IsDelete == false 
                     select p                                                   
                    ;

            return queryProduct;
        }
    }
}
