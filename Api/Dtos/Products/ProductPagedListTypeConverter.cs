using AutoMapper;
using Domain.Common.Paging;
using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Products
{
    public class ProductPagedListTypeConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> 
       where TSource : Product
       where TDestination : ProductDto
    {
        //private readonly IMapper mapper;

        //public PagedListTypeConverter(IMapper _mapper)
        //{
        //    mapper = _mapper;
        //}

        //public PagedList<ProductDto> Convert(ResolutionContext context)
        //{
        //    var model = (PagedList<Product>)context.Items;
        //    var vm = model.Select(m => mapper.Map<Product,ProductDto >(m)).ToList();

        //    return new PagedList<ProductDto>(vm, vm.Count, model.CurrentPage, model.PageSize);
        //}

        //public PagedList<ProductDto> Convert(PagedList<Product> source, PagedList<ProductDto> destination, ResolutionContext context)
        //{

           
        //}

        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var result = new List<TDestination>();

            foreach (var item in source)
            {
                var productDto = context.Mapper.Map<TSource, TDestination>(item);
                if (item.ProductCategories.Any())
                {
                    productDto.CategoryId = item.ProductCategories.FirstOrDefault().CategoryId; 
                }
                
                result.Add(productDto);
            }

            //var vm = source.Select(m =>
            //    context.Mapper.Map<Product, ProductDto>(m)

            //).ToList();

            return new PagedList<TDestination>(result, source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}
