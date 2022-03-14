using Api.Dtos.Categories;
using AutoMapper;
using Domain.Common.Paging;
using Domain.Entities.Catalog;
using Infastructure.Repositories.Catalogs.CategoryRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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
    }
}
