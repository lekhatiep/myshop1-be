using Api.Dtos.Categories;
using Api.Services.Categories;
using AutoMapper;
using Domain.Common.Paging;
using Domain.Common.Wrappers;
using Domain.Entities.Catalog;
using Infastructure.Repositories.Catalogs.CategoryRepo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(
            ICategoryService categoryService,
            ICategoryRepository categoryRepository,
            IMapper mapper
            )
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult GetAll ([FromQuery] PagedCategoryRequestDto categoryRequestDto)
        {
            var listCategories = _categoryService.GetCategoryPaging(categoryRequestDto);

            try
            {
                return Ok(new PagedReponse<PagedList<CategoryDto>>(listCategories) { 
                    
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseResult<object>(e.Message)) ;
            }
            
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                return Ok(new ResponseResult<Category>(category));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseResult<object>(e.Message));
            }
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newCategory = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.Insert(newCategory);
                await _categoryRepository.Save();

                return Ok();
            }
            catch (Exception e)
            {

                 return BadRequest(new ResponseResult<object>(e.Message));
            }
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = await _categoryRepository.GetById(id);

                category.ModifyTime = DateTime.Now;

                var categoryUpdate = _mapper.Map(updateCategoryDto, category);
                var categoryUpdated = await _categoryRepository.Update(category, updateCategoryDto.Id);

                return Ok(new ResponseResult<Category>(categoryUpdated));
            }
            catch (Exception e)
            {

                return BadRequest(new ResponseResult<object>(e.Message));
            }
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _categoryRepository.GetById(id);
                await _categoryRepository.Delete(id);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(new ResponseResult<object>(e.Message));
            }
        }
    }
}
