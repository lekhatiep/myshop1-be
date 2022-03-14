using AutoMapper;
using Domain.Common.Paging;
using Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Products
{
    public class PagedListTypeConverter : ITypeConverter<PagedList<Product>, PagedList<ProductDto>>
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

        public PagedList<ProductDto> Convert(PagedList<Product> source, PagedList<ProductDto> destination, ResolutionContext context)
        {


            var vm = source.Select(m => context.Mapper.Map<Product, ProductDto>(m)).ToList();

            return new PagedList<ProductDto>(vm, source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}
