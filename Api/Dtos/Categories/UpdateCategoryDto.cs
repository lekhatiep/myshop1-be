using Domain.Base;
using Domain.Interfaces.Audit;
using System;

namespace Api.Dtos.Categories
{
    public class UpdateCategoryDto : BaseCategoryDto, IEntity<int>
    {
        public int Id { get; set; }
    }
}
